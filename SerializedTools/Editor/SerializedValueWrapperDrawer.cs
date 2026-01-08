using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VV.Scoring.Editor
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SerializedValueWrapper))]
    public class SerializedValueWrapperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valProp = property.FindPropertyRelative("value");
            return valProp == null ? EditorGUIUtility.singleLineHeight : EditorGUI.GetPropertyHeight(valProp, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valProp = property.FindPropertyRelative("value");
            if (valProp == null)
            {
                EditorGUI.LabelField(position, "SRValue missing 'value' field");
                return;
            }

            // Draw only the managed reference (or the property) — this prevents duplicate dropdowns
            EditorGUI.PropertyField(position, valProp, label, true);
        }
    }
    #endif
}