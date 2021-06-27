using UnityEngine;

public class CatchableObject : MonoBehaviour, ICatchableObject
{
	[SerializeField] private Vector3 m_offset;
	private bool m_catchByFairy = false;
	private Transform m_fairyPos;

	private void Update()
	{
		if(m_catchByFairy)
		{
			transform.position = m_fairyPos.position + m_offset;
		}
	}

	public void Catch(GameObject fairy)
	{
		Debug.Log("Catch : " + gameObject.name);
		m_catchByFairy = true;
		m_fairyPos = fairy.transform;
	}

	public void Drop(GameObject fairy)
	{
		Debug.Log("Drop : " + gameObject.name);
		m_catchByFairy = false;
	}
}
