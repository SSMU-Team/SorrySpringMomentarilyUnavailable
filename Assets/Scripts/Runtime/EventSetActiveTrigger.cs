using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSetActiveTrigger : MonoBehaviour
{
	[SerializeField] private bool m_default;

	[SerializeField]
	private UnityEvent<GameObject> m_onTriggerEnable;

	[SerializeField]
	private UnityEvent<GameObject> m_onTriggerDisable;

	[SerializeField]

	public UnityEvent<GameObject> TriggerEnable => m_onTriggerEnable;
	public UnityEvent<GameObject> TriggerDisable => m_onTriggerDisable;

	private void Awake()
	{
		gameObject.SetActive(m_default);
	}

	private void OnEnable()
	{
		m_onTriggerEnable.Invoke(gameObject);
	}

	private void OnDisable()
	{
		m_onTriggerDisable.Invoke(gameObject);
	}
}
