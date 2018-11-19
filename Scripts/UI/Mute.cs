using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MuteSystem
{
	private static MuteSystem _instance = null;

	public bool m_Mute;
	public static MuteSystem Instacne
	{
		get
		{
			if (_instance == null)
				_instance = new MuteSystem();
			return _instance;
		}
	}
}
public class Mute : MonoBehaviour {

	// Inspector
	public Toggle m_Toggle;
	public Image m_Image;

	string m_MuteOn = "Mute1";
	string m_MuteOff = "Mute2";

	public void Start()
	{
		MuteCheck();
	}
	public void SetMute()
	{
		if(MuteSystem.Instacne.m_Mute)
		{
			MuteSystem.Instacne.m_Mute = false;
			m_Image.sprite = Resources.Load<Sprite>(m_MuteOn);
		}
		else
		{
			MuteSystem.Instacne.m_Mute = true;
			m_Image.sprite = Resources.Load<Sprite>(m_MuteOff);
		}
		CoinInfo.Instance.m_SoundMgr.MuteActive(MuteSystem.Instacne.m_Mute);
		m_Toggle.targetGraphic = m_Image;
	}

	public void MuteCheck()
	{
		if (MuteSystem.Instacne.m_Mute)
		{
			m_Image.sprite = Resources.Load<Sprite>(m_MuteOff);
		}
		else
		{
			m_Image.sprite = Resources.Load<Sprite>(m_MuteOn);
		}
		CoinInfo.Instance.m_SoundMgr.MuteActive(MuteSystem.Instacne.m_Mute);
		m_Toggle.targetGraphic = m_Image;
	}
}
