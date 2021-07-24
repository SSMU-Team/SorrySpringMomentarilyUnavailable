using System.Collections;
using System.Collections.Generic;
using CustomEvents;
using SceneManagement;
 using UnityEngine.Playables; 
using UnityEngine;

public class Autel : MonoBehaviour
{
	[SerializeField] private ScriptableCollectable m_oakNut;
	[SerializeField] private SimpleEvent m_interactableEvent;
	[SerializeField] private BoolEvent m_autelEvent;
	[SerializeField] private SimpleEvent m_loadMainMenu;
	[SerializeField] private PlayableDirector m_timeline;

	private bool m_isPlayerCollide = false;

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		m_isPlayerCollide = true;
	}

	public void OnTriggerFilteredExit(GameObject obj)
	{
		m_isPlayerCollide = false;
	}

	public void OnInteraction()
	{
		if(m_isPlayerCollide)
		{
			AutelInteraction();
		}
	}

	private void AutelInteraction()
	{
		if(m_oakNut.IsFull())
		{
			// Le niveau est termin�
			Debug.Log("Le niveau est termin� -> Retour du printemps.");
			m_autelEvent.Invoke(true);
			m_timeline.Play();
		}
		else
		{
			// Le joueur doit encore chercher des noisettes
			Debug.Log("Il reste des noisettes � r�cup�rer !");
			m_autelEvent.Invoke(false);
		}
	}


}
