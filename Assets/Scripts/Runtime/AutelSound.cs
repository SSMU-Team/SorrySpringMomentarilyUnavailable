using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AutelSound : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_autelVictoryName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_autelWrongName;

	private EventInstance m_autelVictory;
	private EventInstance m_autelWrong;

	private void OnEnable()
	{
		m_autelVictory = FMODUnity.RuntimeManager.CreateInstance(m_autelVictoryName);
		m_autelWrong = FMODUnity.RuntimeManager.CreateInstance(m_autelWrongName);
	}

	private void OnDisable()
	{
		m_autelVictory.release();
		m_autelWrong.release();
	}

	public void OnInteraction(bool isFull)
	{
		if(isFull)
		{
			m_autelVictory.start();
		}
		else
		{
			m_autelWrong.start();
		}
	}
}
