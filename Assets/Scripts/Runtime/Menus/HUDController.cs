
using TMPro;

using UnityEngine;

public class HUDController : MonoBehaviour
{
	private Canvas m_hud;

	private void Awake()
	{
		m_hud = GetComponent<Canvas>();
	}

	public void OnPause(bool pause)
	{
		m_hud.enabled = !pause;
	}
}
