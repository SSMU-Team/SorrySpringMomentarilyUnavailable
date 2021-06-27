
using UnityEngine;

namespace CustomEvents
{
	public delegate void IntEventCallback(int param);

	[CreateAssetMenu(fileName = "IntEvent", menuName = "Events/IntEvent")]
	public class IntEvent : ScriptableEvent
	{
		private IntEventCallback m_call;

		public void AddListener(IntEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(IntEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(int param)
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

		public void RemoveListener(IntEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(IntEventCallback listener)
		{
			m_call -= listener;
		}
	}
}