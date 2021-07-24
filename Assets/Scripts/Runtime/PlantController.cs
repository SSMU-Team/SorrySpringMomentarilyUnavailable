using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
	public enum State
	{
		InGround,
		GrowthInProgress,
		GrowthCompleted,
		Dying
	}

	[SerializeField] [Range(0f, 10f)] [Tooltip("In Second")] private float m_growthTimer = 2f;
	[SerializeField] [Range(0f, 10f)] [Tooltip("In Second")] private float m_timeToFade = 3f;

	[SerializeField] private State m_state = State.InGround;
	[SerializeField] private GameObject m_plateform;

	private bool m_isSpring = false;
	private float m_growthProgress = 0f; // Entre 0 et 1

	private void Start()
	{
		m_growthTimer = 1 / m_growthTimer;
		m_plateform.SetActive(false);
	}

	private void HandleNewState(State newState)
	{
		switch(newState)
		{
			case State.InGround:
				{
					m_growthProgress = 0;
					m_plateform.SetActive(false);
					break;
				}
			case State.GrowthInProgress:
				{
					m_growthProgress = 0;
					break;
				}
			case State.GrowthCompleted:
				{
					m_plateform.SetActive(true);
					break;
				}
			case State.Dying:
				{
					if(m_state == State.GrowthCompleted) // On a enlevé le spring pour utiliser la plateforme
					{
						StartCoroutine(Dying());
					}
					else if(m_state == State.GrowthInProgress) // On a enlevé le spring avant que la plateforme soit utilisable
					{
						StartCoroutine(ReverseGrowth());
					}

					break;
				}
		}

		m_state = newState;
	}

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		m_isSpring = true;
		if(m_state == State.InGround) // On fait poussé la plante
		{
			HandleNewState(State.GrowthInProgress);
		}
		else if (m_state == State.Dying) // On arrête la mort de la plante
		{
			HandleNewState(State.GrowthCompleted);
		}
	}

	public void OnTriggerFilteredStay(GameObject obj)
	{
		if(m_state == State.GrowthInProgress)
		{
			if(m_isSpring)
			{
				m_growthProgress += m_growthTimer * Time.deltaTime;
				if(m_growthProgress >= 1)
				{
					HandleNewState(State.GrowthCompleted);
				}
			}
		}
	}

	public void OnTriggerFilteredExit(GameObject obj)
	{
		m_isSpring = false;
		if(m_state != State.InGround && m_state != State.Dying)
		{
			HandleNewState(State.Dying);
		}
	}

	private IEnumerator Dying()
	{
		yield return new WaitUntil(() => m_state == State.Dying);
		yield return new WaitForSeconds(m_timeToFade);
		if(m_state == State.Dying)
		{
			HandleNewState(State.InGround);
		}
	}

	private IEnumerator ReverseGrowth()
	{
		yield return new WaitUntil(() => m_state == State.Dying);
		HandleNewState(State.InGround);
	}
}
