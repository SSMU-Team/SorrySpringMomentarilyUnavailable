using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private GameObject m_creditMenu;
	[SerializeField] private IntEvent m_loadLevel;

	private void Start()
	{
		m_creditMenu.SetActive(false);
	}

	public void Play()
	{
		m_loadLevel.Invoke(0);
	}

	public void Credit()
	{
		m_creditMenu.SetActive(true);
	}

	public void Quit()
	{
		MenuUtilities.Quit();
	}

	public void BackToMainMenu()
	{
		m_creditMenu.SetActive(false);
	}
}
