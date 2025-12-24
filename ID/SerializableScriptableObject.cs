using System;
using UnityEngine;
using VV.ID;

#if UNITY_EDITOR
using UnityEditor;
using VV.Utility;
#endif

namespace VV.SO
{
    public class SerializableScriptableObject : ScriptableObject
    {
        [SerializeField]
        protected VVGuid vvGuid;
        public VVGuid VVGuid => vvGuid;
        
        protected virtual void OnEnable() => vvGuid.Init();

#if UNITY_EDITOR
        protected virtual void Awake() => vvGuid.Init();

        protected virtual void OnValidate() => vvGuid.Init();

        #region Reliable In-Editor OnDestroy

        // Sadly OnDestroy is not being called reliably by the editor. So we need this.
        // Thanks to: https://discussions.unity.com/t/845563/6
        class OnDestroyProcessor : AssetModificationProcessor
        {
            // Cache the type for reuse.
            private static readonly Type CurrentType = typeof(SerializableScriptableObject);

            // Limit to certain file endings only.
            private const string FileEnding = ".asset";

            public static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions _)
            {
                if (!path.EndsWith(FileEnding))
                    return AssetDeleteResult.DidNotDelete;

                Type assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
                if (assetType != null && (assetType == CurrentType || assetType.IsSubclassOf(CurrentType)))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<SerializableScriptableObject>(path);
                    asset.OnDestroy();
                }

                return AssetDeleteResult.DidNotDelete;
            }
        }

        #endregion

        protected virtual void OnDestroy() { }
        
        protected void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        [Button]
        public void GenerateNewGuid()
        {
            vvGuid.Reset();
            Save();
        }
#endif
    }
}