using UnityEditor;

using UnityEngine;

using System.IO;

public static class BrushUtilities
{
	static string m_path_brush_pattern = "Assets/Editor Default Resources/BrushPattern.brush";
	static string m_dest_brushes = "Assets/Data/Brushes";


	[MenuItem("Assets/Create/CreateBrush", true, 1100)]
	static bool ValidateCreateBrush(MenuCommand command)
	{
		foreach(string guid in Selection.assetGUIDs)
		{
			string asset_path = AssetDatabase.GUIDToAssetPath(guid);
			Object base_obj = AssetDatabase.LoadAssetAtPath(asset_path, typeof(Texture));
			if(base_obj == null)
				return false;
		}
		return true;
	}

	[MenuItem("Assets/Create/CreateBrush", false, 1100)]
	static void CreateBrush(MenuCommand command)
	{
		Debug.Log("Creating brush selected :" + Selection.assetGUIDs.Length);
		foreach(string guid in Selection.assetGUIDs)
		{
			string asset_path = AssetDatabase.GUIDToAssetPath(guid);
			Texture texture = AssetDatabase.LoadAssetAtPath(asset_path, typeof(Texture)) as Texture;
			long file_id;
			if(AssetDatabase.TryGetGUIDAndLocalFileIdentifier(texture, out _, out file_id))
			{
				string new_path = m_dest_brushes + "\\brush_" + texture.name + ".brush";
				if(AssetDatabase.CopyAsset(m_path_brush_pattern, new_path))
				{
					string replace_str_name = string.Format("m_Name: {0}", "brush_" + texture.name);
					string replace_str_mask = string.Format("m_Mask: {{ fileID: {0}, guid: {1}, type: 3}}", file_id, guid);

					string content = File.ReadAllText(new_path);
					content = content.Replace("m_Name: BrushPattern", replace_str_name);
					content = content.Replace("m_Mask: {fileID: 0}", replace_str_mask);
					File.WriteAllText(new_path, content);
					AssetDatabase.ImportAsset(new_path);
				}
				else
				{
					Debug.LogError("Can't copy in " + new_path);
				}
			}
			else
			{
				Debug.LogError("Can't get file id for " + texture.name);
			}
		}
	}
}
