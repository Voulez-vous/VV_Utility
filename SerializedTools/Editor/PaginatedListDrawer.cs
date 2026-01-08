using System;
using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VV.Utility.SerializedTools
{
    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PaginatedSerializedList<>), true)]
    public class PaginatedListDrawer : PropertyDrawer
    {
        const float Spacing = 4f;
        const float InnerPadding = 4f;
        const float BoxDefaultHeight = 10f;
        const float EmptyLabelHeight = 18f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Find children
            SerializedProperty itemsProp = property.FindPropertyRelative("visibleItems");
            SerializedProperty pageProp = property.FindPropertyRelative("page");
            SerializedProperty perPageProp = property.FindPropertyRelative("itemsPerPage");
            SerializedProperty foldoutProp = property.FindPropertyRelative("foldout");
            
            // Collapsed = only height of the foldout line
            if (!foldoutProp.boolValue)
                return EditorGUIUtility.singleLineHeight;

            // Basic safety
            if (itemsProp == null)
                return EditorGUIUtility.singleLineHeight;

            int page = (pageProp != null) ? Mathf.Max(0, pageProp.intValue) : 0;
            int perPage = perPageProp is { intValue: > 0 } ? perPageProp.intValue : 10;

            int total = itemsProp.arraySize;
            int start = page * perPage;
            start = Mathf.Clamp(start, 0, Math.Max(0, total - 1));
            int end = Mathf.Min(start + perPage, total);

            // pagination row height
            float h = EditorGUIUtility.singleLineHeight + Spacing + BoxDefaultHeight + 20f + 20f;

            if (itemsProp.arraySize == 0)
                h += EmptyLabelHeight;

            // add heights for visible elements
            for (int i = 0; i < total; i++)
            {
                SerializedProperty elem = itemsProp.GetArrayElementAtIndex(i);
                h += EditorGUI.GetPropertyHeight(elem, true) + Spacing;
            }

            return h;
        }

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            SerializedProperty itemsProp = property.FindPropertyRelative("visibleItems");
            SerializedProperty pageProp = property.FindPropertyRelative("page");
            SerializedProperty itemsPerPageProp = property.FindPropertyRelative("itemsPerPage");
            SerializedProperty foldoutProp = property.FindPropertyRelative("foldout");
            object paginatedList = property.GetUnderlyingValue();
            Type listType = paginatedList.GetType();

            EditorGUI.BeginProperty(pos, label, property);
            
            float x = pos.x;
            float y = pos.y;
            float width = pos.width;
            
            Rect foldoutRect = new Rect(x, y, width, EditorGUIUtility.singleLineHeight);
            y += 20;
            foldoutProp.boolValue = EditorGUI.Foldout(foldoutRect, foldoutProp.boolValue, label, true);
            
            if (!foldoutProp.boolValue)
                return;

            // Draw label
            Rect header = new Rect(x, y, width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(header, label);

            int currentPage = pageProp.intValue;
            int perPage = itemsPerPageProp.intValue;

            pos.y = y;
            ControlsDisplay(pos, listType, paginatedList);

            y += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;

            int start = currentPage * perPage;
            int end = Mathf.Min(start + perPage, itemsProp.arraySize);

            Rect boxRect = new Rect(
                x,
                y,
                width,
                GetElementsHeight(itemsProp, start, end) + InnerPadding * 2
            );

            GUI.Box(boxRect, GUIContent.none, EditorStyles.helpBox);

            y += InnerPadding;

            // If empty, show message and return
            if (itemsProp.arraySize == 0)
            {
                EditorGUI.LabelField(new Rect(x + InnerPadding, y, width - InnerPadding * 2, EmptyLabelHeight),
                    "List is empty", EditorStyles.miniLabel);
                return;
            }

            // Draw each element inside the box
            for (int i = 0; i < itemsProp.arraySize; i++)
            {
                SerializedProperty element = itemsProp.GetArrayElementAtIndex(i);
                float h = EditorGUI.GetPropertyHeight(element, true);

                Rect elementRect = new Rect(
                    x + 20f,
                    y,
                    width - InnerPadding * 2,
                    h
                );

                EditorGUI.PropertyField(elementRect, element, true);
                y += h + 2;
            }

            EditorGUI.EndProperty();
        }
        
        private float GetElementsHeight(SerializedProperty itemsProp, int start, int end)
        {
            float h = 0;
            for (int i = 0; i < itemsProp.arraySize; i++)
                h += EditorGUI.GetPropertyHeight(itemsProp.GetArrayElementAtIndex(i), true) + 2;

            return Mathf.Max(h, 20f); // Give room for "List is empty"
        }

        private void ControlsDisplay(Rect pos, Type paginatedListType,  object paginatedList)
        {
            // Define widths
            float btnWidth = 30f;
            float labelWidth = 50f;
            float fieldWidth = 40f;
            float gap = 6f;

            PropertyInfo totalPagesProp = paginatedListType.GetProperty("TotalPages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            int totalPages = (int)totalPagesProp?.GetValue(paginatedList)!;
            
            PropertyInfo totalProp = paginatedListType.GetProperty("Count", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            int total = (int)totalProp?.GetValue(paginatedList)!;
            
            PropertyInfo pageProp = paginatedListType.GetProperty("Page", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            int currentPage = (int)pageProp?.GetValue(paginatedList)!;
            
            PropertyInfo itemsPerPageProp = paginatedListType.GetProperty("ItemsPerPage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            int itemsPerPage = (int)itemsPerPageProp?.GetValue(paginatedList)!;

            // Pagination bar
            Rect row = new Rect(pos.x, pos.y + EditorGUIUtility.singleLineHeight + 2, pos.width,
                EditorGUIUtility.singleLineHeight);
            
            float x = row.x;
            float y = row.y;
            float h = row.height;

            // Previous
            if (GUI.Button(new Rect(x, y, btnWidth, h), "<"))
            {
                MethodInfo previousPageMethod = paginatedListType.GetMethod("PreviousPage");
                if (previousPageMethod != null) previousPageMethod.Invoke(paginatedList, null);
            }
                
            x += btnWidth + gap;

            // Next
            if (GUI.Button(new Rect(x, y, btnWidth, h), ">"))
            {
                MethodInfo previousPageMethod = paginatedListType.GetMethod("NextPage");
                if (previousPageMethod != null) previousPageMethod.Invoke(paginatedList, null);
            }
            x += btnWidth + gap;

            // Page counter
            EditorGUI.LabelField(new Rect(x, y, labelWidth, h),
                $"Page");
            x += labelWidth + gap;
            EditorGUI.BeginChangeCheck();
            int newPageInput = EditorGUI.IntField(new Rect(x, y, fieldWidth, h),
                currentPage + 1);
            if (EditorGUI.EndChangeCheck())
            {
                pageProp.SetValue(paginatedList, newPageInput);

                // Force repaint
                GUI.changed = true;
            }
            x += fieldWidth + gap;
            EditorGUI.LabelField(new Rect(x, y, labelWidth, h),
                $"{totalPages}");
            x += labelWidth + gap;

            // Items per page field
            EditorGUI.LabelField(new Rect(x, y, labelWidth, h),
                "Per Page");
            x += labelWidth + gap;
            EditorGUI.BeginChangeCheck();
            int newPerPageValue = EditorGUI.IntField(new Rect(x, y, fieldWidth, h), itemsPerPage);
            if (EditorGUI.EndChangeCheck())
            {
                itemsPerPageProp.SetValue(paginatedList, newPerPageValue);

                // Force repaint
                GUI.changed = true;
            }
            x += fieldWidth + gap;
            
            // Total items
            EditorGUI.LabelField(new Rect(x, y, labelWidth, h),
                "Total");
            x += labelWidth + gap;
            EditorGUI.IntField(new Rect(x, y, fieldWidth, h),
                total);
            x += fieldWidth + gap;
        }
    }
    #endif
}