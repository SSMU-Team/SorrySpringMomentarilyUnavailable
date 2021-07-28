using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEvents;
using FMOD.Studio;

public class SoundThemeManager : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_themeSpringName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_themeWinterName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_themeVictoryName;

	private EventInstance m_themeSpring;
	private EventInstance m_themeWinter;
	private EventInstance m_themeVictory;

	void Start()
	{
		m_themeSpring = FMODUnity.RuntimeManager.CreateInstance(m_themeSpringName);
		m_themeWinter = FMODUnity.RuntimeManager.CreateInstance(m_themeWinterName);
		m_themeVictory = FMODUnity.RuntimeManager.CreateInstance(m_themeVictoryName);
		m_themeSpring.start();
	}

	private void OnDestroy()
	{
		m_themeSpring.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeWinter.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeVictory.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeSpring.release();
		m_themeWinter.release();
		m_themeVictory.release();
	}

	public void OnLoadLevel(int level)
	{
		m_themeSpring.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeWinter.start();
	}

	public void OnEndLevel()
	{
		m_themeSpring.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeWinter.stop(STOP_MODE.ALLOWFADEOUT);
		m_themeVictory.start();
		Debug.Log("Sound Theme : Victory");
	}
}
