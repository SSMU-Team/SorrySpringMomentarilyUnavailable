using UnityEngine;

namespace CustomEvents
{
	public delegate void FloatEventCallback(float param);

	[CreateAssetMenu(fileName = "FloatEvent", menuName = "Events/FloatEvent")]
	public class FloatEvent : ScriptableEvent
	{
		private FloatEventCallback m_call;

		public void AddListener(FloatEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(FloatEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(float param)
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

		public void RemoveListener(FloatEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(FloatEventCallback listener)
		{
			m_call -= listener;
		}
	}
}