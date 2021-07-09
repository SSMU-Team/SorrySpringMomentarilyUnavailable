using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CustomEvents;

public class ItemUI : MonoBehaviour
{
	[SerializeField] private ScriptableCollectable m_collectable;
	[SerializeField] private TextMeshProUGUI m_name;
	[SerializeField] private TextMeshProUGUI m_number;
	[SerializeField] private TextMeshProUGUI m_max;

	public void OnPause(bool pause)
	{
		if(pause)
		{
			m_name.text = m_collectable.Name;
			m_number.text = m_collectable.Number.ToString();
		}
	}
}
