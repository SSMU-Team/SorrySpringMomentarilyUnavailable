
using CustomEvents;

using UnityEngine;

public class SpringPlayerCollisionController : MonoBehaviour
{
	[SerializeField]
	private bool m_is_in_spring;

	[SerializeField]
	private BoolEvent m_event_spring;

	public bool IsInSpring => m_is_in_spring;

	public void OnTriggerFilteredEnter(GameObject other)
	{
		m_event_spring.Invoke(true);
		m_is_in_spring = true;
	}

	public void OnTriggerFilteredExit(GameObject other)
	{
		m_event_spring.Invoke(false);
		m_is_in_spring = false;
	}
}
