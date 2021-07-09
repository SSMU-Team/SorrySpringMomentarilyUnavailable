using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

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

	protected void Awake()
	{
		Enabled = false;
	}
}
