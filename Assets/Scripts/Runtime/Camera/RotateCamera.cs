using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	[SerializeField] private Transform m_target;

	[SerializeField] private bool m_activateRotation = true;
	[SerializeField] [Range(0f, 100f)] private float m_speed = 1f;
	[SerializeField] private bool m_moveRightOrLeft = true;

	private void FixedUpdate()
	{
		if(m_activateRotation)
		{
			transform.LookAt(m_target);
			Vector3 movement = m_moveRightOrLeft ? Vector3.right : Vector3.left;
			transform.Translate(movement * m_speed * Time.fixedDeltaTime);
		}
	}
}
