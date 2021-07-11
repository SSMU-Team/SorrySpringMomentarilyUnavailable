
using UnityEngine;
using UnityEngine.Rendering;

public class GraphSceneHUDController : MonoBehaviour
{
	//0 = vue tps ; 1 = vue rts
	[SerializeField] private int m_viewState = 0;
	[SerializeField] private GameObject m_player;
	[SerializeField] private GameObject m_freeCam;
	[SerializeField] private GameObject m_prefab_postprocess_debug;

	private Volume m_monochrome;

	private void Start()
	{
		m_freeCam.SetActive(false);
		m_player.SetActive(true);
		m_monochrome = GameObject.Find("Post Process DebugLuminosity").GetComponent<Volume>();
		if(m_monochrome == null)
		{
			m_monochrome = Instantiate(m_prefab_postprocess_debug).GetComponent<Volume>();
		}
		m_monochrome.enabled = false;
	}

	public void DebugLuminosity()
	{
		m_monochrome.enabled = !m_monochrome.enabled;
	}

	public void ChangeSeason(int season)
	{
		SpringManager.Instance.SpringMode = (SpringSceneMode)(season);
	}

	public void ChangeView()
	{
		if(m_viewState == 0)
		{
			m_freeCam.transform.position = m_player.transform.position;

			m_viewState = 1;
			m_player.SetActive(false);
			m_freeCam.SetActive(true);
		}
		else
		{
			m_player.transform.position = m_freeCam.transform.position;

			m_viewState = 0;
			m_freeCam.SetActive(false);
			m_player.SetActive(true);
		}
	}
}
