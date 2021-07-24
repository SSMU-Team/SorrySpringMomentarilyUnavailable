using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using SceneManagement;

using UnityEngine;

public class EndLevel : MonoBehaviour
{
	[SerializeField] private SimpleEvent m_endLevelEvent;
	[SerializeField] private SimpleEvent m_fairyEnterAutelEvent;
	public Animation animationPlayer;
	public SimpleEvent loadMainMenu;

	public void OnAutelActive(bool isActive)
	{
		if(isActive)
		{
			m_fairyEnterAutelEvent.Invoke();
			animationPlayer.Play();
		}
	}

	public void OnFairyEnterFinish()
	{
		m_endLevelEvent.Invoke();
	}

	public void OnEndLevelAnimation()
	{
		loadMainMenu.Invoke();
	}
}
