using UnityEditor;

using UnityEngine;

namespace CustomEvents
{
	[CustomEditor(typeof(Vector2Event), true), CanEditMultipleObjects]
	public class Vector2EventInspector : Editor
	{
		private Vector2 m_parameter;
		private Vector2Event[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new Vector2Event[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (Vector2Event)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			m_parameter = EditorGUILayout.Vector2Field("Parameter :", m_parameter);
			EditorGUILayout.Separator();
			if(GUILayout.Button("Invoke event"))
			{
				foreach(Vector2Event e in m_objs)
				{
					e.Invoke(m_parameter);
				}
			}
		}
	}
}
