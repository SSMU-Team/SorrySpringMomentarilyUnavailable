using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
	public class SimpleEventListener : MonoBehaviour
	{

#pragma warning disable 0649

		[SerializeField]
		private SimpleEvent m_event;

		[SerializeField]
		private UnityEvent m_func;

#pragma warning restore 0649

		public SimpleEvent GetEvent => m_event;

		public UnityEvent Func => m_func;

		public void OnEnable()
		{
			GetEvent.AddListener(this);
		}

		public void OnDisable()
		{
			GetEvent.RemoveListener(this);
		}

		public void OnEventInvoke()
		{
#if EVENT_DEBUG
            DebugInvoke();
#endif
			m_func.Invoke();
		}

		public void DebugInvoke()
		{
			Debug.Log("Invoked " + m_event.name + " in " + gameObject.name, gameObject);
		}
	}
}
