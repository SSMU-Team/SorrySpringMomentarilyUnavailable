using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using CustomEvents;

using UnityEngine;

public class MovementsSound : MonoBehaviour
{
	[FMODUnity.EventRef]
	[SerializeField]
	private string m_walkName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_jumpName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_landName;

	[FMODUnity.ParamRef]
	[SerializeField]
	private string m_springModeWalkName = "SpringMode";

	private EventInstance m_walk;
	private EventInstance m_jump;
	private EventInstance m_land;

	private bool m_isInSpring;
	private bool m_isOnLeaf;

	void Start()
	{
		m_walk = FMODUnity.RuntimeManager.CreateInstance(m_walkName);
		m_jump = FMODUnity.RuntimeManager.CreateInstance(m_jumpName);
		m_land = FMODUnity.RuntimeManager.CreateInstance(m_landName);
		m_jump.setParameterByName(m_springModeWalkName, 0);
		m_land.setParameterByName(m_springModeWalkName, 0);
	}

	public void OnJump()
	{
		PLAYBACK_STATE state;
		m_jump.getPlaybackState(out state);
		if(state == PLAYBACK_STATE.STOPPED)
		{
			m_jump.start();
		}
	}


	public void OnLand()
	{
		m_land.start();
	}

	public void OnSpring(bool isInSpring)
	{
		m_isInSpring = isInSpring;
		m_jump.setParameterByName(m_springModeWalkName, m_isInSpring ? 1 : 0);
		m_land.setParameterByName(m_springModeWalkName, m_isInSpring ? 1 : 0);
	}

	public void OnMove(Move move)
	{
		PLAYBACK_STATE state;
		m_walk.getPlaybackState(out state);
		if(move.is_grounded && move.speed != MoveSpeed.Idle && state == PLAYBACK_STATE.STOPPED)
		{
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
