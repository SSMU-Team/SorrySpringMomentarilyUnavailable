using System.Collections;
using System.Collections.Generic;

using Cinemachine;

using UnityEngine;

public enum CamPriority : int
{
	On = 1000,
	Off = -1,
}

public class PlayerCameraController : MonoBehaviour
{
	[SerializeField] private GameObject m_prefabDefaultCam;

	private CinemachineVirtualCamera m_defaultCam;
	private Camera m_mainCamera;
	public Vector3 Forward => m_mainCamera.transform.forward;
	private int m_nbCamOn;

	public CinemachineVirtualCamera ActualCam { get; private set; }

	private void Awake()
	{
		m_nbCamOn = 0;
	}

	private void Start()
	{
		m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		GameObject default_cam = GameObject.Find("CM VCam Default");
		if(default_cam == null)
		{
			default_cam = Instantiate(m_prefabDefaultCam);
		}
		m_defaultCam = default_cam.GetComponent<CinemachineVirtualCamera>();
	}

	public void EnterCamera(CinemachineVirtualCamera camera)
	{
		CameraOn(camera);
		if(m_nbCamOn == 0)
		{
			CameraOff(m_defaultCam);
		}
		m_nbCamOn++;
	}

	public void ExitCamera(CinemachineVirtualCamera camera)
	{
		CameraOff(camera);
		m_nbCamOn--;
		if(m_nbCamOn == 0)
		{
			CameraOn(m_defaultCam);
		}
	}

	public Quaternion LookRotationForward()
	{
		Vector3 cam_forward = Forward;
		cam_forward.y = 0f;
		return Quaternion.LookRotation(cam_forward);
	}

	private void CameraOn(CinemachineVirtualCamera camera)
	{
		ActualCam = camera;
		camera.Priority = (int)CamPriority.On;
		camera.Follow = transform;
		camera.LookAt = transform;
	}

	private void CameraOff(CinemachineVirtualCamera camera)
	{
		camera.Priority = (int)CamPriority.Off;
		camera.Follow = null;
		camera.LookAt = null;
	}

	#region Old Code

	/*

	[SerializeField] private CinemachineVirtualCamera m_defaultCMVCamera;
	private CinemachineVirtualCamera m_actualCMVCamera;

	private List<CinemachineVirtualCamera> m_confinersTriggered;

	private bool m_playerIsCatch = false;
	 
	private void Awake()
	{
		m_mainCamera = Camera.main;
		m_actualCMVCamera = m_defaultCMVCamera;
		m_confinersTriggered = new List<CinemachineVirtualCamera>();
	}

	public void EnterConfiner(CinemachineVirtualCamera camera)
	{
		if(m_confinersTriggered.Count == 0)
		{
			StartCoroutine(SetupCameraCoroutine(m_defaultCMVCamera, 100, null, null));
		}
		else
		{
			StartCoroutine(SetupCameraCoroutine(m_actualCMVCamera, 500, null, null));
		}
		m_actualCMVCamera = camera;
		m_confinersTriggered.Add(camera);
		StartCoroutine(SetupCameraCoroutine(camera, 1000, transform, transform));
	}

	public void ExitConfiner(CinemachineVirtualCamera camera)
	{
		StartCoroutine(SetupCameraCoroutine(camera, 100, null, null));
		m_confinersTriggered.Remove(camera);
		if(m_confinersTriggered.Count == 0)
		{
			m_actualCMVCamera = m_defaultCMVCamera;
			GameObject player = PlayerReference.PlayerGameObject;
			StartCoroutine(SetupCameraCoroutine(m_defaultCMVCamera, 1000, player.transform, player.transform));
		}
	}


	public void OnFairyCatchPlayer(bool isCatch)
	{
		if(isCatch)
		{
			StartCoroutine(SetupCameraCoroutine(m_actualCMVCamera, m_actualCMVCamera.Priority, null, null));
			m_playerIsCatch = true;
		}
		else
		{
			GameObject player = PlayerReference.PlayerGameObject;
			m_playerIsCatch = false;
			StartCoroutine(SetupCameraCoroutine(m_actualCMVCamera, 1000, player.transform, player.transform));
		}
	}

	private IEnumerator SetupCameraCoroutine(CinemachineVirtualCamera camera, int priority, Transform follow, Transform lookAt)
	{
		yield return new WaitWhile(() => m_playerIsCatch);

		camera.Priority = priority;
		camera.Follow = follow;
		camera.LookAt = lookAt;
	}*/
	#endregion
}
