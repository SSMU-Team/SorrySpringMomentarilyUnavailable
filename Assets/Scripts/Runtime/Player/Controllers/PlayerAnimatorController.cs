using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
	Animator m_anim;
	PlayerMovementContoller m_movement;

	void Start()
	{
		m_anim = GetComponent<Animator>();
		m_movement = GetComponentInParent<PlayerMovementContoller>();
	}

	// Update is called once per frame
	void Update()
	{
		m_anim.SetFloat("SpeedXY", m_movement.IsMoving ? 1.0f : 0.0f);
	}
}
