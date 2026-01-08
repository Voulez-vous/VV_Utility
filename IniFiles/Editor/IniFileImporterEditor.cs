#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AssetImporters;

namespace IniFiles.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(IniFileImporter))]
    public class IniFileImporterEditor : ScriptedImporterEditor
    {
    }
}
#endif