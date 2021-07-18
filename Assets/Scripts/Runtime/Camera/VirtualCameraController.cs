using Cinemachine;

using CustomEvents;

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VirtualCameraController : MonoBehaviour
{
	[Header("Settings Confiner", order = 1)]
	[SerializeField] private bool m_isDefaultCam = false;
	[SerializeField] [Range(0f, 10f)] private float m_virtualCamDamping;
	[SerializeField] private bool m_confineCamera = false;
	[SerializeField] private bool m_confineScreenEdges = true;

	private CinemachineVirtualCamera m_cMVCamera;

	private CinemachineConfiner m_cmConfiner;
	private Collider m_confinerCollider;

	private void Awake()
	{
		m_cMVCamera = GetComponentInChildren<CinemachineVirtualCamera>();
		m_confinerCollider = GetComponent<Collider>();
		if(!m_isDefaultCam)
		{
			m_confinerCollider.isTrigger = true;
			if(m_confineCamera)
				InitCinemachineConfiner(m_cMVCamera);
			m_cMVCamera.Priority = (int)CamPriority.Off;
		}
		else
		{
			m_confinerCollider.enabled = false;
			m_cMVCamera.Priority = (int)CamPriority.On;
		}
	}

	public void OnTriggerFilteredEnter(GameObject player)
	{
		PlayerCameraController controller = player.GetComponent<PlayerCameraController>();
		controller.EnterCamera(m_cMVCamera);
	}

	public void OnTriggerFilteredExit(GameObject player)
	{
		PlayerCameraController controller = player.GetComponent<PlayerCameraController>();
		controller.ExitCamera(m_cMVCamera);
	}

	private void InitCinemachineConfiner(CinemachineVirtualCamera vCam)
	{
		m_cmConfiner = vCam.gameObject.AddComponent<CinemachineConfiner>();
		m_cmConfiner.m_ConfineMode = CinemachineConfiner.Mode.Confine3D;
		m_cmConfiner.m_BoundingVolume = m_confinerCollider;
		m_cmConfiner.m_Damping = m_virtualCamDamping;
		m_cmConfiner.m_ConfineScreenEdges = m_confineScreenEdges;
	}
}
