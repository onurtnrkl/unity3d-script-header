using UnityEditor;
using UnityEngine;

namespace ScriptHeader
{
    internal class Settings : ScriptableObject
    {
        private static Settings instance;

        [SerializeField]
        private string developerName;

        [SerializeField]
        private string scriptExtension;

        [Space]
        [TextArea(10, 100)]
        [SerializeField]
        private string headerTemplate;

        public static bool HasInstance => instance != null;

        public static string DeveloperName => instance.developerName;

        public static string ScriptExtension => instance.scriptExtension;

        public static string HeaderTemplate => instance.headerTemplate;

        public static void CreateInstance()
        {
            string path = "Assets/ScriptHeader/Settings.asset";
            instance = AssetDatabase.LoadAssetAtPath<Settings>(path);

            if (HasInstance)
            {
                return;
            }

            instance = CreateInstance<Settings>();
            AssetDatabase.CreateAsset(instance, path);
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
