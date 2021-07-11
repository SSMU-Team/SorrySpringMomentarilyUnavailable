using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

public class WorldText : MonoBehaviour
{
	Camera m_mainCamera;
	RotationConstraint m_constraints;

	private void Start()
	{
		m_constraints = GetComponent<RotationConstraint>();
		m_mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		ConstraintSource constraint = new ConstraintSource();
		constraint.sourceTransform = m_mainCamera.transform;
		constraint.weight = 1.0f;
		m_constraints.AddSource(constraint);
	}

}
