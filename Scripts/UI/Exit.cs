using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour {

	// Inspector
	[SerializeField] GameObject m_CheckBox;
	[SerializeField] Image m_Fade;
	[SerializeField] CoinSelect m_CoinSelect;
	[SerializeField] SkillSelect m_SkillSelect;

	// Function
	public void ExitBtn()
	{
		if (m_CheckBox.active == false)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(14);
			CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_Fade);
			m_CheckBox.SetActive(true);
			m_Fade.raycastTarget = true;
		}
	}

	public void OkBtn()
	{
		Application.Quit();
	}

	public void NoBtn()
	{
		m_Fade.raycastTarget = false;
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(1.0f, ref m_Fade);
		m_CheckBox.SetActive(false);
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if(m_CoinSelect.m_CheckBox.active)
			{
				m_CoinSelect.NoBtn();
			}
			else if(m_SkillSelect.m_CheckBox.active)
			{
				m_SkillSelect.CancelBtn();
			}
			else if(m_CheckBox.active)
			{
				NoBtn();
			}
			else if(m_CheckBox.active == false)
			{
				ExitBtn();
			}
		}
	}
}
