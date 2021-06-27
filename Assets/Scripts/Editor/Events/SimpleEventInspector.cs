
using UnityEditor;

using UnityEngine;


namespace CustomEvents
{
	[CustomEditor(typeof(SimpleEvent), true), CanEditMultipleObjects]
	public class SimpleEventInspector : Editor
	{
		private SimpleEvent[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new SimpleEvent[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (SimpleEvent)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			EditorGUILayout.Separator();
			if(!Application.isPlaying)
			{
				GUI.enabled = false;
			}
			if(GUILayout.Button("Invoke event"))
			{
				foreach(SimpleEvent e in m_objs)
				{
					e.Invoke();
				}
			}

			GUI.enabled = true;
		}
	}
}
