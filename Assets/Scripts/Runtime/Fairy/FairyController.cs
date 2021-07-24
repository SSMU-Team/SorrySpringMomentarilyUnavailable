
using CustomEvents;

using DG.Tweening;

using UnityEngine;

using static UnityEngine.InputSystem.InputAction;

public class FairyController : MonoBehaviour
{
	[Header("General Fairy ground interaction")]
	[SerializeField] private LayerMask m_layerMask;
	[SerializeField] private float m_maxDistance;
	[SerializeField] private float m_positionOffset;

	[Header("Speed")]
	[Range(0.001f, 5.0f)]
	[SerializeField] private float m_smoothSpeedFairy;
	[Range(1f, 50.0f)]
	[SerializeField] private float m_maxSpeedFairy;

	[Header("Spring")]
	[Min(0.25f)]
	[SerializeField] private float m_radiusSpring;
	[Range(0.001f, 20.0f)]
	[SerializeField] private float m_durationSpringTransition;
	[SerializeField] private Ease m_springEase;
	[SerializeField] private float m_minLevelCharge = 0.05f;

	[Header("End Level")]
	[Min(0.25f)]
	[SerializeField] private float m_radiusSpringEnd;
	[Range(0.001f, 20.0f)]
	[SerializeField] private float m_durationSpringTransitionEnd;

	private Camera m_main_camera;

	private bool m_springActivated;

	private Vector2 m_mouse_pos;
	private Vector3 m_targetpos;
	private Vector3 m_currentvelocity;

	private float m_actualCharge;
	private float m_speedCharge;
	private Tweener m_springtween;

	private bool m_isPaused;

	private float m_lastFairyPosY;

	private bool m_canMoveFairy = true;

	#region Events

	public void OnPause(bool pause)
	{
		m_isPaused = pause;
	}

	public void OnFairyPosition(CallbackContext ctx)
	{
		if(ctx.performed)
		{
			m_mouse_pos = ctx.ReadValue<Vector2>();
		}
	}

	public void OnFairyClick(CallbackContext ctx)
	{
		if(ctx.started && !m_springActivated && !m_isPaused)
		{
			ActivateSpring(true);
		}
		else if(ctx.canceled && m_springActivated)
		{
			ActivateSpring(false);
		}
	}

	#endregion

	private void Start()
	{
		m_actualCharge = 1.0f;
		m_main_camera = Camera.main;
		SpringManager.Instance.SetFairy(this);
	}

	private void ActivateSpring(bool activate)
	{
		if(!m_springActivated && activate && m_actualCharge > m_minLevelCharge)
		{
			m_springtween.Kill();
			m_springtween = DOTween.To(
				() => SpringManager.Instance.HotRadius,
				x => SpringManager.Instance.HotRadius = x,
				m_radiusSpring,
				m_durationSpringTransition)
			.SetEase(m_springEase)
			.Play()
			;
			m_springActivated = true;

		}
		else if(m_springActivated && !activate)
		{
			m_springtween.Kill();
			m_springtween = DOTween.To(
				() => SpringManager.Instance.HotRadius,
				x => SpringManager.Instance.HotRadius = x,
				0.0f,
				m_durationSpringTransition)
			.SetEase(m_springEase)
			.Play()
			;
			m_springActivated = false;
		}
	}

	private void FixedUpdate()
	{
		if(m_canMoveFairy)
		{
			FairyMovements();
		}
	}

	private void FairyMovements()
	{
		Ray ray = m_main_camera.ScreenPointToRay(m_mouse_pos);

		if(Physics.Raycast(ray, out RaycastHit raycast_hit, float.MaxValue, m_layerMask))
		{
			m_targetpos = raycast_hit.point + (raycast_hit.normal * m_positionOffset);
#if UNITY_EDITOR
			Debug.DrawLine(m_targetpos, transform.position, Color.red);
#endif
			Vector3 new_pos = Vector3.SmoothDamp(transform.position, m_targetpos, ref m_currentvelocity, m_smoothSpeedFairy, m_maxSpeedFairy, Time.fixedDeltaTime);

			Vector3 player_pos = PlayerReference.PlayerGameObject.transform.position;
			Vector2 center_pos = new Vector2(player_pos.x, player_pos.z);

			if(Vector2.Distance(new Vector2(new_pos.x, new_pos.z), center_pos) >= m_maxDistance)
			{
				new_pos.y = m_lastFairyPosY;
				Vector2 v = new Vector2(new_pos.x, new_pos.z) - center_pos;
				v = Vector2.ClampMagnitude(v, m_maxDistance);

				new_pos = new Vector3(center_pos.x, new_pos.y, center_pos.y) + new Vector3(v.x, 0, v.y);
			}
			else
			{
				m_lastFairyPosY = new_pos.y;
			}

			transform.position = new_pos;
		}
	}

	public void OnEndLevel(GameObject obj)
	{
		m_radiusSpring = m_radiusSpringEnd;
		m_durationSpringTransition = m_durationSpringTransitionEnd;
		m_canMoveFairy = false;
		ActivateSpring(true);
	}
}
