
using UnityEditor;

using UnityEngine;


namespace Utility
{
	/// <summary>
	/// Static class containining utilities functions for editor.
	/// </summary>
	public static class FuncEditor
	{
		/// <summary>
		/// Ui funtions to draw separator in Editors.
		/// </summary>
		/// <param name="color"> The color of the separation</param>
		/// <param name="thickness">The thickness.</param>
		/// <param name="padding"> The padding.</param>
		public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
		{
			Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
			r.height = thickness;
			r.y += padding / 2;
			r.x -= 2;
			r.width += 6;
			EditorGUI.DrawRect(r, color);
		}

		/// <summary>
		/// Test if the given object is an instance of prefab.
		/// </summary>
		public static bool IsGameObjectInstancePrefab(UnityEngine.Object obj)
		{
			PrefabAssetType type = PrefabUtility.GetPrefabAssetType(obj);
			return type == PrefabAssetType.Regular && PrefabUtility.IsPartOfNonAssetPrefabInstance(obj);
		}

		/// <summary>
		/// Test if the given object is an instantiated object in scene view.
		/// </summary>
		public static bool IsGameObjectSceneView(UnityEngine.Object obj)
		{
			PrefabAssetType type = PrefabUtility.GetPrefabAssetType(obj);
			return IsGameObjectInstancePrefab(obj) || type == PrefabAssetType.NotAPrefab;
		}

		/// <summary>
		/// Get the prefab from an instance.
		/// </summary>
		/// <param name="prefabInstance"> The instance of a prefab.</param>
		/// <returns>The prefab, or null if not founded.</returns>
		public static GameObject GetPrefabFromInstance(GameObject prefabInstance)
		{
			if(IsGameObjectInstancePrefab(prefabInstance))
			{
				return PrefabUtility.GetCorrespondingObjectFromSource(prefabInstance);
			}
			return null;
		}
	}

}

