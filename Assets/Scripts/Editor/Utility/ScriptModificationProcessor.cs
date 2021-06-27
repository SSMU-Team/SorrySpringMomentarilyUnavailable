using System.IO;

using UnityEditor;

using UnityEngine;

/// <summary>
/// Based on https://gist.github.com/wcoastsands/e2c0ee835ae1eb645bf0
/// </summary>
public class ScriptModificationProcessor : UnityEditor.AssetModificationProcessor
{
	public static void OnWillCreateAsset(string path)
	{
		path = path.Replace(".meta", "");
		int index = path.LastIndexOf(".");
		if(index != -1)
		{
			string file = path.Substring(index);
			ScriptCreation(path, index, file);
		}
	}

	private static void ScriptCreation(string path, int index, string file)
	{
		if(file != ".cs" && file != ".js" && file != ".boo")
		{
			return;
		}

		string filename = Path.GetFileName(path);

		index = Application.dataPath.LastIndexOf("Assets");
		path = Application.dataPath.Substring(0, index) + path;
		file = System.IO.File.ReadAllText(path);

		if(filename.Contains("Inspector"))
		{
			index = filename.LastIndexOf("Inspector");
			string object_name = filename.Substring(0, index);
			file = file.Replace("#OBJECT_INSPECTED#", object_name);
		}
		if(filename.Contains("Listener"))
		{
			index = filename.LastIndexOf("Listener");
			string event_name = filename.Substring(0, index);
			file = file.Replace("#EVENTNAME#", event_name);
		}

		if(filename.Contains("Event"))
		{
			index = filename.LastIndexOf("Event");
			string type = filename.Substring(0, index);
			file = file.Replace("#TYPE#", type);
		}

		file = file.Replace("#COMPANYNAMESPACE#", PlayerSettings.companyName);
		file = file.Replace("#PRODUCTNAMESPACE#", PlayerSettings.productName);

		System.IO.File.WriteAllText(path, file);
		AssetDatabase.Refresh();
	}
}
