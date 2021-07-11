
using TMPro;

using UnityEngine;

public class HUDController : Menu
{

	public override void OpenMenu()
	{
		Enabled = true;
	}

	public override void CloseMenu()
	{
		Enabled = false;
	}
}
