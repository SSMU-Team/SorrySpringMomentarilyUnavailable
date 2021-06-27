using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
	public class MoveEventListener : MonoBehaviour
	{
		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private MoveEvent m_event;

		[SerializeField]
		private UnityEvent<Move> m_func;

#pragma warning restore 0649
		#endregion

		public MoveEvent GetEvent => m_event;

		public UnityEvent<Move> Func { get => m_func; }

		public void OnEnable()
		{
			GetEvent.AddListener(this);
		}

		public void OnDisable()
		{
			GetEvent.RemoveListener(this);
		}

		public void OnEventInvoke(Move parameters)
		{
#if EVENT_DEBUG
            DebugInvoke();
#endif
			m_func.Invoke(parameters);
		}

		public void DebugInvoke()
		{
			Debug.Log("Invoked " + m_event.name + " in " + gameObject.name, gameObject);
		}
	}
}