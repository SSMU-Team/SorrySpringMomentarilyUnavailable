using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Collectable : MonoBehaviour
{
	[SerializeField] private ScriptableCollectable m_collectable;
	[SerializeField] private ScriptableTask m_task;
	[SerializeField] private bool m_isAutoCollect = false;

	public bool IsAutoCollect => m_isAutoCollect;

	private bool m_canCollect = false;

	public bool OnCollect()
	{
		if(m_canCollect)
		{
			m_collectable.Number++;
			if(m_task != null)
			{
				m_task.NumberPerformed++;
			}
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
		//m_canCollect = false;
	}
}
