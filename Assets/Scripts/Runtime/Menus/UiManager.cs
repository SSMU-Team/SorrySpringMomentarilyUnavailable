using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

[System.Serializable]
public class UiData
{
	[SerializeField] private Menu m_menu;
	public bool canBeOverrided;

	public MenuType Type => m_menu.Type;
	public bool Enabled => m_menu.Enabled;
	public void OpenMenu() => m_menu.OpenMenu();
	public void CloseMenu() => m_menu.CloseMenu();
}

public class UiManager : MonoBehaviour
{
	[SerializeField] private UiData[] m_menus;

	private UiData m_currentUi;
	private Dictionary<MenuType, UiData> m_menusDict;

	private void Awake()
	{
		m_currentUi = null;

		m_menusDict = new Dictionary<MenuType, UiData>();
		foreach(UiData data in m_menus)
		{
			m_menusDict.Add(data.Type, data);
		}
	}

	public void OnMenu(MenuType type)
	{
		if(m_currentUi == null) // Ouverture d'un 1er menu
		{
			m_currentUi = m_menusDict[type];
			m_currentUi.OpenMenu();
		}
		else // Un menu est déjà ouvert
		{
			if(type == m_currentUi.Type) // Fermeture du menu actuel
			{
				m_currentUi.CloseMenu();
				m_currentUi = null;
			}
			else
			{
				if(m_currentUi.canBeOverrided) // Ouverture d'un autre menu alors qu'il y a déjà un menu d'ouvert
				{
					m_currentUi.CloseMenu();
					m_currentUi = m_menusDict[type];
					m_currentUi.OpenMenu();
				}
			}

		}
	}
}
