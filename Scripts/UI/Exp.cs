using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour {

	// Inspector
	[SerializeField] Image m_BoxBack;
	[SerializeField] Image m_Box;
	[SerializeField] Text m_BoxTxt;
	[SerializeField] Text m_LvTxt;

	int m_ScoreSave;
	[HideInInspector] public bool m_Skip;

	// Function
	public void ExpUpdate(int Score)
	{
		if (Score >= 0)
			m_ScoreSave = Score;
		else
			m_ScoreSave = 0;
	}
	public void ExpActive(float fadetime)
	{
		m_Box.fillAmount = (float)CoinInfo.Instance.m_Exp / (float)CoinInfo.Instance.m_ExpLimit;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(fadetime, ref m_BoxBack);
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(fadetime, ref m_Box);
		CoinInfo.Instance.m_CocosFunc.TextFadeIn(fadetime, 1.0f, ref m_BoxTxt);
		CoinInfo.Instance.m_CocosFunc.TextFadeIn(fadetime, 1.0f, ref m_LvTxt);
		StartCoroutine("ExpActiveC");
	}

	IEnumerator ExpActiveC()
	{
		m_Skip = false;
		yield return new WaitForSecondsRealtime(0.5f);
		CoinInfo.Instance.LevelSet();
		m_BoxTxt.text = CoinInfo.Instance.m_Exp.ToString() + " / " + CoinInfo.Instance.m_ExpLimit.ToString();
		m_LvTxt.text = "LV " + CoinInfo.Instance.m_Lv.ToString();
		m_Box.fillAmount = (float)CoinInfo.Instance.m_Exp / (float)CoinInfo.Instance.m_ExpLimit;
		float m_Time = 0.5f / m_ScoreSave;
		StartCoroutine("BarUpdate", m_Time);
	}

	IEnumerator BarUpdate(float m_Time)
	{
		if (m_ScoreSave != 0 && m_Skip == false)
		{
			m_ScoreSave -= 1;
			CoinInfo.Instance.m_Exp++;
			CoinInfo.Instance.LevelSet();
			m_BoxTxt.text = CoinInfo.Instance.m_Exp.ToString() + " / " + CoinInfo.Instance.m_ExpLimit.ToString();
			m_LvTxt.text = "LV " + CoinInfo.Instance.m_Lv.ToString();
			m_Box.fillAmount = (float)CoinInfo.Instance.m_Exp / (float)CoinInfo.Instance.m_ExpLimit;
			yield return new WaitForSecondsRealtime(m_Time);
			StartCoroutine("BarUpdate", m_Time);
		}
	}
	// 경험치 초기화
	public void Init()
	{
		m_ScoreSave = 0;
		m_Box.color = new Color(m_Box.color.r, m_Box.color.g, m_Box.color.b, 0);
		m_BoxBack.color = new Color(m_BoxBack.color.r, m_BoxBack.color.g, m_BoxBack.color.b, 0);
		m_BoxTxt.color = new Color(m_BoxTxt.color.r, m_BoxTxt.color.g, m_BoxTxt.color.b, 0);
		m_LvTxt.color = new Color(m_LvTxt.color.r, m_LvTxt.color.g, m_LvTxt.color.b, 0);
	}

	public void ExpSkip()
	{
		m_Skip = true;
		CoinInfo.Instance.LevelSetSkip(m_ScoreSave);
	}
}
