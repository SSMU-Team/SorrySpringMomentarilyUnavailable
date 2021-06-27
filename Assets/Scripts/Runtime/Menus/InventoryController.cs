using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class InventoryController : Menu
{
	[SerializeField] private ScriptableInventory m_inventory;

	public override void OpenMenu()
	{
		Enabled = true;
	}
	
	public override void CloseMenu()
	{
		Enabled = false;
	}

	public void OnInventory(CallbackContext ctx)
	{
		if(ctx.performed)
		{
			menuEvent.Invoke(type);
		}
	}
}
