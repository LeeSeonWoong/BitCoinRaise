using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBox : MonoBehaviour
{
	// Inspector
	[SerializeField] Image m_Fade;
	[SerializeField] Image[] m_white;
	[SerializeField] Text m_OkBtn;

	[SerializeField] CocosFunc m_Function;
	[SerializeField] MenuControl m_Menu;
	[SerializeField] GameObject m_AllSell;

	[SerializeField] Exp m_Exp;                 // 경험치
	[SerializeField] ResultInfo m_Info;			// 거래정보
	[SerializeField] Text m_MoneyTxt;           // 시작돈 -> 현재돈

	private bool m_GameOverSetCheck;

	// Function
	public void ShowResult()
	{
		CoinInfo.Instance.m_BuyManMgr.m_Gazua.GazuaStop();
		CoinInfo.Instance.m_SoundMgr.StopAllSound();
		m_Info.InfoUpdate();
		m_Exp.ExpUpdate(m_Info.m_Score);
		StartCoroutine("FadeBack");
	}

	IEnumerator FadeBack()
	{
		m_Function.ImageFadeIn(1.0f, ref m_Fade);
		yield return new WaitForSecondsRealtime(1.1f);
		for (int i = 0; i < m_white.Length; i++)
			m_Function.ImageFadeIn(1.0f, ref m_white[i]);
		m_Function.TextFadeIn(1.0f, 1.0f, ref m_OkBtn);

		StartCoroutine("GameOverSetC");

		yield return new WaitForSecondsRealtime(1.0f);
		m_Info.StartCoroutine("MakeStarAndGrade");

		yield return new WaitForSecondsRealtime(1.0f);
		m_Function.TextFadeIn(0.5f, 1.0f, ref m_MoneyTxt);
		m_MoneyTxt.text = string.Format("{0:n0}", CoinInfo.Instance.m_StartMoney) + " 원" + " → " + string.Format("{0:n0}", CoinInfo.Instance.m_CurMoney) + " 원";

		yield return new WaitForSecondsRealtime(0.5f);
		// 거래내역 + 점수
		m_Info.ShowResult();
		// 경험치
		m_Exp.ExpActive(0.5f);
	}

	IEnumerator GameOverSetC()
	{
		yield return new WaitForSecondsRealtime(0.8f);
		if (m_GameOverSetCheck == false)
		{
			CoinInfo.Instance.m_BuyManMgr.BuyManDelete();
			CoinInfo.Instance.m_SellManMgr.SellManDelete();
			CoinInfo.Instance.m_BitCoinMgr.DeleteAllCoin();
			m_AllSell.SetActive(false);
			m_GameOverSetCheck = true;
		}
	}
	public void GameOverSet()
	{
		CoinInfo.Instance.m_BuyManMgr.BuyManDelete();
		CoinInfo.Instance.m_SellManMgr.SellManDelete();
		CoinInfo.Instance.m_BitCoinMgr.DeleteAllCoin();
		m_AllSell.SetActive(false);
		m_GameOverSetCheck = true;
	}
	public void OKbtn()
	{
		CoinInfo.Instance.SaveHistory(m_Info.m_ProfitRate);
		if (m_GameOverSetCheck == false)
			GameOverSet();
		m_Exp.ExpSkip();
		CoinInfo.Instance.CoinPriceUpdate_ByGame();
		CoinInfo.Instance.Save();
		m_Menu.GameExit();
		ResultInit();
		gameObject.SetActive(false);
		CoinInfo.Instance.m_SoundMgr.StopAllSound();
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
	}

	public void ResultInit()
	{
		m_GameOverSetCheck = false;
		m_Fade.color = new Color(m_Fade.color.r, m_Fade.color.g, m_Fade.color.b, 0.0f);
		m_Exp.Init();
		m_Info.Init();
		m_MoneyTxt.color = new Color(m_MoneyTxt.color.r, m_MoneyTxt.color.g, m_MoneyTxt.color.b, 0.0f);
		for (int i = 0; i < 3; i++)
			m_white[i].color = new Color(m_white[i].color.r, m_white[i].color.g, m_white[i].color.b, 0.0f);
		m_OkBtn.color = new Color(m_OkBtn.color.r, m_OkBtn.color.g, m_OkBtn.color.b, 0.0f);
	}
}
