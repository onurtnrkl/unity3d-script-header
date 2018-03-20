using System;
using System.IO;
using UnityEditor;

internal sealed class ScriptTemplate : UnityEditor.AssetModificationProcessor
{
    private const string developerName = "Onur Tanrıkulu";
    private const string scriptExtension = ".cs";

    public static void OnWillCreateAsset(string path)
    {
        string directory = Path.GetDirectoryName(path);
        string name = Path.GetFileNameWithoutExtension(path);
        string extension = Path.GetExtension(name);

        if (extension.Equals(scriptExtension))
        {
            path = Path.Combine(directory, name);
            string file = File.ReadAllText(path);

            ReplaceFile(path, file);
            //AssetDatabase.Refresh();
        }        
    }

    private static void ReplaceFile(string path, string file)
    {
        file = file.Replace("#DATE#", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        file = file.Replace("#YEAR#", DateTime.Now.Year.ToString());
        file = file.Replace("#PRODUCTNAME#", PlayerSettings.productName);
        file = file.Replace("#DEVELOPERNAME#", developerName);
        file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);

        File.WriteAllText(path, file);
    }
}