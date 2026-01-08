using System;
using System.Collections;
using System.Reflection;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VV.Utility
{
    public static class SerializedPropertyExtensions
    {
        #if UNITY_EDITOR
        
        /// <summary>
        /// Get the specific object underneath the property.
        /// </summary>
        /// <param name="property"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetUnderlyingValue<T>(this SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;
            var path = property.propertyPath.Replace(".Array.data[", "[");
            var elements = path.Split('.');

            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var name = element[..element.IndexOf("[")];
                    int index = Convert.ToInt32(
                        element[(element.IndexOf("[") + 1)..^1]);

                    obj = GetField(obj, name);
                    obj = ((IList)obj)[index];
                }
                else
                {
                    obj = GetField(obj, element);
                }
            }

            return (T)obj;
        }
        
        /// <summary>
        /// Get the generic object underneath the property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetUnderlyingValue(this SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;
            var path = property.propertyPath.Replace(".Array.data[", "[");
            var elements = path.Split('.');

            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var name = element[..element.IndexOf("[")];
                    int index = int.Parse(
                        element[(element.IndexOf("[") + 1)..^1]
                    );

                    obj = GetField(obj, name);
                    obj = ((IList)obj)[index];
                }
                else
                {
                    obj = GetField(obj, element);
                }
            }

            return obj;
        }

        /// <summary>
        /// Get a specific field value from a source object.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object GetField(object source, string name)
        {
            if (source == null) return null;
            Type type = source.GetType();

            // Try fields first
            FieldInfo field = type.GetField(
                name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (field != null)
                return field.GetValue(source);

            // Then auto-properties
            PropertyInfo prop = type.GetProperty(
                name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return prop?.GetValue(source);
        }
        
        /// <summary>
        /// Get the type from a serialized property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Type GetElementType(this SerializedProperty property)
        {
            Object target = property.serializedObject.targetObject;
            Type type = target.GetType();
            FieldInfo field = type.GetField(property.propertyPath, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (field == null)
                return null;

            Type fieldType = field.FieldType;
            if (fieldType.IsGenericType)
            {
                return fieldType.GetGenericArguments()[0]; // returns typeof(T)
            }

            return null;
        }
        
        #endif
    }
}