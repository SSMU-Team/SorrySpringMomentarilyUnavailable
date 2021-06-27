
using UnityEditor;

using UnityEngine;

namespace CustomEvents
{
	[CustomEditor(typeof(StringEvent), true), CanEditMultipleObjects]
	public class StringEventInspector : Editor
	{
		private string m_parameter;
		private StringEvent[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new StringEvent[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (StringEvent)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			m_parameter = EditorGUILayout.TextField("Parameter :", m_parameter);
			EditorGUILayout.Separator();

			if(!Application.isPlaying)
			{
				GUI.enabled = false;
			}

			if(GUILayout.Button("Invoke event"))
			{
				foreach(StringEvent e in m_objs)
				{
					e.Invoke(m_parameter);
				}
			}

			GUI.enabled = true;
		}
	}
}
