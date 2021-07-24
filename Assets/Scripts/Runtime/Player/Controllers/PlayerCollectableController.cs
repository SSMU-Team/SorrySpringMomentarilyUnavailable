using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

public class PlayerCollectableController : MonoBehaviour
{
	private List<Collectable> m_collectableStack;

	private void Awake()
	{
		m_collectableStack = new List<Collectable>();
	}

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		Collectable collectable = obj.GetComponent<Collectable>();
		if(collectable != null)
		{
			m_collectableStack.Add(collectable);
			if(collectable.IsAutoCollect)
			{
				OnInteraction();
			}
		}
	}

	public void OnTriggerFilteredExit(GameObject obj)
	{
		Collectable collectable = obj.GetComponent<Collectable>();
		if(collectable != null)
		{
			if(m_collectableStack.Contains(collectable))
			{
				m_collectableStack.Remove(collectable);
			}
		}
	}

	public void OnInteraction()
	{
		if(m_collectableStack.Count > 0)
		{
			Collectable item = m_collectableStack[0];
			if(item.OnCollect())
			{
				m_collectableStack.RemoveAt(0);
			}
		}
	}
}
