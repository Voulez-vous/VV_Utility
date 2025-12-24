using System;
using System.Reflection;
using UnityEngine;

namespace VV.Utility
{
    /// <summary>
    /// Can get the specified field in the editor.
    /// Also works with nested fields : user.name
    /// And lists : user.state[0]
    /// </summary>
    [Serializable]
    public class GeneratedFieldDataGetter
    {
        [SerializeReference] private UnityEngine.Object targetObject;
        [Tooltip("Get data through nested objects. e.g. user.name / user.state[0]")]
        [SerializeField] private string fieldPath;

        public object TargetObject => targetObject;

        public string FieldPath => fieldPath;

        public object GetData()
        {
            if (targetObject == null || string.IsNullOrEmpty(fieldPath))
            {
                Debug.LogWarning("Target asset or field path is not set.");
                return null;
            }

            object currentObject = targetObject;
            Type currentType = targetObject.GetType();

            foreach (string part in fieldPath.Split('.'))
            {
                if (currentObject == null) return null;

                if (part.Contains("["))
                {
                    string fieldName = part.Substring(0, part.IndexOf("[", StringComparison.InvariantCulture));
                    string indexStr = part.Substring(part.IndexOf("[", StringComparison.InvariantCulture) + 1, part.IndexOf("]", StringComparison.InvariantCulture) - part.IndexOf("[", StringComparison.InvariantCulture) - 1);
                    int index = int.Parse(indexStr);

                    FieldInfo listField = currentType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    
                    var list = listField?.GetValue(currentObject) as System.Collections.IList;
                    currentObject = list?[index];
                    
                    currentType = currentObject?.GetType();
                }
                else
                {
                    FieldInfo field = currentType.GetField(part, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    currentObject = field?.GetValue(currentObject);
                    currentType = field?.FieldType;
                }
            }

            return currentObject;

        }
    }
}