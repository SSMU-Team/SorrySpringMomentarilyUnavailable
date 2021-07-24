using System.Collections;
using System.Collections.Generic;
using CustomEvents;
using SceneManagement;
using UnityEngine.Playables;
using UnityEngine;

public class Autel : MonoBehaviour, IInteractable
{
	[SerializeField] private ScriptableCollectable m_oakNut;
	[SerializeField] private SimpleEvent m_endLevelEvent;
	[SerializeField] private BoolEvent m_autelEvent;
	//[SerializeField] private SimpleEvent m_loadMainMenu;


	public void OnInteraction()
	{
		AutelInteraction();
	}

	private void AutelInteraction()
	{
		if(m_oakNut.IsFull())
		{
			// Le niveau est terminé
			Debug.Log("Le niveau est terminé -> Retour du printemps.");
			m_autelEvent.Invoke(true);
			m_endLevelEvent.Invoke();
		}
		else
		{
			// Le joueur doit encore chercher des noisettes
			Debug.Log("Il reste des noisettes à récupérer !");
			m_autelEvent.Invoke(false);
		}
	}


}
