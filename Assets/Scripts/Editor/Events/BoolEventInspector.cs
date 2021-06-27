
using UnityEditor;

using UnityEngine;


namespace CustomEvents
{
	[CustomEditor(typeof(BoolEvent), true), CanEditMultipleObjects]
	public class BoolEventInspector : Editor
	{
		private bool m_parameter;
		private BoolEvent[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new BoolEvent[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (BoolEvent)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			m_parameter = EditorGUILayout.Toggle("Parameter :", m_parameter);
			EditorGUILayout.Separator();
			if(!Application.isPlaying)
			{
				GUI.enabled = false;
			}
			if(GUILayout.Button("Invoke event"))
			{
				foreach(BoolEvent e in m_objs)
				{
					e.Invoke(m_parameter);
				}
			}

			GUI.enabled = true;
		}
	}
}
