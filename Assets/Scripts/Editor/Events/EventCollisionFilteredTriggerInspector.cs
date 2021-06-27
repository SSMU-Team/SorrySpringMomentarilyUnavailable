using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

namespace Utility.Event
{
	[CustomEditor(typeof(EventCollisionFilteredTrigger), true), CanEditMultipleObjects]
	public class EventCollisionFilteredTriggerInspector : Editor
	{
		private EventCollisionFilteredTrigger[] m_objs;

		public void OnEnable()
		{
			//Multi object management
			Object[] target_cast = targets;
			m_objs = new EventCollisionFilteredTrigger[targets.Length];
			int i = 0;
			foreach(Object o in targets)
			{
				m_objs[i] = (EventCollisionFilteredTrigger)o;
				i++;
			}
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			EditorGUILayout.Separator();
			foreach(EventCollisionFilteredTrigger event_collision in m_objs)
			{
				AutoRegisterEvent(event_collision.TriggerFilteredEnter, event_collision, "TriggerFilteredEnter");
				AutoRegisterEvent(event_collision.TriggerFilteredStay, event_collision, "TriggerFilteredStay");
				AutoRegisterEvent(event_collision.TriggerFilteredExit, event_collision, "TriggerFilteredExit");
			}
		}

		private void AutoRegisterEvent(UnityEvent<GameObject> e, EventCollisionFilteredTrigger caller, string name)
		{
			if(GUILayout.Button("Auto-register " + name))
			{
				e.AutoReferenceUnityEvent<GameObject>(caller, name);
			}
		}
	}
}
