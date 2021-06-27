using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Item/Inventory")]
public class ScriptableInventory : ScriptableObject
{
	[SerializeField] private List<ScriptableCollectable> m_collectables;
	public List<ScriptableCollectable> Collectables => m_collectables;
}

