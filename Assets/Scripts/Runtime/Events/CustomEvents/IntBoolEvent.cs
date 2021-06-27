
using UnityEngine;

namespace CustomEvents
{
	public struct IntBool
	{
		public int first;
		public bool second;
	}
	public delegate void IntBoolEventCallback(IntBool param);

	[CreateAssetMenu(fileName = "IntBoolEvent", menuName = "Events/IntBoolEvent")]
	public class IntBoolEvent : ScriptableEvent
	{
		private IntBoolEventCallback m_call;

		public void AddListener(IntBoolEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(IntBoolEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(IntBool param)
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

		public void RemoveListener(IntBoolEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(IntBoolEventCallback listener)
		{
			m_call -= listener;
		}
	}
}