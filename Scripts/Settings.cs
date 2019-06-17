using System.IO;
using UnityEditor;
using UnityEngine;

namespace ScriptHeader
{
    internal class Settings : ScriptableObject
    {
        [SerializeField]
        private string developerName;

        [SerializeField]
        private string scriptExtension;

        [Space]
        [TextArea(10, 100)]
        [SerializeField]
        private string headerTemplate;

        private const string assetPath = "Assets/ScriptHeader/Settings.asset";
        private const string assetName = "Settings";
        private const string assetExtension = ".asset";

        private static Settings instance;

        public static bool HasInstance => instance != null;
        public static string DeveloperName => instance.developerName;
        public static string ScriptExtension => instance.scriptExtension;
        public static string HeaderTemplate => instance.headerTemplate;

        public static void CreateInstance()
        {
            instance = AssetDatabase.LoadAssetAtPath<Settings>(assetPath);

            if (HasInstance) return;

            instance = CreateInstance<Settings>();           
            AssetDatabase.CreateAsset(instance, assetPath);
        }

        [MenuItem("ScriptHeader/Edit Settings")]
        public static void Edit()
        {
            Selection.activeObject = instance;
        }

        [MenuItem("ScriptHeader/Github")]
        public static void OpenGithub()
        {
            string url = "https://github.com/onurtnrkl/unity3d-script-header";
            Application.OpenURL(url);
        }
    }
}