using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Noisette : MonoBehaviour
{
	[SerializeField] VisualEffect m_itemFound;
	private bool m_isFirstFoundEffect = true;

	public void OnTriggerFilteredEnter(GameObject obj)
	{
		if(m_isFirstFoundEffect)
		{
			m_itemFound.SendEvent("OnObjectFound");
			m_isFirstFoundEffect = false;
		}
	}
}
