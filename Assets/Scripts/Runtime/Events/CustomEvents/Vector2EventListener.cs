using UnityEngine;
using UnityEngine.Events;

namespace CustomEvents
{
	public class Vector2EventListener : MonoBehaviour
	{
		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private Vector2Event m_event;

		[SerializeField]
		private UnityEvent<Vector2> m_func;

#pragma warning restore 0649
		#endregion

		public Vector2Event GetEvent => m_event;

		public UnityEvent<Vector2> Func => m_func;

		public void OnEnable()
		{
			GetEvent.AddListener(this);
		}

		public void OnDisable()
		{
			GetEvent.RemoveListener(this);
		}

		public void OnEventInvoke(Vector2 parameters)
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