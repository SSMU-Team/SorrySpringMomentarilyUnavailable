
using CustomEvents;

using UnityEngine;

using Utility;

using static UnityEngine.InputSystem.InputAction;

public class PauseController : Menu
{
	[SerializeField] private BoolEvent m_pauseEvent;

	private void Start()
	{
		OnApplicationPause(false);
	}

	public void OnPause(CallbackContext ctx)
	{
		if(ctx.performed)
		{
			menuEvent.Invoke(type);
		}
	}

	public override void OpenMenu()
	{
		OnApplicationPause(true);
	}

	public override void CloseMenu()
	{
		OnApplicationPause(false);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		Enabled = pauseStatus;
		if(Enabled)
		{
			Time.timeScale = 0;
			CursorState.SetCursorState(CursorLockMode.None, true);
		}
		else
		{
			Time.timeScale = 1;
			CursorState.SetCursorState(CursorLockMode.Confined, false);
		}
		m_pauseEvent.Invoke(pauseStatus);
	}

	public void OnButtonQuit()
	{
		MenuUtilities.Quit();
	}
}
