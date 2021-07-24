
using TMPro;

using UnityEngine;

public class HUDController : Menu
{
	public UICollectable[] collectables;

	public override void OpenMenu()
	{
		Enabled = true;
	}

	public override void CloseMenu()
	{
		Enabled = false;
	}

	public void OnScriptableTaskUpdate(ScriptableCollectable task)
	{
		foreach(UICollectable t in collectables)
		{
			if(task == t.collectable)
			{
				t.NumberPerformed = task.Number.ToString();
			}
		}
	}
}
