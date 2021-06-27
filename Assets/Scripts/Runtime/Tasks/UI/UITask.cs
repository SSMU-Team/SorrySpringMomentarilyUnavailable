using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITask : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_description;
	[SerializeField] private TextMeshProUGUI m_numberPerformed;
	[SerializeField] private TextMeshProUGUI m_numberToPerform;

	public ScriptableTask Task { get; set; }

	public string Description { get => m_description.text; set => m_description.text = value; }
	public string NumberPerformed { get => m_numberPerformed.text; set => m_numberPerformed.text = value; }
	public string NumberToPerform { get => m_numberToPerform.text; set => m_numberToPerform.text = value; }

	public void Complete()
	{
		m_description.fontStyle = FontStyles.Strikethrough;
	}
}
