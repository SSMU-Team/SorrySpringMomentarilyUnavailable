using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using CustomEvents;

using UnityEngine;

public class WalkSound : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_walkName;

	[FMODUnity.ParamRef]
	[SerializeField]
	private string m_springModeWalkName = "SpringMode";

	private EventInstance m_walk;

	private bool m_isInSpring;
	private bool m_isOnLeaf;

	void Start()
	{
		m_walk = FMODUnity.RuntimeManager.CreateInstance(m_walkName);
	}

	public void OnSpring(bool isInSpring)
	{
		m_isInSpring = isInSpring;
	}

	public void OnMove(Move move)
	{
		PLAYBACK_STATE state;
		m_walk.getPlaybackState(out state);
		if(move.is_grounded && move.speed != MoveSpeed.Idle && state == PLAYBACK_STATE.STOPPED)
		{
			m_walk = FMODUnity.RuntimeManager.CreateInstance(m_walkName);
			m_walk.start();
		}
		else if(!move.is_grounded || move.speed == MoveSpeed.Idle)
		{
			m_walk.stop(STOP_MODE.ALLOWFADEOUT);
		}
		m_isInSpring |= SpringManager.Instance.SpringMode == SpringSceneMode.Spring;
		m_walk.setParameterByName(m_springModeWalkName, m_isInSpring ? move.material == MoveMaterial.Leaf ? 2 : 1 : 0);
	}
}
