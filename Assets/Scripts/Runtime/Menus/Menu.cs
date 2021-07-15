using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

public enum MenuType : int
{
	HUD = 0,
	Pause = 1,
	Inventory = 2,
}

public abstract class Menu : MonoBehaviour
{
	[SerializeField] private Canvas m_canvas;
	[SerializeField] protected MenuTypeEvent menuEvent;
	[SerializeField] protected MenuType type;

	public bool Enabled
	{
		get
		{
			return m_canvas.enabled;
		}

		protected set
		{
			m_canvas.enabled = value;
		}
	}

	public MenuType Type => type;

	public abstract void OpenMenu();
	public abstract void CloseMenu();

}
