using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using SceneManagement;

using UnityEngine;

public class EndLevel : MonoBehaviour
{
	public Animation animationPlayer;
	public SimpleEvent loadMainMenu;

	public void OnEndLevel()
	{
		animationPlayer.Play();
	}

	public void OnEndLevelAnimation()
	{
		loadMainMenu.Invoke();
	}
}
