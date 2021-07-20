using System;
using System.Collections;

using CustomEvents;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementContoller : MonoBehaviour
{
	[Header("Basic movement", order = 0)]
	[SerializeField] [Range(0f, 1000f)] private float m_speedWalk;
	[SerializeField] [Range(1f, 2f)] private float m_sprintMultiplier;
	[SerializeField] [Range(5f, 50f)] private float m_rotationSpeed = 10f;

	[Header("Jump and fall", order = 1)]
	[SerializeField] [Range(0.0f, 5.0f)] private float m_airControl = 1.0f;
	[SerializeField] private PhysicMaterial[] m_physicsMat;
	[SerializeField] [Range(0f, 1000f)] private float m_jumpForce;
	[SerializeField] [Range(1, 30)] private int m_jumpStepsPowerMax;
	[SerializeField] [Range(0.01f, 1f)] private float m_jumpCooldown;
	[SerializeField] [Range(0.2f, 1f)] private float m_jumpForceSnowMultiplier;
	[SerializeField] private Vector2 m_minMaxSpeedY;
	[SerializeField] [Range(1f, 10f)] private float m_gravityMultiplier = 3f;
	[SerializeField] [Range(0f, 1f)] private float m_slowFallMultiplier = 0.5f;

	[Header("Spring", order = 2)]
	[SerializeField] [Range(0.0f, 1f)] private float m_smoothSpringTransition;

	[Header("Check Ground", order = 3)]
	[SerializeField] private Transform m_groundCheck;
	[SerializeField] private LayerMask m_whatIsGround;
	[SerializeField] private float m_rayDist;

	[Header("Events", order = 4)]
	[SerializeField] private MoveEvent m_moveEvent;

	public bool IsGrounded { get; private set; } = false;
	public bool IsMoving => m_inputVelocity.sqrMagnitude > 0.1;
	public Vector2 MovementDirection => m_inputVelocity;

	private SpringPlayerCollisionController m_springCollisionController;
	private Rigidbody m_rigidbody;
	private PlayerCameraController m_cameraController;
	private Collider m_collider;

	private bool IsInSpring => m_springCollisionController.IsInSpring || SpringManager.Instance.SpringMode == SpringSceneMode.Spring;

	private Vector2 m_inputVelocity = Vector2.zero;
	private bool m_jumpToPerformThisFrame = false;
	private float m_movementMultiplier = 1;
	private Vector3 m_velocity = Vector3.zero;
	private int m_jumpPerformCounter = 0;
	private bool m_canJump = true;
	private Ray m_rayCheckGround;
	private Ray m_rayCheckWall;
	private bool m_isMouseOverOnPlayer = false;

	private bool m_cooldownJump = false;

	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_springCollisionController = GetComponentInChildren<SpringPlayerCollisionController>();
		m_cameraController = GetComponent<PlayerCameraController>();
		m_collider = GetComponent<Collider>();
		m_collider.material = m_physicsMat[0];
	}

	private void Update()
	{
		//m_isMouseOverOnPlayer = IsMouseOverOnPlayer();
	}

	private void FixedUpdate()
	{
		m_velocity = new Vector3(0, m_rigidbody.velocity.y, 0);

		CheckGround();

		if(IsMoving && IsGrounded)
		{
			ApplyMove(1.0f);
		}
		else if(IsMoving && CheckWall())
		{
			ApplyMove(m_airControl);
		}

		if((IsGrounded && m_jumpPerformCounter == 0) || m_jumpPerformCounter > 0)
		{
			if(m_jumpToPerformThisFrame)
			{
				if(IsInSpring)
				{
					ApplyJump(1);
				}
				else
				{
					ApplyJump(m_jumpForceSnowMultiplier);
				}
			}
		}

		ApplyGravity(m_isMouseOverOnPlayer);

		if(IsMoving)
		{
			FlattenCameraRotation();
		}

		m_velocity.y = Mathf.Clamp(m_velocity.y, m_minMaxSpeedY.x, m_minMaxSpeedY.y);
		m_rigidbody.velocity = m_velocity;

		//Events
		Move move;
		move.is_grounded = IsGrounded;
		move.speed = !IsMoving ? MoveSpeed.Idle :
				m_movementMultiplier == m_sprintMultiplier ? MoveSpeed.Run : MoveSpeed.Walk;
		move.material = MoveMaterial.Grass;
		m_moveEvent.Invoke(move);
	}
	#region Events

	public void OnMove(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			Vector2 direction = context.ReadValue<Vector2>();
			m_inputVelocity = direction;
		}
		else if(context.canceled)
		{
			m_inputVelocity = Vector2.zero;
		}
	}

	public void OnSprint(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			float is_active_sprint = context.ReadValue<float>();
			if(is_active_sprint > 0.5f)
			{
				m_movementMultiplier = m_sprintMultiplier;
			}
		}
		else if(context.canceled)
		{
			m_movementMultiplier = 1;
		}
	}

	public void OnFairyClick(InputAction.CallbackContext context)
	{
		if(m_isMouseOverOnPlayer)
		{
			if(context.started)
			{

			}
			else if(context.canceled)
			{

			}
		}
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			float is_active_jump = context.ReadValue<float>();
			if(is_active_jump > 0.5f)
			{
				if(m_canJump)
				{
					m_jumpToPerformThisFrame = true;
					m_canJump = false;
				}
			}
		}
		else if(context.canceled)
		{
			m_jumpToPerformThisFrame = false;
			m_jumpPerformCounter = 0;
		}
	}

	#endregion

	private void ApplyMove(float springMult)
	{
		float mult = m_speedWalk * m_movementMultiplier * springMult * Time.fixedDeltaTime;
		m_velocity += new Vector3(m_inputVelocity.x, 0, m_inputVelocity.y) * mult;
	}

	private void ApplyJump(float jumpForceSnowMultiplier)
	{
		m_velocity.y += m_jumpForce * jumpForceSnowMultiplier * Time.fixedDeltaTime;
		m_jumpPerformCounter++;
		if(m_jumpPerformCounter >= m_jumpStepsPowerMax)
		{
			m_jumpToPerformThisFrame = false;
			m_jumpPerformCounter = 0;
		}
		IsGrounded = false;
	}

	private void ApplyGravity(bool isSlowFall)
	{
		float gravity = -9.81f * m_gravityMultiplier * (isSlowFall ? m_slowFallMultiplier : 1f) * Time.fixedDeltaTime;
		m_velocity.y += gravity;
	}

	private void CheckGround()
	{
		bool old_is_grounded = IsGrounded;

		RaycastHit hit;
		m_rayCheckGround = new Ray(m_groundCheck.position, Vector3.down);
		IsGrounded = Physics.Raycast(m_rayCheckGround, out hit, m_rayDist, m_whatIsGround);
		if(IsGrounded)
		{
			if(!old_is_grounded)
			{
				m_collider.material = m_physicsMat[1];
				if(!m_cooldownJump)
				{
					StartCoroutine(CooldownJump());
				}
			}
			else
			{
				m_collider.material = m_physicsMat[0];
				//TODO To delete and correct, watchout at slides.
				m_canJump = true;
			}
		}
	}

	private bool CheckWall()
	{
		Quaternion cam_rotation_flattened = m_cameraController.LookRotationForward();
		Vector3 direction = cam_rotation_flattened * new Vector3(m_inputVelocity.x, 0, m_inputVelocity.y).normalized;
		RaycastHit hit;
		m_rayCheckWall = new Ray(transform.position, direction);
		//Debug.DrawLine(m_rayCheckWall.origin, m_rayCheckWall.origin + m_rayCheckWall.direction * 10.0f, Color.red);
		bool wall = Physics.Raycast(m_rayCheckWall, out hit, 1.0f, m_whatIsGround);
		//Debug.Log(wall);
		return !wall;
	}

	private IEnumerator CooldownJump()
	{
		m_cooldownJump = true;
		yield return new WaitForSecondsRealtime(m_jumpCooldown);
		m_canJump = true;
		m_cooldownJump = false;
	}

	private void FlattenCameraRotation()
	{
		Quaternion cam_rotation_flattened = m_cameraController.LookRotationForward();
		// Camera space
		m_velocity = cam_rotation_flattened * m_velocity;

		// Player rotation
		Vector3 input_cam_space = cam_rotation_flattened * new Vector3(m_inputVelocity.x, 0, m_inputVelocity.y);
		Quaternion new_quaternion = Quaternion.LookRotation(input_cam_space);
		transform.rotation = Quaternion.Slerp(transform.rotation, new_quaternion, m_rotationSpeed * Time.fixedDeltaTime);
	}
}