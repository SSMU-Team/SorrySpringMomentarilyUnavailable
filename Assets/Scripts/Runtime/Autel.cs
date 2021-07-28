using System.Collections;
using System.Collections.Generic;
using CustomEvents;
using SceneManagement;
using UnityEngine.Playables;
using UnityEngine;

public class Autel : MonoBehaviour, IInteractable
{
	[SerializeField] private ScriptableCollectable m_oakNut;
	[SerializeField] private BoolEvent m_autelEvent;


	public void OnInteraction()
	{
		AutelInteraction();
	}

	private void AutelInteraction()
	{
		if(m_oakNut.IsFull())
		{
			// Le niveau est termin�
			Debug.Log("Le niveau est termin� -> Retour du printemps.");
			m_autelEvent.Invoke(true);
		}
		else
		{
			// Le joueur doit encore chercher des noisettes
			Debug.Log("Il reste des noisettes � r�cup�rer !");
			m_autelEvent.Invoke(false);
		}
	}


}
