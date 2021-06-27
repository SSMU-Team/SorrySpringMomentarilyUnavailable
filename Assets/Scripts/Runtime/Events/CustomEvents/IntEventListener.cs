using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
	public class IntEventListener : MonoBehaviour
	{
		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private IntEvent m_event;

		[SerializeField]
		private UnityEvent<int> m_func;

#pragma warning restore 0649
		#endregion

		public IntEvent GetEvent => m_event;

		public UnityEvent<int> Func => m_func;

		public void OnEnable()
		{
			GetEvent.AddListener(this);
		}

		public void OnDisable()
		{
			GetEvent.RemoveListener(this);
		}

		public void OnEventInvoke(int parameters)
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