using UnityEditor;

public class CustomTemplate : UnityEditor.AssetModificationProcessor
{
    private const string developerName = "Onur Tanrıkulu";

    public static void OnWillCreateAsset(string path)
    {
        string file = ".cs";
        
        if (!path.Contains(file)) return;

        //Removes .meta extension from file path
        path = path.Remove(path.Length - 4);
       
        file = System.IO.File.ReadAllText(path);
        file = file.Replace("#DATE#", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        file = file.Replace("#YEAR#", System.DateTime.Now.Year.ToString());
        file = file.Replace("#PRODUCTNAME#", PlayerSettings.productName);
        file = file.Replace("#DEVELOPERNAME#", developerName);
        file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}
