using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
	// Inspector
	[SerializeField] Text[] m_NeedLv;
	[SerializeField] Image m_SkillRock;
	[SerializeField] Skill[] m_Skills;

	// 체크박스
	[SerializeField] public GameObject m_CheckBox;
	[SerializeField] Skill m_BoxSkill;
	[SerializeField] Text m_BoxEffectTxt;
	[SerializeField] Text m_BoxNextLvTxt;
	[SerializeField] Image m_Fade;

	int m_SkillNum;

	// Function
	private void Start()
	{
		m_CheckBox.SetActive(false);
		MenuStart();
	}

	public void MenuStart()
	{
		for (int i = 0; i < 6; i++)
		{
			m_Skills[i].m_SkillLv = CoinInfo.Instance.m_Skills[i];
			if (m_Skills[i].m_SkillLv != 10)
				m_Skills[i].m_SkillPrice.text = string.Format("{0:n0}", m_Skills[i].ReturnLevelUpPrice()) + "원";
			else
				m_Skills[i].m_SkillPrice.text = "MAX LEVEL";
		}
		BtnUpdate();
		if (CoinInfo.Instance.m_Lv < 3)
			m_NeedLv[0].text = "필요레벨: " + "<color=red>" + "3" + "</color>";
		else
			m_NeedLv[0].text = "필요레벨: " + "<color=#969696FF>" + "3" + "</color>";

		if (CoinInfo.Instance.m_Lv < 5)
			m_NeedLv[1].text = "필요레벨: " + "<color=red>" + "5" + "</color>";
		else
			m_NeedLv[1].text = "필요레벨: " + "<color=#969696FF>" + "5" + "</color>";

		for (int num = 0; num < 6; num++)
		{
			if (num == 0)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "상남자 매매법";
					m_Skills[num].m_SkillEffectText.text = "초기 매수 충전 횟수 증가";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "상남자 매매법";
					m_Skills[num].m_SkillEffectText.text = "초기 매수 충전 횟수 " + "<color=red>" + m_Skills[num].m_SkillLv + "</color> 증가";
				}
			}
			else if (num == 1)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "먹이를 노리는 사자 매매법";
					m_Skills[num].m_SkillEffectText.text = "최대 매수 충전 횟수 증가";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "먹이를 노리는 사자 매매법";
					m_Skills[num].m_SkillEffectText.text = "최대 매수 충전 횟수 " + "<color=red>" + m_Skills[num].m_SkillLv + "</color> 증가";
				}
			}
			else if (num == 2)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "맞아야 정신차리지 매매법";
					m_Skills[num].m_SkillEffectText.text = "매수맨 이동속도 증가";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "맞아야 정신차리지 매매법";
					m_Skills[num].m_SkillEffectText.text = "매수맨 이동속도 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 증가";
				}
			}
			else if (num == 3)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "폭주기관차두리 매매법";
					m_Skills[num].m_SkillEffectText.text = "가즈아- 초기 지속 시간 증가";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "폭주기관차두리 매매법";
					m_Skills[num].m_SkillEffectText.text = "가즈아- 초기 지속 시간 " + "<color=red>" + m_Skills[num].m_SkillLv + "초</color>" + " 증가";
				}
			}
			else if (num == 4)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "심상치 않은 매매법";
					m_Skills[num].m_SkillEffectText.text = "등반 시 추가 매수맨 등장 확률 증가";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "심상치 않은 매매법";
					m_Skills[num].m_SkillEffectText.text = "등반 시 추가 매수맨 등장 확률 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 증가";
				}
			}
			else if (num == 5)
			{
				if (m_Skills[num].m_SkillLv == 0)
				{
					m_Skills[num].m_SkillText.text = "무조건 간다 매매법";
					m_Skills[num].m_SkillEffectText.text = "매수맨 등장주기 감소";
				}
				else
				{
					m_Skills[num].m_SkillText.text = "무조건 간다 매매법";
					m_Skills[num].m_SkillEffectText.text = "매수맨 등장주기 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 감소";
				}
			}
		}
		SkillInfoUpdate();
	}
	public void SkillInfoUpdate()
	{
		for (int i = CoinInfo.Instance.m_Lv-1; i < 6; i++)
		{
			if(m_Skills[i].m_Lock && m_Skills[i].m_NeedLv > CoinInfo.Instance.m_Lv)
			{
				m_Skills[i].m_Icon.sprite = m_SkillRock.sprite;
				m_Skills[i].m_SkillText.text = "? ? ?";
				m_Skills[i].m_SkillEffectText.text = "아직 알려지지 않은 매매법";
				m_Skills[i].m_BuyBtn.gameObject.SetActive(false);
			}
			else if(m_Skills[i].m_Lock)
			{
				m_Skills[i].m_Lock = false;
				m_Skills[i].m_Icon.sprite = m_Skills[i].m_Sprite;
				m_Skills[i].m_BuyBtn.gameObject.SetActive(true);
			}
		}
	}

	public void BtnUpdate()
	{
		for (int i = 0; i < 6; i++)
		{
			if (m_Skills[i].m_SkillLv == 10)
			{
				m_Skills[i].m_BuyBtn.interactable = false;
				m_Skills[i].m_LearnTxt.enabled = false;
			}
			else if (m_Skills[i].m_NeedLv <= CoinInfo.Instance.m_Lv)
			{
				if (m_Skills[i].ReturnLevelUpPrice() > CoinInfo.Instance.m_CurMoney)
					m_Skills[i].m_BuyBtn.interactable = false;
				else
					m_Skills[i].m_BuyBtn.interactable = true;
			}
		}
	}

	public void OkBtn()
	{
		CoinInfo.Instance.m_Skills[m_SkillNum]++;
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.3f, ref m_Fade);
		CoinInfo.Instance.m_SoundMgr.PlaySound(15);
		m_CheckBox.SetActive(false);
		BtnSet(m_SkillNum);
		CoinInfo.Instance.Save();
	}
	public void CancelBtn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		m_CheckBox.SetActive(false);
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.3f, ref m_Fade);
	}

	public void CheckBoxSet()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		if (m_Skills[m_SkillNum].m_SkillLv == 0)
		{
			m_BoxSkill.m_SkillText.text = m_Skills[m_SkillNum].m_SkillText.text.ToString();
			m_BoxSkill.m_Icon.sprite = m_Skills[m_SkillNum].m_Icon.sprite;
			m_BoxSkill.m_SkillEffectText.text = "Lv" + (m_Skills[m_SkillNum].m_SkillLv + 1.0f);
			m_BoxNextLvTxt.text = m_Skills[m_SkillNum].m_PlusPer.ToString();
			m_BoxSkill.m_SkillPrice.text = string.Format("{0:n0}", m_Skills[m_SkillNum].ReturnLevelUpPrice()) + " 원";
		}
		else
		{
			m_BoxSkill.m_SkillText.text = m_Skills[m_SkillNum].m_SkillText.text.ToString();
			m_BoxSkill.m_Icon.sprite = m_Skills[m_SkillNum].m_Icon.sprite;
			m_BoxSkill.m_SkillEffectText.text = "Lv" + m_Skills[m_SkillNum].m_SkillLv + "→Lv" + (m_Skills[m_SkillNum].m_SkillLv + 1.0f);
			m_BoxNextLvTxt.text = (m_Skills[m_SkillNum].m_PlusPer * (m_Skills[m_SkillNum].m_SkillLv)).ToString() + "→" + (m_Skills[m_SkillNum].m_PlusPer * (m_Skills[m_SkillNum].m_SkillLv + 1)).ToString();
			m_BoxSkill.m_SkillPrice.text = string.Format("{0:n0}", m_Skills[m_SkillNum].ReturnLevelUpPrice()) + " 원";
		}
	}
	public void Skill1_Btn()
	{
		m_SkillNum = 0;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "초기 매수 충전 횟수";
		CheckBoxSet();
	}

	public void Skill2_Btn()
	{
		m_SkillNum = 1;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "최대 매수 충전 횟수";
		CheckBoxSet();
	}

	public void Skill3_Btn()
	{
		m_SkillNum = 2;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "매수맨 이동속도";
		CheckBoxSet();
	}

	public void Skill4_Btn()
	{
		m_SkillNum = 3;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "가즈아- 초기 지속 시간";
		CheckBoxSet();
	}

	public void Skill5_Btn()
	{
		m_SkillNum = 4;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "등반 시 추가 매수맨 등장 확률";
		CheckBoxSet();
	}

	public void Skill6_Btn()
	{
		m_SkillNum = 5;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.3f, 0.5f, ref m_Fade);
		m_CheckBox.SetActive(true);
		m_BoxEffectTxt.text = "매수맨 등장주기";
		CheckBoxSet();
	}

	public void BtnSet(int _num)
	{
		int num = _num;
		if (CoinInfo.Instance.m_CurMoney >= m_Skills[num].ReturnLevelUpPrice())
		{
			CoinInfo.Instance.m_CurMoney -= m_Skills[num].ReturnLevelUpPrice();
			m_Skills[num].m_SkillLv++;
			if (m_Skills[num].m_SkillLv != 10)
				m_Skills[num].m_SkillPrice.text = string.Format("{0:n0}", m_Skills[num].ReturnLevelUpPrice()) + "원";
			else
			{
				m_Skills[num].m_SkillPrice.text = "MAX LEVEL";
				m_Skills[num].m_LearnTxt.enabled = false;
			}
		}
		if (num == 0)
		{
			m_Skills[num].m_SkillText.text = "상남자 매매법";
			m_Skills[num].m_SkillEffectText.text = "초기 매수 충전 횟수 " + "<color=red>" + m_Skills[num].m_SkillLv + "</color> 증가";
		}
		else if (num == 1)
		{
			m_Skills[num].m_SkillText.text = "먹이를 노리는 사자 매매법";
			m_Skills[num].m_SkillEffectText.text = "최대 매수 충전 횟수 " + "<color=red>" + m_Skills[num].m_SkillLv + "</color> 증가";
		}
		else if (num == 2)
		{
			m_Skills[num].m_SkillText.text = "맞아야 정신차리지 매매법";
			m_Skills[num].m_SkillEffectText.text = "매수맨 이동속도 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 증가";
		}
		
		else if (num == 3)
		{
			m_Skills[num].m_SkillText.text = "폭주기관차두리 매매법";
			m_Skills[num].m_SkillEffectText.text = "가즈아- 초기 지속 시간 " + "<color=red>" + m_Skills[num].m_SkillLv + "초</color>" + " 증가";
		}
		else if (num == 4)
		{
			m_Skills[num].m_SkillText.text = "심상치 않은 매매법";
			m_Skills[num].m_SkillEffectText.text = "등반 시 추가 매수맨 등장 확률 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 증가";
		}
		else if(num == 5)
		{
			m_Skills[num].m_SkillText.text = "무조건 간다 매매법";
			m_Skills[num].m_SkillEffectText.text = "매수맨 등장주기 " + "<color=red>" + m_Skills[num].m_SkillLv * m_Skills[num].m_PlusPer + "%</color>" + " 감소";
		}
		BtnUpdate();
	}
}
