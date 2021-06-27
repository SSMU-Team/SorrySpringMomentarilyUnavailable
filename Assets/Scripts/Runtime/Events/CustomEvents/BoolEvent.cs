using UnityEngine;

namespace CustomEvents
{
	public delegate void BoolEventCallback(bool param);

	[CreateAssetMenu(fileName = "BoolEvent", menuName = "Events/BoolEvent")]
	public class BoolEvent : ScriptableEvent
	{
		private BoolEventCallback m_call;

		public void AddListener(BoolEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(BoolEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(bool param)
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

		public void RemoveListener(BoolEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(BoolEventCallback listener)
		{
			m_call -= listener;
		}
	}
}
