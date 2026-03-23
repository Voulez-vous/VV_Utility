using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Events;
#endif

namespace VV.Utility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string name;
        public int size;
        public int space;
        public Color color;
        public AttributeEngine engine = AttributeEngine.ImGui;

        public ButtonAttribute(string name = "", int size = 20, int space = 0, string color = "gray", AttributeEngine engine = AttributeEngine.ImGui)
        {
            this.name = name;
            this.size = size;
            this.space = space;
            this.color = ColorUtility.TryParseHtmlString(color, out Color parsedColor) ? parsedColor : Color.gray;
        }
        
        public ButtonAttribute(string name) => this.name = name;
    }
    
#if UNITY_EDITOR
    [InitializeOnLoad]
    public static class ButtonAttributeInjector
    {
        // keep track of element -> last target instance id to avoid duplicate insertion
        private static readonly Dictionary<int, int> s_attached = new();

        static ButtonAttributeInjector()
        {
            // IMGUI header hook (works for builtin inspectors)
            Editor.finishedDefaultHeaderGUI += OnPostHeaderGUI;

            // For UIToolkit inspectors we poll inspector windows and attach buttons as needed.
            // Polling is cheap because we do small checks and cache attachments.
            EditorApplication.update += OnEditorUpdate;
        }

        #region IMGUI

        private static void OnPostHeaderGUI(Editor editor)
        {
            if (editor == null) return;

            var targets = editor.targets;
            if (targets == null || targets.Length == 0) return;

            // Use the first target's type to find methods
            Type type = targets[0].GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                var attr = method.GetCustomAttribute<ButtonAttribute>();
                if(attr == null) continue;
                if (attr is not { engine: AttributeEngine.ImGui }) continue;
                if (method.GetParameters().Length > 0)
                {
                    // skip parameterized methods
                    continue;
                }

                GUILayout.Space(attr.space);

                Color prevColor = GUI.backgroundColor;
                
                GUI.backgroundColor = attr.color;

                var label = string.IsNullOrEmpty(attr.name) ? ObjectNames.NicifyVariableName(method.Name) : attr.name;

                if (GUILayout.Button(label, GUILayout.Height(attr.size)))
                {
                    foreach (Object t in editor.targets)
                    {
                        try { method.Invoke(t, null); }
                        catch (Exception ex) { Debug.LogException(ex); }
                    }
                }

                GUI.backgroundColor = prevColor;
            }
        }

        #endregion

        #region UI Toolkit

        private static void OnEditorUpdate()
        {
            // Find all open inspector windows
            var editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            foreach (EditorWindow w in editorWindows)
            {
                // match by name to avoid referencing internal types directly
                if (w == null || w.GetType().Name != "InspectorWindow") continue;
                TryAttachToInspectorWindow(w);
            }
        }

        private static void TryAttachToInspectorWindow(EditorWindow inspectorWindow)
        {
            try
            {
                VisualElement root = inspectorWindow.rootVisualElement;
                if (root == null) return;

                // Query inspector elements (each one corresponds to a visible editor)
                var inspectorElements = root.Query<VisualElement>(className: "unity-inspector-element").ToList();
                foreach (VisualElement ie in inspectorElements)
                {
                    if (ie == null) continue;
                    // unique key per VisualElement instance
                    int key = ie.GetHashCode();

                    // Get the Editor instance for this inspector element.
                    // Many Unity versions set the Editor as userData; otherwise try reflection fallbacks.
                    Editor editor = ie.userData as Editor;
                    if (editor == null)
                    {
                        object maybeEditor = TryGetEditorFromInspectorElement(ie);
                        editor = maybeEditor as Editor;
                    }
                    if (editor == null) continue;

                    // Determine primary target instance id (0 if none)
                    Object primary = editor.target;
                    int primaryId = primary != null ? primary.GetInstanceID() : 0;

                    // If we've already attached for this element + same target, skip
                    if (s_attached.TryGetValue(key, out var prevId) && prevId == primaryId)
                        continue;
                    s_attached[key] = primaryId;

                    // Remove existing container if present (rebuild)
                    VisualElement existing = ie.Q("button-attribute-container");
                    if (existing != null) existing.RemoveFromHierarchy();

                    // Build new container (column)
                    var container = new VisualElement();
                    container.name = "button-attribute-container";
                    container.style.flexDirection = FlexDirection.Column;
                    container.style.marginTop = 4;
                    container.style.paddingLeft = 2;
                    container.style.paddingRight = 2;

                    // Attach container to the inspector element (at the end)
                    ie.Add(container);
                }
            }
            catch (Exception ex)
            {
                // Prevent editor spam if something goes wrong; log once
                Debug.LogException(ex);
            }
        }

        // Try multiple reflection strategies to extract an Editor instance from the inspector element
        private static object TryGetEditorFromInspectorElement(VisualElement inspectorElement)
        {
            try
            {
                Type type = inspectorElement.GetType();

                // 1) "userData" already attempted earlier (common)
                // 2) try property "editor"
                PropertyInfo prop = type.GetProperty("editor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (prop != null)
                {
                    var val = prop.GetValue(inspectorElement);
                    if (val is Editor) return val;
                }

                // 3) try field "m_Editor" or "editor"
                FieldInfo field = type.GetField("m_Editor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                  ?? type.GetField("editor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (field != null)
                {
                    var val = field.GetValue(inspectorElement);
                    if (val is Editor) return val;
                }

                // 4) sometimes there's a property "inspector" or "targetEditor" — attempt generically
                var maybeNames = new[] { "inspector", "targetEditor", "editorInstance" };
                foreach (var name in maybeNames)
                {
                    PropertyInfo p = type.GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (p != null)
                    {
                        var v = p.GetValue(inspectorElement);
                        if (v is Editor) return v;
                    }

                    FieldInfo f = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (f != null)
                    {
                        var v2 = f.GetValue(inspectorElement);
                        if (v2 is Editor) return v2;
                    }
                }
            }
            catch(Exception ex)
            {
                // ignored - reflection can fail across Unity versions
                Debug.LogException(ex);
            }

            return null;
        }
        #endregion
    }
    
    [CustomEditor(typeof(Object), true, isFallback = true)]
    public class ButtonAttributeEditor : Editor
    {
        private List<ButtonAttribute> buttonAttrList = new();
        private int nbImguiAttrs;
        private int nbUitkAttrs;

        private bool useIMGUI => nbImguiAttrs >= nbUitkAttrs;

        private UnityAction OnAttributesGathered;

        private void GatherAttributes()
        {
            buttonAttrList.Clear();
            nbImguiAttrs = 0;
            nbUitkAttrs = 0;
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            foreach (MethodInfo method in methods)
            {
                var buttonAttr = method.GetCustomAttribute<ButtonAttribute>();
                if(buttonAttr == null) continue;
                nbImguiAttrs += buttonAttr.engine == AttributeEngine.ImGui ? 1 : 0;
                nbUitkAttrs += buttonAttr.engine == AttributeEngine.UIToolkit ? 1 : 0;
                buttonAttrList.Add(buttonAttr);
            }
            
            OnAttributesGathered?.Invoke();
        }
        
        #region UI Toolkit

        public override VisualElement CreateInspectorGUI()
        {
            GatherAttributes();
            if(useIMGUI) return base.CreateInspectorGUI();
            
            // Default inspector
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
        
            // Find all methods with [Button]
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        
            foreach (MethodInfo method in methods)
            {
                var buttonAttr = method.GetCustomAttribute<ButtonAttribute>();
                if(buttonAttr == null) continue;
                if (buttonAttr is not { engine: AttributeEngine.UIToolkit })
                    continue;
        
                var buttonName = string.IsNullOrEmpty(buttonAttr.name) ? method.Name : buttonAttr.name;
                var button = new Button(() =>
                {
                    method.Invoke(target, null);
                })
                {
                    text = buttonName,
                    style =
                    {
                        height = buttonAttr.size,
                        marginTop = buttonAttr.space,
                        marginBottom = buttonAttr.space,
                        backgroundColor = new StyleColor(buttonAttr.color)
                    }
                };

                root.Add(button);
            }
        
            return root;
        }

        #endregion

        #region IMGUI

        public override void OnInspectorGUI()
        {
            // Draw IMGUI default inspector
            DrawDefaultInspector();

            // Draw buttons for IMGUI mode
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                var buttonAttr = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttr is not { engine: AttributeEngine.ImGui })
                    continue;

                GUILayout.Space(buttonAttr.space);

                GUI.backgroundColor = buttonAttr.color;

                var buttonName = string.IsNullOrEmpty(buttonAttr.name) ? method.Name : buttonAttr.name;

                if (GUILayout.Button(buttonName, GUILayout.Height(buttonAttr.size)))
                {
                    method.Invoke(target, null);
                }

                GUI.backgroundColor = Color.white;
            }
        }
        
        #endregion
    }
#endif
}