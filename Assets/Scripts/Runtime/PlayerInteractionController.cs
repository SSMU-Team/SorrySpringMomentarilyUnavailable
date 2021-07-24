using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

interface IInteractable
{
	void OnInteraction();
}

public class PlayerInteractionController : MonoBehaviour
{
	private List<IInteractable> m_collectableStack;

	private void Awake()
	{
		m_collectableStack = new List<IInteractable>();
	}

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		IInteractable item = obj.GetComponent<IInteractable>();
		if(item != null)
		{
			m_collectableStack.Add(item);
		}
	}

	public void OnTriggerFilteredExit(GameObject obj)
	{
		IInteractable item = obj.GetComponent<IInteractable>();
		if(item != null && m_collectableStack.Contains(item))
		{
			m_collectableStack.Remove(item);
		}
	}

	public void OnInteraction(CallbackContext ctx)
	{
		if(ctx.performed && m_collectableStack.Count > 0)
		{
			IInteractable item = m_collectableStack[0];
			item.OnInteraction();
		}
	}
}
