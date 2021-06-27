using UnityEngine;

public class PrefabFactory : MonoBehaviour
{
	[SerializeField] GameObject m_prefab;
	[SerializeField] [Min(1)] int m_number = 1;

	[ContextMenu("Create GameObject")]
	public void Create()
	{
		if(m_prefab)
		{
			if(m_number > 1)
			{
				for(int i = 0; i < m_number; ++i)
				{
					GameObject obj = Instantiate(m_prefab);
					obj.name = m_prefab.name + "_" + i;
				}
			}
			else
			{
				GameObject obj = Instantiate(m_prefab);
				obj.name = m_prefab.name;
			}
		}
		else
		{
			Debug.LogError("No prefab to clone.");
		}
	}
}
