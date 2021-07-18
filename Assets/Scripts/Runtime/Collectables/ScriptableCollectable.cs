using System.Collections;
using System.Collections.Generic;

using CustomEvents;

using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Item/Collectable")]
public class ScriptableCollectable : ScriptableObject
{

	[SerializeField] [Range(0, 100)] private int m_number = 0;
	[SerializeField] [Range(1, 100)] private int m_numberMax = 1;
	[SerializeField] public bool can_be_reset = true;
	[SerializeField] private CollectableEvent m_collectableEvent;
	public string Name => name;

	public int Number
	{
		get => m_number;
		set
		{
			int new_value = value;
			m_number = Mathf.Clamp(new_value, 0, m_numberMax);
			m_collectableEvent.Invoke(this);
		}
	}
	public int NumberMax => m_numberMax;

	private void OnValidate()
	{
		if(m_number > m_numberMax)
		{
			m_number = m_numberMax;
		}
	}

	public void Reset()
	{
		m_number = 0;
	}
}
