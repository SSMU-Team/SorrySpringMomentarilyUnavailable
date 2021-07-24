using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
	Animator m_anim;
	PlayerMovementContoller m_movement;
	Rigidbody m_parentRigid;

	void Start()
	{
		m_anim = GetComponent<Animator>();
		m_movement = GetComponentInParent<PlayerMovementContoller>();
		m_parentRigid = GetComponentInParent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		m_anim.SetFloat("SpeedXZ", m_movement.IsMoving ? 1.0f : 0.0f);
		m_anim.SetFloat("SpeedY", m_parentRigid.velocity.y);
		m_anim.SetBool("IsGrounded", m_movement.IsGrounded);
	}
}
