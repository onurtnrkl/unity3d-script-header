#region License
// ====================================================
// Product:    TapEngine
// Developer:  Onur Tanrıkulu
// Date:       24/05/2018 11:21
// Copyright (c) 2018 Onur Tanrikulu. All rights reserved.
// ====================================================
#endregion

using System;
using System.IO;
using System.Text;
using UnityEditor;

namespace TapEditor
{
    internal sealed class ScriptHeader : UnityEditor.AssetModificationProcessor
    {
        ////TODO: Use asset for settings.
        private const string TemplatePath = "Assets/ScriptHeader/Template.txt";
        private const string DeveloperName = "Onur Tanrıkulu";
        private const string ScriptExtension = ".cs";

        public static void OnWillCreateAsset(string path)
        {
            string directory = Path.GetDirectoryName(path);
            string name = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(name);

            if (extension.Equals(ScriptExtension))
            {
                path = Path.Combine(directory, name);
                string header = File.ReadAllText(TemplatePath);
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
            builder.Replace("#DEVELOPERNAME#", DeveloperName);
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