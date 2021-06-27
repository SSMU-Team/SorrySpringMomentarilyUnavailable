using UnityEngine;

public class FairyCatchObjetController : MonoBehaviour
{
	private ICatchableObject m_catchableObject = null;
	private CapsuleCollider m_capsuleCollider;

	public bool IsCatchObject => m_catchableObject != null;


	private void Start()
	{
		m_capsuleCollider = GetComponent<CapsuleCollider>();
	}

	public bool RechearchObjects()
	{
		RaycastHit[] hits;
		float distance_to_points = (m_capsuleCollider.height / 2) - m_capsuleCollider.radius;
		Vector3 point1 = transform.position + m_capsuleCollider.center + (Vector3.up * distance_to_points);
		Vector3 point2 = transform.position + m_capsuleCollider.center - (Vector3.up * distance_to_points);
		hits = Physics.CapsuleCastAll(point1, point2, m_capsuleCollider.radius, Vector3.up, (m_capsuleCollider.height / 2) + m_capsuleCollider.radius);
		foreach(RaycastHit hit in hits)
		{
			ICatchableObject catchable_object = hit.collider.gameObject.GetComponent<ICatchableObject>();
			if(catchable_object != null)
			{
				m_catchableObject = catchable_object;
				return true;
			}
		}
		return false;
	}

	public void CatchObject()
	{
		if(m_catchableObject != null)
		{
			m_catchableObject.Catch(gameObject);
		}
	}

	public void DropObject()
	{
		if(m_catchableObject != null)
		{
			m_catchableObject.Drop(gameObject);
			m_catchableObject = null;
		}
	}
}
