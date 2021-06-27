using UnityEngine;

namespace CustomEvents
{
	public delegate void StringEventCallback(string param);

	[CreateAssetMenu(fileName = "StringEvent", menuName = "Events/StringEvent")]
	public class StringEvent : ScriptableEvent
	{
		private StringEventCallback m_call;

		public void AddListener(StringEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(StringEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(string param)
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

		public void RemoveListener(StringEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(StringEventCallback listener)
		{
			m_call -= listener;
		}
	}
}