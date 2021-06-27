using System.Collections;
using System.Collections.Generic;
using CustomEvents;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteractionController : MonoBehaviour
{
	[SerializeField] private SimpleEvent m_interactableEvent;

	public void OnInteraction(CallbackContext ctx)
	{
		if(ctx.performed)
		{
			m_interactableEvent.Invoke();
		}
	}
}
