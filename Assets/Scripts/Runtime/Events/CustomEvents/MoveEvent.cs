
using UnityEngine;

namespace CustomEvents
{
	public enum MoveSpeed
	{
		Idle,
		Walk,
		Run
	};

	public enum MoveMaterial
	{
		Leaf,
		Grass,
	};

	public struct Move
	{
		public bool is_grounded;
		public MoveMaterial material;
		public MoveSpeed speed;
	}


	public delegate void MoveEventCallback(Move param);

	[CreateAssetMenu(fileName = "MoveEvent", menuName = "Events/MoveEvent")]
	public class MoveEvent : ScriptableEvent
	{
		private MoveEventCallback m_call;

		public void AddListener(MoveEventListener listener)
		{
			m_call += listener.OnEventInvoke;
		}

		public void AddListener(MoveEventCallback listener)
		{
			m_call += listener;
		}

		public void Invoke(Move param)
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

		public void RemoveListener(MoveEventListener listener)
		{
			m_call -= listener.OnEventInvoke;
		}

		public void RemoveListener(MoveEventCallback listener)
		{
			m_call -= listener;
		}
	}
}