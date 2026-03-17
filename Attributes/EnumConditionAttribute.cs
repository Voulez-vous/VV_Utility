using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace VV.Utility
{
    public class EnumConditionAttribute : PropertyAttribute
    {
        public string ConditionalSourceField { get; private set; }
        public bool ShowWhenEqual { get; private set; }
        public int EnumValueIndex { get; private set; }
        public AttributeEngine Engine { get; }

        public EnumConditionAttribute(string conditionalSourceField, int enumValueIndex, 
            bool showWhenEqual = true, AttributeEngine engine = AttributeEngine.ImGui)
        {
            ConditionalSourceField = conditionalSourceField;
            EnumValueIndex = enumValueIndex;
            ShowWhenEqual = showWhenEqual;
            Engine = engine;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EnumConditionAttribute))]
    public class EnumConditionPropertyDrawer : PropertyDrawer
    {
        private EnumConditionAttribute Attr => (EnumConditionAttribute)attribute;
        
        #region IMGUI

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attr.Engine == AttributeEngine.UIToolkit)
                return; // skip IMGUI completely

            bool show = UpdateVisibility(property);
            if (!show)
                return;

            EditorGUI.PropertyField(position, property, label, true);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (Attr.Engine == AttributeEngine.UIToolkit)
                return -2; // prevent IMGUI drawing space

            return UpdateVisibility(property)
                ? EditorGUI.GetPropertyHeight(property, label, true)
                : -2;
        }
        
        /// <summary>
        /// Updates visibility for IMGui engine.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private bool UpdateVisibility(SerializedProperty property)
        {
            SerializedProperty cond = FindConditionProperty(property);
            if (cond == null)
                return true;
            
            if (cond.propertyType != SerializedPropertyType.Enum)
                return true;

            bool value = cond.enumValueIndex == Attr.EnumValueIndex;
            return Attr.ShowWhenEqual ? value : !value;
        }

        #endregion

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
            
            root.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                UpdateVisibility(property, condition, root);
            });

            root.TrackPropertyValue(condition, _ =>
            {
                UpdateVisibility(property, condition, root);
            });
            
            root.Add(new PropertyField(property));
            
            return root;
        }
        
        /// <summary>
        /// Updates visibility for UI Toolkit engine.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="cond"></param>
        /// <param name="root"></param>
        private void UpdateVisibility(SerializedProperty property, SerializedProperty cond, VisualElement root)
        {
            bool shouldShow = cond == null || (Attr.ShowWhenEqual ? cond.boolValue : !cond.boolValue);

            bool isEmptyList = property.isArray && property.arraySize == 0;

            root.style.display = shouldShow && !isEmptyList
                ? DisplayStyle.Flex
                : DisplayStyle.None;
        }

        #endregion

        #region Shared

        private SerializedProperty FindConditionProperty(SerializedProperty property)
        {
            string path = property.propertyPath.Replace(property.name, Attr.ConditionalSourceField);
            return property.serializedObject.FindProperty(path);
        }

        #endregion
    }
#endif
}

