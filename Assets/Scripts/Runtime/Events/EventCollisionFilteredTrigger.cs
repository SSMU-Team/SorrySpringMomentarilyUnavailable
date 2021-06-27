using UnityEngine;
using UnityEngine.Events;

namespace Utility.Event
{

	/// <summary>
	/// Handle trigger detection and invoke events in response.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public class EventCollisionFilteredTrigger : MonoBehaviour
	{
		#region Inspector
#pragma warning disable 0649

		[SerializeField]
		private LayerMask m_layers_detected;

#pragma warning restore 0649
		#endregion

		#region LocalEvents
#pragma warning disable 0649
		[Header("Events")]

		[SerializeField]
		private UnityEvent<GameObject> m_onTriggerFilteredEnter;

		[SerializeField]
		private UnityEvent<GameObject> m_onTriggerFilteredStay;

		[SerializeField]
		private UnityEvent<GameObject> m_onTriggerFilteredExit;

		public UnityEvent<GameObject> TriggerFilteredEnter => m_onTriggerFilteredEnter;
		public UnityEvent<GameObject> TriggerFilteredStay => m_onTriggerFilteredStay;
		public UnityEvent<GameObject> TriggerFilteredExit => m_onTriggerFilteredExit;

#pragma warning restore 0649
		#endregion

		private void Start()
		{
			GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if(m_layers_detected.HaveLayer(other.gameObject.layer))
			{
				m_onTriggerFilteredEnter.Invoke(other.gameObject);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(m_layers_detected.HaveLayer(other.gameObject.layer))
			{
				m_onTriggerFilteredExit.Invoke(other.gameObject);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if(m_layers_detected.HaveLayer(other.gameObject.layer))
			{
				m_onTriggerFilteredStay.Invoke(other.gameObject);
			}
		}
	}

}