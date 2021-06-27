using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Item/Collectable")]
public class ScriptableCollectable: ScriptableObject
{
	[SerializeField] [Range(0, 100)] private int m_number = 0;
	[SerializeField] [Range(1, 100)] private int m_maxNumber = 1;

	public bool CanCollect() => m_number < m_maxNumber;
	
	public string Name => name;
	public int Number { get => m_number; set => m_number = value; }
	public int MaxNumber { get => m_maxNumber; }
}
