using System.Collections;
using System.Collections.Generic;

using FMOD.Studio;

using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : UIBehaviour, IPointerClickHandler, IPointerEnterHandler
{

	[SerializeField] private bool m_isButtonBack;
	[FMODUnity.EventRef] [SerializeField] private string m_hoverName;
	[FMODUnity.EventRef] [SerializeField] private string m_validationName;
	[FMODUnity.EventRef] [SerializeField] private string m_backName;

	private EventInstance m_hover;
	private EventInstance m_validation;
	private EventInstance m_back;

	protected override void Start()
	{
		m_hover = FMODUnity.RuntimeManager.CreateInstance(m_hoverName);
		m_validation = FMODUnity.RuntimeManager.CreateInstance(m_validationName);
		m_back = FMODUnity.RuntimeManager.CreateInstance(m_backName);
	}

	protected override void OnDestroy()
	{
		m_hover.release();
		m_validation.release();
		m_back.release();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(m_isButtonBack)
		{
			m_back.start();
		}
		else
		{
			m_validation.start();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_hover.start();
	}
}
