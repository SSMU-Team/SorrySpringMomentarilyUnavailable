using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;
using UnityEngine.Events;

public class Collectable: MonoBehaviour
{
	[SerializeField] private ScriptableCollectable m_collectable;
	[SerializeField] private ScriptableTask m_task;

	public bool OnCollect()
	{
		if(m_collectable.CanCollect())
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
}
