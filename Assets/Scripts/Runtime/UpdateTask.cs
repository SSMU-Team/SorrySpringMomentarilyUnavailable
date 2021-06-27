using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTask : MonoBehaviour
{
	[SerializeField] private ScriptableTask m_taskToUpdate;

	public int NumberPerformed
	{
		get => m_taskToUpdate.NumberPerformed;
		set => m_taskToUpdate.NumberPerformed = value;
	}
}
