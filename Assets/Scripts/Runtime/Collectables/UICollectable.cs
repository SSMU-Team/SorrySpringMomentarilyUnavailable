using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICollectable : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_numberPerformed;
	[SerializeField] private TextMeshProUGUI m_numberToPerform;

	public ScriptableCollectable collectable;

	public string NumberPerformed { get => m_numberPerformed.text; set => m_numberPerformed.text = value; }
	public string NumberToPerform { get => m_numberToPerform.text; set => m_numberToPerform.text = value; }
	private void Start()
	{
		NumberPerformed = collectable.Number.ToString();
		NumberToPerform = collectable.NumberMax.ToString();
	}

}
