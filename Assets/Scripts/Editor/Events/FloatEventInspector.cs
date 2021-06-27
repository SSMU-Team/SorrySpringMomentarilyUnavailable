using UnityEditor;

using UnityEngine;

namespace CustomEvents
{
	[CustomEditor(typeof(FloatEvent), true), CanEditMultipleObjects]
	public class FloatEventInspector : Editor
	{
		private float m_parameter;
		private FloatEvent[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new FloatEvent[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (FloatEvent)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			m_parameter = EditorGUILayout.FloatField("Parameter :", m_parameter);
			EditorGUILayout.Separator();
			if(GUILayout.Button("Invoke event"))
			{
				foreach(FloatEvent e in m_objs)
				{
					e.Invoke(m_parameter);
				}
			}
		}
	}
}
