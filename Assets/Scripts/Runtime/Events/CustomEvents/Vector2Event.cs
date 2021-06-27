using UnityEngine;

namespace CustomEvents
{
	public delegate void Vector2EventCallback(Vector2 param);

	[CreateAssetMenu(fileName = "Vector2Event", menuName = "Events/Vector2Event")]
	public class Vector2Event : ScriptableEvent
	{
		private Vector2EventCallback m_call;

		public void AddListener(Vector2EventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(Vector2EventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(Vector2 param)
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

		public void RemoveListener(Vector2EventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(Vector2EventCallback listener)
		{
			m_call -= listener;
		}
	}
}