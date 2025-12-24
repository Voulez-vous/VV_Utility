using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VV.Utility;

namespace IniFiles
{
    public class IniFile : ScriptableObject
    {
        [Serializable]
        public class IniSection
        {
            public Dictionary<string, string> values = new();
        }

        public Dictionary<string, IniSection> sections = new();

        public bool TryAddSection(string sectionName, Dictionary<string, string> sectionValues = null) => 
            sections.TryAdd(sectionName, new IniSection() { values = sectionValues ?? new Dictionary<string, string>() });

        public bool TryAddValue(string sectionName, string key, string value) =>
            sections.TryGetValue(sectionName, out IniSection section) && section.values.TryAddValue(key, value);

        public bool TryAddValues(string sectionName, Dictionary<string, string> values)
        {
            if (!sections.TryGetValue(sectionName, out IniSection section)) return false;
            
            bool success = true;
            foreach ((string key, string value) in values)
                success = success && section.values.TryAddValue(key, value);

            return success;
        }
        
        public string GetValue(string sectionName, string key) => 
            sections.TryGetValue(sectionName, out IniSection section) ? section.values.GetValueOrDefault(key) : null;

#if !UNITY_EDITOR && UNITY_WEBGL
        public static async Task<IniFile> OpenIniFile(string filePath)
        {
            Debug.Log("OpenIniFile WEB GL : " + filePath);

            UnityWebRequest fileUnityWebRequest = new UnityWebRequest(filePath)
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            await fileUnityWebRequest.SendWebRequest();

            if (fileUnityWebRequest.result != UnityWebRequest.Result.Success) return null;

            return ParseIniFile(fileUnityWebRequest.downloadHandler.text);
        }
#else
        public static IniFile OpenIniFile(string filePath)
        {
            Debug.Log("OpenIniFile : " + filePath);
            if (!File.Exists(filePath)) return null;
            var textContent = File.ReadAllText(filePath);
            Debug.Log("OpenIniFile : " + textContent);
            return ParseIniFile(textContent);
        }
#endif
        
        public static IniFile ParseIniFile(string iniFileContent)
        {
            var sections = new Dictionary<string, IniSection>();
            IniSection currentSection = null;
            using (StringReader stringReader = new StringReader(iniFileContent))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    //Comment
                    if (line.StartsWith(";")) continue;
                    //Section
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        currentSection = new IniSection();
                        sections.Add(line.TrimStart('[').TrimEnd(']'), currentSection);
                        continue;
                    }

                    //Key=Value
                    if (line.Contains("="))
                    {
                        var key = line.Substring(0, line.IndexOf("=", StringComparison.Ordinal));
                        var value = line.Substring(key.Length + 1);

                        if (currentSection == null)
                        {
                            currentSection = new IniSection();
                            sections.Add("default", currentSection);
                        }
                        currentSection.values.Add(key, value);
                    }
                }
            }
            
            var ini = CreateInstance<IniFile>();
            ini.sections = sections;
            return ini;
        }
        
        public void SaveIniFile(string filepath, string filename)
        {
            if(!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);
            StreamWriter streamWriter = new StreamWriter(filepath + filename);
            
            streamWriter.WriteLine("version " + Application.version);
            
            foreach ((string sectionKey, IniSection section) in sections)
            {
                streamWriter.WriteLine("[" + (sectionKey != "" ? sectionKey : "default") + "]");

                foreach ((string key,string value) in section.values)
                    streamWriter.WriteLine(key + "=" + value);
            }
            
            streamWriter.Close();
        }
    }
}