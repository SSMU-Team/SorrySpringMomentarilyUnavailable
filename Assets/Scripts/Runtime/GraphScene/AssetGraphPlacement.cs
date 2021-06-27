using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class AssetGraphPlacement : MonoBehaviour
{
	[SerializeField]
	private GameObject m_container_props;

	[Min(1)]
	[SerializeField]
	private int m_numberByRow;

	[SerializeField]
	private float m_minimalSpace;

	[SerializeField]
	private List<float> m_size_row = new List<float>();

	private int m_nbchild;


	[ContextMenu("Compute Position")]
	void ComputePositionChildren()
	{
		m_nbchild = m_container_props.transform.childCount;

		if(m_size_row.Count == 0)
			ComputeSizeRow(m_numberByRow);

		Vector3 position = Vector3.zero;
		int row = 0;
		for(int i = 0; i < m_nbchild; i++)
		{
			Transform child = m_container_props.transform.GetChild(i);
			Bounds b = GetMaxBounds(child.gameObject);
			float half_size_square = Mathf.Max(b.extents.x, b.extents.z);

			child.position = position + (Vector3.right * half_size_square);
			position += Vector3.right * ((2 * half_size_square) + m_minimalSpace);

			if(i % m_numberByRow == m_numberByRow - 1)
			{
				position.z += m_size_row[row + 1] + m_minimalSpace + m_size_row[row];
				position.x = 0;
				row++;
			}
		}
	}

	void ComputeSizeRow(int size_row)
	{
		m_size_row = new List<float>();
		float half_max_row_size = 0;
		for(int i = 0; i < m_nbchild; i++)
		{
			Transform child = m_container_props.transform.GetChild(i);
			Bounds b = GetMaxBounds(child.gameObject);
			float half_size_square = Mathf.Max(b.extents.x, b.extents.z);
			half_max_row_size = Mathf.Max(half_max_row_size, half_size_square);
			if(i % size_row == size_row - 1)
			{
				m_size_row.Add(half_max_row_size);
				half_max_row_size = 0;
			}
		}
		m_size_row.Add(half_max_row_size);
	}

	Bounds GetMaxBounds(GameObject g)
	{
		Renderer[] renderers = g.GetComponentsInChildren<Renderer>();
		if(renderers.Length == 0)
			return new Bounds(g.transform.position, Vector3.zero);
		Bounds b = renderers[0].bounds;
		foreach(Renderer r in renderers)
		{
			b.Encapsulate(r.bounds);
		}
		return b;
	}
}
