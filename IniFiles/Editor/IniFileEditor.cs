#if UNITY_EDITOR
using UnityEditor;

namespace IniFiles.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(IniFile))]
    public class IniFileEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            foreach (var o in targets)
            {
                var iniObject = o as IniFile;
                if (iniObject == null) continue;

                if (targets.Length > 1)
                {
                    var filename = AssetDatabase.GetAssetPath(iniObject);
                    EditorGUILayout.LabelField(filename, EditorStyles.boldLabel);
                }

                foreach ((string sectionKey, IniFile.IniSection section) in iniObject.sections)
                {
                    EditorGUILayout.LabelField($"[{sectionKey}]", EditorStyles.boldLabel);

                    foreach ((string key, string value) in section.values)
                        EditorGUILayout.TextField(key, value);

                    EditorGUILayout.Space();
                }

                EditorGUILayout.Separator();
            }
        }
    }
}
#endif