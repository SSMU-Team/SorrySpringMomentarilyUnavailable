using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
	public class IntBoolEventListener : MonoBehaviour
	{
		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private IntBoolEvent m_event;

		[SerializeField]
		private UnityEvent<IntBool> m_func;

#pragma warning restore 0649
		#endregion

		public IntBoolEvent GetEvent => m_event;

		public UnityEvent<IntBool> Func => m_func;

		public void OnEnable()
		{
			GetEvent.AddListener(this);
		}

		public void OnDisable()
		{
			GetEvent.RemoveListener(this);
		}

		public void OnEventInvoke(IntBool parameters)
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