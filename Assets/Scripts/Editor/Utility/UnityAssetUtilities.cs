using UnityEditor;

using UnityEngine;

public static class UnityAssetUtilities
{
	[MenuItem("Assets/DisplayAssetsInfo")]
	static void DisplatAssetsInfos(MenuCommand command)
	{
		foreach(string guid in Selection.assetGUIDs)
		{
			string asset_path = AssetDatabase.GUIDToAssetPath(guid);
			long file;
			Object obj = AssetDatabase.LoadMainAssetAtPath(asset_path);
			if(AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out _, out file))
			{
				Debug.Log("Asset: " + obj.name + "\n"
					+ "Path: " + asset_path + "\n"
					+ "Type: " + obj.GetType() + "\n"
					+ "Instance ID: " + obj.GetInstanceID() + "\n"
					+ "GUID: " + guid + "\n"
					+ "File ID: " + file);
			}
		}
	}
}
