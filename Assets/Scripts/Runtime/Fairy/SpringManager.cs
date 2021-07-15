
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
public class SpringManager : SingletonBehaviour<SpringManager>
{
	[SerializeField]
	private float m_hotPointSoftRadius;

	[SerializeField]
	private float m_hotRadius = 0;

	[SerializeField]
	private SpringSceneMode m_springMode = 0;

	private FairyController m_fairy;
	private SphereCollider m_collider;
	private int m_shaderid_hotpoint;
	private int m_shaderid_hotradius;
	private int m_shaderid_softradius;
	private int m_shaderid_modespring;

	public float HotRadius { get => m_hotRadius; set => m_hotRadius = value; }
	public SpringSceneMode SpringMode { get => m_springMode; set => m_springMode = value; }

	public FairyController GetFairy()
	{
		return m_fairy;
	}

	public void SetFairy(FairyController value)
	{
		m_fairy = value;
		m_collider = m_fairy.GetComponent<SphereCollider>();
	}

	private void Start()
	{
		m_hotRadius = 0;
		m_shaderid_hotpoint = Shader.PropertyToID("_HotPoint");
		m_shaderid_hotradius = Shader.PropertyToID("_HotPointRadius");
		m_shaderid_softradius = Shader.PropertyToID("_HotPointSoft");
		m_shaderid_modespring = Shader.PropertyToID("_SpringMode");
	}

	[ContextMenu("Reset mode")]
	private void Update()
	{
		if(m_fairy != null && m_fairy.transform.hasChanged)
		{
			m_collider.radius = m_hotRadius;
			Shader.SetGlobalVector(m_shaderid_hotpoint, new Vector4(m_fairy.transform.position.x, m_fairy.transform.position.y, m_fairy.transform.position.z, 0));
			Shader.SetGlobalFloat(m_shaderid_hotradius, m_hotRadius);
			Shader.SetGlobalFloat(m_shaderid_softradius, m_hotRadius > 0.1 ? m_hotPointSoftRadius : 0);
		}
		Shader.SetGlobalFloat(m_shaderid_modespring, (int)m_springMode);
	}
}
