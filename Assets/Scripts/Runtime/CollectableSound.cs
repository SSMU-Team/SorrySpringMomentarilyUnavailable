using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using CustomEvents;

using UnityEngine;

public class CollectableSound : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_objectFoundName;

	private EventInstance m_objectFound;

	private bool m_soundPlayed = false;

	private void OnEnable()
	{
		m_objectFound = FMODUnity.RuntimeManager.CreateInstance(m_objectFoundName);
	}

	private void OnDisable()
	{
		m_objectFound.release();
	}

	public void OnObjectFound()
	{
		if(!m_soundPlayed)
		{
			m_objectFound.start();
			m_soundPlayed = true;
		}
	}
}
