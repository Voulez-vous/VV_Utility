using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VV.Utility;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VV.ID
{
    public class SerializedMonoBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected VVGuid id;
        public VVGuid ID => id;
        
        [SerializeField]
        protected bool keepPrefabID;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (keepPrefabID) return;

            if (!PrefabUtility.IsPartOfAnyPrefab(this))
            {
                id.Init();
                return;
            }
            
            PropertyModification[] modificationsArray = PrefabUtility.GetPropertyModifications(gameObject);
            if(modificationsArray == null) return;
            List<PropertyModification> modifications = modificationsArray.ToList();
            bool hasModification = modifications.Any(
                modification => modification.propertyPath == (nameof(id) + ".vvidSerialized"));

            if (!hasModification)
            {
                id.Reset();
                
                modifications.Add(
                    new PropertyModification()
                    {
                        target = PrefabUtility.GetCorrespondingObjectFromSource(this), 
                        propertyPath = (nameof(id) + ".vvidSerialized")
                    });
                PrefabUtility.SetPropertyModifications(gameObject, modifications.ToArray());
                EditorUtility.SetDirty(gameObject);
                AssetDatabase.SaveAssets();
            }
            else
                id.Init();
        }

        [Button]
        public void InitID()
        {
            id.Init();
        }
        
#endif

        protected virtual void OnEnable() => id.Init();
    }
}