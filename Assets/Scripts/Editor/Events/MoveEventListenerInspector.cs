using UnityEditor;

using UnityEngine;

using Utility;

namespace CustomEvents
{
	[CustomEditor(typeof(MoveEventListener), true), CanEditMultipleObjects]
	public class MoveEventListenerInspector : Editor
	{
		private string m_name_event;
		private MoveEventListener[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new MoveEventListener[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (MoveEventListener)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			EditorGUILayout.Separator();
			string newname = "";
			if(m_objs[0].GetEvent != null)
			{
				newname = m_objs[0].GetEvent.NameEvent;
			}
			GUI.enabled = false;
			m_name_event = EditorGUILayout.TextField("Name event :", newname);
			if(m_objs[0].GetEvent != null)
				GUI.enabled = true;
			if(GUILayout.Button("Auto-register"))
			{
				foreach(MoveEventListener e in m_objs)
				{
					e.Func.AutoReferenceUnityEvent(e, m_name_event);
				}
			}
			if(m_objs[0].GetEvent == null)
				GUI.enabled = true;
		}
	}
}
