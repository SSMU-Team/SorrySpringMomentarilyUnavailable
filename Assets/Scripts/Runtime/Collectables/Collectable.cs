using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Collectable : MonoBehaviour
{
	[SerializeField] private ScriptableCollectable m_collectable;
	[SerializeField] private CollectableEvent m_collectableEvent;
	[SerializeField] private bool m_isAutoCollect = false;
	[SerializeField] bool m_canCollect = false;

	public bool IsAutoCollect => m_isAutoCollect;

	private void Start()
	{
		if(m_collectable.can_be_reset)
			m_collectable.Reset();
	}

	public bool OnCollect()
	{
		if(m_canCollect)
		{
			m_collectable.Number++;
			Destroy(gameObject);
			return true;
		}
		return false;
	}

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		m_canCollect = true;
	}

	public void OnTriggerFilteredExit(GameObject obj)
	{

	}

	[ContextMenu("Update collectable")]
	public void UpdateCollectable()
	{
		m_collectableEvent.Invoke(m_collectable);
	}
}
