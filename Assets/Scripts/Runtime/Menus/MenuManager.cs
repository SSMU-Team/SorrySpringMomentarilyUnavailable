using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

public enum MenuType : int
{
	None = -1,
	Pause = 0,
	Inventory = 1,
	TaskList = 2
}

[System.Serializable]
public class MenuData
{
	[SerializeField] private Menu m_menu;
	public bool canBeOverrided;

	public MenuType Type => m_menu.Type;
	public bool Enabled => m_menu.Enabled;
	public void OpenMenu() => m_menu.OpenMenu();
	public void CloseMenu() => m_menu.CloseMenu();
}

public class MenuManager : MonoBehaviour
{
	[SerializeField] private MenuTypeEvent m_menuEvent;
	[SerializeField] private MenuData[] m_menus;

	private MenuData m_currentMenu;
	private Dictionary<MenuType, MenuData> m_menusDict;

	private void Awake()
	{
		m_menuEvent.AddListener(OnMenu);
		m_currentMenu = null;

		m_menusDict = new Dictionary<MenuType, MenuData>();
		foreach(MenuData data in m_menus)
		{
			m_menusDict.Add(data.Type, data);
		}
	}

	private void OnMenu(MenuType type)
	{
		if(m_currentMenu == null) // Ouverture d'un 1er menu
		{
			m_currentMenu = m_menusDict[type];
			m_currentMenu.OpenMenu();
		}
		else // Un menu est déjà ouvert
		{
			if(type == m_currentMenu.Type) // Fermeture du menu actuel
			{
				m_currentMenu.CloseMenu();
				m_currentMenu = null;
			}
			else
			{
				if(m_currentMenu.canBeOverrided) // Ouverture d'un autre menu alors qu'il y a déjà un menu d'ouvert
				{
					m_currentMenu.CloseMenu();
					m_currentMenu = m_menusDict[type];
					m_currentMenu.OpenMenu();
				}
			}
			
		}
	}
}
