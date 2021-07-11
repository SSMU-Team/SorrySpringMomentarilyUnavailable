
using CustomEvents;

using FMOD.Studio;

using UnityEngine;

public class MusicController : MonoBehaviour
{
	[Header("FMOD")]

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_themeName;

	[FMODUnity.EventRef]
	[SerializeField]
	private string m_walkName;

	[FMODUnity.ParamRef]
	[SerializeField]
	private string m_springModeWalkName = "SpringMode";

	private EventInstance m_walk;
	private EventInstance m_theme;

	private bool m_is_in_spring;
	void Start()
	{
		m_theme = FMODUnity.RuntimeManager.CreateInstance(m_themeName);
		m_theme.start();
	}

	private void OnDisable()
	{
		m_walk.release();
		m_theme.release();
	}

	public void OnPlayerSpringCollision(bool isInSpring)
	{
		m_is_in_spring = isInSpring || SpringManager.Instance.SpringMode == SpringSceneMode.Spring;
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
		m_is_in_spring |= SpringManager.Instance.SpringMode == SpringSceneMode.Spring;
		m_walk.setParameterByName(m_springModeWalkName, m_is_in_spring ? 0 : 1);
	}
}
