using System.IO;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;


namespace Utility
{
	public class ScriptTemplateCreator : EditorWindow
	{
		private Vector2 m_scroll;
		private string m_template_folder;
		private int m_priority = 82;
		private string m_hierarchy = "CustomScript";
		private string m_name_template;
		private string m_str_code =
@"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class #SCRIPTNAME# : MonoBehaviour
{
   
}
";


		[MenuItem("Tools/ScriptTemplateCreator")]
		public static void OpenWindows()
		{
			ScriptTemplateCreator window = (ScriptTemplateCreator)GetWindow(typeof(ScriptTemplateCreator));
			window.Show();
		}

		private void OnEnable()
		{
			m_template_folder = Application.dataPath + "\\ScriptTemplates\\";
			Directory.CreateDirectory(m_template_folder);
		}

		public void OnGUI()
		{
			m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
			EditorGUILayout.LabelField("Code template : ");
			m_priority = EditorGUILayout.IntField("Number panel :", m_priority);
			m_hierarchy = EditorGUILayout.TextField("Hierarchy :", m_hierarchy);
			m_name_template = EditorGUILayout.TextField("Name :", m_name_template);
			m_str_code = EditorGUILayout.TextArea(m_str_code);
			if(GUILayout.Button("Create script template"))
			{
				Regex pattern = new Regex("[./\\,;]");
				string hierarchytemplate = pattern.Replace(m_hierarchy, "__");
				File.WriteAllText(m_template_folder + m_priority + "-" + hierarchytemplate + " __New " + m_name_template + " -" + m_name_template + ".cs.txt", m_str_code);
				AssetDatabase.Refresh();
			}
			EditorGUILayout.EndScrollView();

			EditorGUILayout.LabelField("Restart Unity when templates are created.");
		}
	}
}