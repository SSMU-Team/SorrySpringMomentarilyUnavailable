using System.Collections;
using System.Collections.Generic;
using CustomEvents;
using UnityEngine;

public class GiveTask : MonoBehaviour
{
	[SerializeField] private SimpleEvent m_interactableEvent;
	[SerializeField] private ScriptableTask m_taskToGive;

	private void Give()
	{
		m_taskToGive.GiveTask();
	}

	public void OnInteraction()
	{
		Give();
	}

	public void AddInteraction()
	{
		m_interactableEvent.AddListener(OnInteraction);
	}

	public void RemoveInteraction()
	{
		m_interactableEvent.RemoveListener(OnInteraction);
	}
}
