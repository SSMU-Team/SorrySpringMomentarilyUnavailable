using UnityEditor;

using UnityEngine;

namespace CustomEvents
{
	[CustomEditor(typeof(IntEvent), true), CanEditMultipleObjects]
	public class IntEventInspector : Editor
	{
		private int m_parameter;
		private IntEvent[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new IntEvent[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (IntEvent)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			m_parameter = EditorGUILayout.IntField("Parameter :", m_parameter);
			EditorGUILayout.Separator();
			if(GUILayout.Button("Invoke event"))
			{
				foreach(IntEvent e in m_objs)
				{
					e.Invoke(m_parameter);
				}
			}
		}
	}
}
