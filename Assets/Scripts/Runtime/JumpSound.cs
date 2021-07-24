using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using CustomEvents;

using UnityEngine;

public class JumpSound : MonoBehaviour
{	
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_jumpName;

	private EventInstance m_jump;

	private void OnEnable()
	{
		m_jump = FMODUnity.RuntimeManager.CreateInstance(m_jumpName);
	}

	private void OnDisable()
	{
		m_jump.release();
	}

	public void OnJump()
	{
		m_jump.start();
	}
}
