using System;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Hierarchy2
{
	[CustomEditor(typeof(HierarchyFolder))]
	internal class HierarchyFolderEditor : Editor
	{
		private void OnEnable()
		{

		}

		public override VisualElement CreateInspectorGUI()
		{
			HierarchyFolder script = target as HierarchyFolder;

			VisualElement root = new VisualElement();

			IMGUIContainer imguiContainer = new IMGUIContainer(() =>
			{
				script.flattenMode = (HierarchyFolder.FlattenMode)EditorGUILayout.EnumPopup("Flatten Mode", script.flattenMode);
				if(script.flattenMode != HierarchyFolder.FlattenMode.None)
				{
					script.flattenSpace = (HierarchyFolder.FlattenSpace)EditorGUILayout.EnumPopup("Flatten Space", script.flattenSpace);
					script.destroyAfterFlatten = EditorGUILayout.Toggle("Destroy After Flatten", script.destroyAfterFlatten);
				}
			});
			root.Add(imguiContainer);

			return root;
		}

		[MenuItem("GameObject/Hierarchy Folder", priority = 0)]
		private static void CreateInstance()
		{
			new GameObject("Folder", new Type[1] { typeof(HierarchyFolder) });
		}
	}
}