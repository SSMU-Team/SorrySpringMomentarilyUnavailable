
using UnityEngine;

using Utility.Singleton;


public enum SpringSceneMode
{
	WinterOnly = -1,
	Normal = 0,
	Spring = 1
}

[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
public class SpringController : SingletonBehaviour<SpringController>
{
	[SerializeField]
	private float m_hotPointSoftRadius;

	[SerializeField]
	private float m_hotRadius = 0;

	[SerializeField]
	private SpringSceneMode m_springMode = 0;


	private Transform m_target;
	private SphereCollider m_collider;
	private int m_shaderid_hotpoint;
	private int m_shaderid_hotradius;
	private int m_shaderid_softradius;
	private int m_shaderid_modespring;

	public float HotRadius { get => m_hotRadius; set => m_hotRadius = value; }
	public SpringSceneMode SpringMode { get => m_springMode; set => m_springMode = value; }

	private void Start()
	{
		m_hotRadius = 0;
		FairyController fairy = FindObjectOfType<FairyController>();
		if(fairy != null)
		{
			m_target = fairy.transform;
			m_collider = m_target.GetComponent<SphereCollider>();
		}

		m_shaderid_hotpoint = Shader.PropertyToID("_HotPoint");
		m_shaderid_hotradius = Shader.PropertyToID("_HotPointRadius");
		m_shaderid_softradius = Shader.PropertyToID("_HotPointSoft");
		m_shaderid_modespring = Shader.PropertyToID("_SpringMode");
	}

	[ContextMenu("Reset mode")]
	private void Update()
	{
		if(m_target != null && m_target.hasChanged)
		{
			m_collider.radius = m_hotRadius;
			Shader.SetGlobalVector(m_shaderid_hotpoint, new Vector4(m_target.position.x, m_target.position.y, m_target.position.z, 0));
			Shader.SetGlobalFloat(m_shaderid_hotradius, m_hotRadius);
			Shader.SetGlobalFloat(m_shaderid_softradius, m_hotRadius > 0.1 ? m_hotPointSoftRadius : 0);
		}
		Shader.SetGlobalFloat(m_shaderid_modespring, (int)m_springMode);
	}
}
