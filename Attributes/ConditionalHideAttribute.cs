using System;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace VV.Utility
{
    /// <summary>
    /// Attribute to conditionally hide fields in the inspector based on a boolean field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ConditionalHideAttribute : PropertyAttribute
    {
        public string ConditionalSourceField { get; private set; }
        public bool HideWhenTrue { get; private set; }
        public AttributeEngine Engine { get; }

        /// <summary>
        /// Hide field when the conditional source field is true/false
        /// </summary>
        /// <param name="conditionalSourceField">Name of the boolean field to check</param>
        /// <param name="hideWhenTrue">If true, hide when source field is true. If false, hide when source field is false</param>
        /// <param name="engine">Choose the rendering engine</param>
        public ConditionalHideAttribute(
            string conditionalSourceField,
            bool hideWhenTrue = true,
            AttributeEngine engine = AttributeEngine.ImGui)
        {
            ConditionalSourceField = conditionalSourceField;
            HideWhenTrue = hideWhenTrue;
            Engine = engine;
        }
    }
     
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHidePropertyDrawer : PropertyDrawer
    {
        private ConditionalHideAttribute Attr => (ConditionalHideAttribute)attribute;

        #region UI Toolkit

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return Attr.Engine == AttributeEngine.ImGui ? null : // forces Unity to fall back to IMGUI
                // Use your UITK drawer here
                CreateUitkView(property);
        }

        private VisualElement CreateUitkView(SerializedProperty property)
        {
            var root = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Column
                }
            };

            SerializedProperty condition = FindConditionProperty(property);

            // Bind visibility
            root.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                UpdateVisibility(property, condition, root);
            });

            root.TrackPropertyValue(condition, _ =>
            {
                UpdateVisibility(property, condition, root);
            });

            // ============================================================
            //   CUSTOM LIST HANDLING (IMPORTANT PART)
            // ============================================================
            if (property.isArray && property.propertyType != SerializedPropertyType.String)
            {
                var listRoot = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Column,
                        marginLeft = 16
                    }
                };

                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty element = property.GetArrayElementAtIndex(i);

                    // Create inline row (NO FOLDOUT, NO HEADER)
                    var row = new VisualElement
                    {
                        style =
                        {
                            flexDirection = FlexDirection.Row,
                            alignItems = Align.Center
                        }
                    };

                    // Element field
                    var elementField = new PropertyField(element)
                    {
                        style =
                        {
                            flexGrow = 1
                        }
                    };

                    // Remove button
                    var removeBtn = new Button(() =>
                    {
                        property.DeleteArrayElementAtIndex(i);
                        property.serializedObject.ApplyModifiedProperties();
                        RefreshList(property, listRoot);
                    })
                    {
                        text = "–"
                    };

                    row.Add(elementField);
                    row.Add(removeBtn);

                    listRoot.Add(row);
                }

                // Add button
                var addBtn = new Button(() =>
                {
                    property.InsertArrayElementAtIndex(property.arraySize);
                    property.serializedObject.ApplyModifiedProperties();
                    RefreshList(property, listRoot);
                })
                {
                    text = "+"
                };

                root.Add(listRoot);
                root.Add(addBtn);

                return root;
            }

            // fallback for normal fields
            root.Add(new PropertyField(property));
            return root;
        }
        
        private void RefreshList(SerializedProperty property, VisualElement listRoot)
        {
            listRoot.Clear();

            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);

                var row = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row
                    }
                };

                var elementField = new PropertyField(element)
                {
                    style =
                    {
                        flexGrow = 1
                    }
                };

                var removeBtn = new Button(() =>
                {
                    property.DeleteArrayElementAtIndex(i);
                    property.serializedObject.ApplyModifiedProperties();
                    RefreshList(property, listRoot);
                })
                {
                    text = "–"
                };

                row.Add(elementField);
                row.Add(removeBtn);
                listRoot.Add(row);
            }
        }
        
        #endregion
        
        #region ImGUI 
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attr.Engine == AttributeEngine.UIToolkit)
                return; // skip IMGUI completely

            bool show = ShouldShow(property);
            if (!show)
                return;

            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (Attr.Engine == AttributeEngine.UIToolkit)
                return -2; // prevent IMGUI drawing space

            return ShouldShow(property)
                ? EditorGUI.GetPropertyHeight(property, label, true)
                : -2;
        }
        
        #endregion
        
        #region Shared Logic
        
        private SerializedProperty FindConditionProperty(SerializedProperty property)
        {
            string path = property.propertyPath.Replace(property.name, Attr.ConditionalSourceField);
            return property.serializedObject.FindProperty(path);
        }

        private bool ShouldShow(SerializedProperty property)
        {
            SerializedProperty cond = FindConditionProperty(property);
            if (cond == null)
                return true;

            bool value = cond.boolValue;
            return Attr.HideWhenTrue ? !value : value;
        }

        private void UpdateVisibility(SerializedProperty property, SerializedProperty cond, VisualElement root)
        {
            bool shouldShow = cond == null || (Attr.HideWhenTrue ? !cond.boolValue : cond.boolValue);

            bool isEmptyList = property.isArray && property.arraySize == 0;

            root.style.display = shouldShow && !isEmptyList
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }
        
        #endregion
    }
#endif
}