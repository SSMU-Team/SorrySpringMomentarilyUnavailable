using UnityEngine;

namespace CustomEvents
{
	public delegate void SimpleEventCallback();

	[CreateAssetMenu(fileName = "SimpleEvent", menuName = "Events/SimpleEvent")]
	public class SimpleEvent : ScriptableEvent
	{
		private SimpleEventCallback m_call;

		public void AddListener(SimpleEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(SimpleEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke()
		{
			if(m_call != null)
			{
				m_call();
			}
			else
			{
				Debug.LogWarning("No event registered on " + name);
			}
		}

		public void RemoveListener(SimpleEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(SimpleEventCallback listener)
		{
			m_call -= listener;
		}
	}
}