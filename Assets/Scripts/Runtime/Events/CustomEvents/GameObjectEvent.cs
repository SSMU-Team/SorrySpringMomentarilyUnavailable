using UnityEngine;

namespace CustomEvents
{
	public delegate void GameObjectEventCallback(GameObject param);

	[CreateAssetMenu(fileName = "GameObjectEvent", menuName = "Events/GameObjectEvent")]
	public class GameObjectEvent : ScriptableEvent
	{
		private GameObjectEventCallback m_call;

		public void AddListener(GameObjectEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(GameObjectEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(GameObject param)
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

		public void RemoveListener(GameObjectEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(GameObjectEventCallback listener)
		{
			m_call -= listener;
		}
	}
}