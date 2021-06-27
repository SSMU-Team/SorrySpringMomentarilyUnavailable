using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEvents;

public enum TaskType
{
	Tutorial,
	Spirit_0,
	Spirit_1,
	Spirit_2,
}

public enum TaskState
{
	Unknow,
	Discovered,
	Completed
}

[CreateAssetMenu(fileName = "Task", menuName = "ToDoList/Task")]
public class ScriptableTask : ScriptableObject
{
	[SerializeField] [Multiline] private string m_description;
	[SerializeField] [Range(0, 100)] private int m_numberPerformed = 0;
	[SerializeField] [Range(1, 100)] private int m_numberPerformToDone = 1;
	[SerializeField] private TaskType m_type;
	[SerializeField] private TaskState m_state;
	[SerializeField] private ScriptableTaskEvent m_taskEvent;

	public string Description => m_description;
	public int NumberPerformToDone => m_numberPerformToDone;
	public TaskType Type => m_type;
	public TaskState State => m_state;

	public int NumberPerformed 
	{ 
		get => m_numberPerformed;
		set 
		{
			if(m_state == TaskState.Discovered)
			{
				int new_value = value;
				m_numberPerformed = Mathf.Clamp(new_value, 0, m_numberPerformToDone);
				UpdateState();
			}
		}  
	}

	private void OnValidate()
	{
		if(m_numberPerformed > m_numberPerformToDone)
		{
			m_numberPerformed = m_numberPerformToDone;
		}
		if(m_state == TaskState.Unknow)
		{
			m_numberPerformed = 0;
		}
		else if(m_state == TaskState.Completed)
		{
			m_numberPerformed = m_numberPerformToDone;
		}

		if(Application.isPlaying)
		{
			UpdateState();
		}
	}

	private void OnEnable()
	{
		m_state = TaskState.Unknow;
		NumberPerformed = 0;
	}

	[ContextMenu("Give Task")]
	public void GiveTask()
	{
		if(m_state == TaskState.Unknow)
		{
			m_state = TaskState.Discovered;
			m_taskEvent.Invoke(this);
		}
	}

	[ContextMenu("Update Task")]
	private void UpdateState()
	{
		if(m_state != TaskState.Unknow)
		{
			if(m_numberPerformed == m_numberPerformToDone)
			{
				m_state = TaskState.Completed;
			}
			else
			{
				m_state = TaskState.Discovered;
			}
			m_taskEvent.Invoke(this);
		}
	}
}

