using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEngine.InputSystem.InputAction;
using Utility;
using CustomEvents;

public class JournalController : Menu
{
	[SerializeField] private ScriptableTaskEvent m_taskEvent;
	[SerializeField] private Button[] m_buttons;
	[SerializeField] Transform m_spawnPoint;
	[SerializeField] GameObject m_itemPrefab;
	[SerializeField] float m_sizeItem;

	private List<UITask> m_uiTasks;
	private TaskType m_typeToDisplay;

	private float m_spawnY = 0;

	private new void Awake()
	{
		base.Awake();
		m_typeToDisplay = TaskType.Tutorial;
		m_taskEvent.AddListener(OnUpdateTask);
		m_uiTasks = new List<UITask>();
		m_buttons[0].onClick.AddListener(delegate { OnChangeList(TaskType.Tutorial); });
		m_buttons[1].onClick.AddListener(delegate { OnChangeList(TaskType.Spirit_0); });
		m_buttons[2].onClick.AddListener(delegate { OnChangeList(TaskType.Spirit_1); });
		m_buttons[3].onClick.AddListener(delegate { OnChangeList(TaskType.Spirit_2); });
	}

	private void OnChangeList(TaskType type)
	{
		m_typeToDisplay = type;
		foreach(UITask ui in m_uiTasks)
		{
			ui.gameObject.SetActive(false);
		}

		List<UITask> ui_to_display = m_uiTasks.FindAll(ui => ui.Task.Type == m_typeToDisplay);

		for(int i = 0; i < ui_to_display.Count; i++)
		{
			UITask ui = ui_to_display[i];
			m_spawnY = i * m_sizeItem;
			ui.gameObject.transform.localPosition = new Vector3(0, -m_spawnY, m_spawnPoint.localPosition.z);
			ui.gameObject.SetActive(true);
		}
	}

	public void OnJournal(CallbackContext ctx)
	{
		if(ctx.performed)
		{
			menuEvent.Invoke(type);
		}
	}

	public void OnUpdateTask(ScriptableTask task)
	{
		UITask ui = null;

		foreach(UITask cur_ui in m_uiTasks)
		{
			if(cur_ui.Task == task)
			{
				ui = cur_ui;
				break;
			}
		}
		
		if(ui != null)
		{
			UpdateUITask(ui);
		}
		else
		{
			NewUITask(task);
		}
	}

	private void NewUITask(ScriptableTask task)
	{
		GameObject go = Instantiate(m_itemPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
		go.transform.SetParent(m_spawnPoint, false);
		UITask ui = go.GetComponent<UITask>();
		ui.Task = task;
		m_uiTasks.Add(ui);
		UpdateUITask(ui);
		if (task.Type == m_typeToDisplay)
		{
			ui.gameObject.transform.localPosition = new Vector3(0, -m_spawnY, m_spawnPoint.localPosition.z);
			ui.gameObject.SetActive(true);
			m_spawnY += m_sizeItem;
		}
	}

	private void UpdateUITask(UITask ui)
	{
		ui.Description = ui.Task.Description;
		if(ui.Task.NumberPerformToDone > 1)
		{
			ui.NumberToPerform = Convert.ToString(ui.Task.NumberPerformToDone);
			ui.NumberPerformed = Convert.ToString(ui.Task.NumberPerformed);
		}

		if(ui.Task.State == TaskState.Completed)
		{
			ui.Complete();
		}
	}

	public override void OpenMenu()
	{		
		Enabled = true;
		CursorState.SetCursorState(CursorLockMode.None, true);
	}

	public override void CloseMenu()
	{
		Enabled = false;
		CursorState.SetCursorState(CursorLockMode.Confined, false);
	}
}