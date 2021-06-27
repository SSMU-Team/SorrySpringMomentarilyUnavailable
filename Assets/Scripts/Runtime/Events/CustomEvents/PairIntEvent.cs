
using UnityEngine;

namespace CustomEvents
{
	public struct PairInt
	{
		public int first;
		public int second;
	}

	public delegate void PairIntEventCallback(PairInt param);

	[CreateAssetMenu(fileName = "PairIntEvent", menuName = "Events/PairIntEvent")]
	public class PairIntEvent : ScriptableEvent
	{
		private PairIntEventCallback m_call;

		public void AddListener(PairIntEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(PairIntEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(PairInt param)
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

		public void RemoveListener(PairIntEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(PairIntEventCallback listener)
		{
			m_call -= listener;
		}
	}
}