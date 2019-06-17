using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace ScriptHeader
{
    [InitializeOnLoad]
    internal class Processor : UnityEditor.AssetModificationProcessor
    {
        static Processor()
        {
            if (Settings.HasInstance) return;
            
            Settings.CreateInstance();
        }

        public static void OnWillCreateAsset(string path)
        {
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(name);

            if (extension.Equals(Settings.ScriptExtension))
            {
                path = Path.Combine(directory, name);
                string header = Settings.HeaderTemplate;
                string body = File.ReadAllText(path);
                string file = BuildFile(header, body);
                File.WriteAllText(path, file);
                AssetDatabase.Refresh();
            }
        }

        private static void ReplaceHeader(StringBuilder builder)
        {
            builder.Replace("#DATE#", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            builder.Replace("#YEAR#", DateTime.Now.Year.ToString());
            builder.Replace("#PRODUCTNAME#", PlayerSettings.productName);
            builder.Replace("#DEVELOPERNAME#", Settings.DeveloperName);
            builder.Replace("#COMPANYNAME#", PlayerSettings.companyName);
        }

        private static string BuildFile(string header, string body)
        {
            int capacity = header.Length + body.Length + 2;
            StringBuilder builder = new StringBuilder(capacity);
            builder.Append(header);
            ReplaceHeader(builder);
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append(body);

            return builder.ToString();
        }
    }
}