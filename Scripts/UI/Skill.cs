using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour {

	// Inspector
	public Image m_Icon;
	public Text m_SkillText;
	public Text m_SkillEffectText;

	// 버튼 구성
	public Button m_BuyBtn;
	public Text m_SkillPrice;
	public Text m_LearnTxt;

	public float m_SkillLv;					// 스킬 현재레벨
	public float m_SkillMoney;				// 스킬 1렙 비용
	public float m_SkillPer;                // 레벨당 비용증가율
	public int m_NeedLv;                    // 필요레벨
	public float m_PlusPer;					// 스킬 효과 증가비율

	[HideInInspector] public Sprite m_Sprite;   // 아이콘 저장
	[HideInInspector] public bool m_Lock;

	// Function
	private void Awake()
	{
		m_Lock = true;
		m_Sprite = m_Icon.sprite;
	}
	public float ReturnLevelUpPrice()
	{
		if (m_SkillLv == 0)
			return m_SkillMoney;
		else
			return (m_SkillLv+1) * m_SkillMoney * m_SkillPer;
	}


}
