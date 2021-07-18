using System.Collections.Generic;

using UnityEngine;

namespace CustomEvents
{
	public delegate void CollectableEventCallback(ScriptableCollectable param);

	[CreateAssetMenu(fileName = "CollectableEvent", menuName = "Events/CollectableEvent")]
	public class CollectableEvent : ScriptableEvent
	{
		private CollectableEventCallback m_call;

		public void AddListener(CollectableEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(CollectableEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(ScriptableCollectable param)
		{
			if(m_call != null)
			{
				m_call(param);
			}
			else
			{
				Debug.LogWarning("No event registered on " + name);
			}
		}

		public void RemoveListener(CollectableEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(CollectableEventCallback listener)
		{
			m_call -= listener;
		}
	}
}