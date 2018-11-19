using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultInfo : MonoBehaviour {

	// Inspector /////////////////////////////////////////////////////////////

	//왼쪽 정보명 목록
	[SerializeField] Text[] m_GreyTxt;              // 정보타이틀들

	//오른쪽 실 갱신 정보들(텍스트)
	[SerializeField] Text m_StarTxt;                // 별
	[SerializeField] Text m_RateTxt;				// 등급
	[SerializeField] Text m_CoinNumTxt;				// 쌓은코인
	[SerializeField] Text m_ProfitTxt;              // 실현손익
	[SerializeField] Text m_ProfitRateTxt;          // 수익률
	[SerializeField] Text m_TitleNumTxt;            // 타이틀(가호)
	[SerializeField] Text[] m_Titles;                 // 타이틀들
	[SerializeField] Text m_ScoreTxt;				// 점수

	[HideInInspector] public int m_Score;			// 점수
	[HideInInspector] public double m_Profit;        // 실현손익
	[HideInInspector] public double m_ProfitRate;								// 수익률
	int m_TitleNum;                                 // 타이틀 개수
	string m_Floor;									// 등급
	int m_StarNum;									// 별 갯수

	// Function /////////////////////////////////////////////////////////////
	// 초기화
	public void Init()
	{
		m_StarNum = 0;
		m_TitleNum = 0;
		m_Profit = 0;
		m_ProfitRate = 0;
		m_RateTxt.color = new Color(m_RateTxt.color.r, m_RateTxt.color.g, m_RateTxt.color.b, 0.0f);
		for (int i = 0; i < m_GreyTxt.Length; i++)
			m_GreyTxt[i].color = new Color(m_GreyTxt[i].color.r, m_GreyTxt[i].color.g, m_GreyTxt[i].color.b, 0.0f);
		m_StarTxt.text = "";
		m_CoinNumTxt.color = new Color(m_CoinNumTxt.color.r, m_CoinNumTxt.color.g, m_CoinNumTxt.color.b, 0.0f);
		m_ProfitTxt.color = new Color(m_ProfitTxt.color.r, m_ProfitTxt.color.g, m_ProfitTxt.color.b, 0.0f);
		m_ProfitRateTxt.color = new Color(m_ProfitRateTxt.color.r, m_ProfitRateTxt.color.g, m_ProfitRateTxt.color.b, 0.0f);
		m_TitleNumTxt.color = new Color(m_TitleNumTxt.color.r, m_TitleNumTxt.color.g, m_TitleNumTxt.color.b, 0.0f);
		for(int i = 0; i < m_Titles.Length; i++)
			m_Titles[i].color = new Color32(150, 150, 150, 0);
		m_ScoreTxt.color = new Color(m_ScoreTxt.color.r, m_ScoreTxt.color.g, m_ScoreTxt.color.b, 0.0f);
	}
	// 정보 갱신
	public void InfoUpdate()
	{
		m_ProfitRate = (CoinInfo.Instance.m_CurMoney / CoinInfo.Instance.m_StartMoney) * 100.0f - 100;
		if (m_ProfitRate < 0)
			m_ProfitRateTxt.color = new Color32(50, 50, 250, 0);
		else
			m_ProfitRateTxt.color = new Color32(250, 50, 50, 0);
		m_Profit = CoinInfo.Instance.m_CurMoney - CoinInfo.Instance.m_StartMoney;
		if (m_Profit < 0)
			m_ProfitTxt.color = new Color32(50, 50, 250, 0);
		else
			m_ProfitTxt.color = new Color32(250, 50, 50, 0);

		// 타이틀(가호) /////////////////////////////////////////////////////////
		if (CoinInfo.Instance.m_SellBuyNum >= 500)
		{
			m_Titles[0].text = "단타신";
			m_Titles[0].color = new Color32(248, 187, 29, 0);
			m_TitleNum++;
		}
		else
			m_Titles[0].text = "단타신";
		if (CoinInfo.Instance.m_GameOverNum == 0)
		{
			m_Titles[1].text = "익절의 마술사";
			m_Titles[1].color = new Color32(248, 187, 29, 0);
			m_TitleNum++;
		}
		else
			m_Titles[1].text = "익절의 마술사";
		if (m_ProfitRate >= 500)
		{
			m_Titles[2].text = "코렌 버핏";
			m_Titles[2].color = new Color32(248, 187, 29, 0);
			m_TitleNum++;
		}
		else
			m_Titles[2].text = "코렌 버핏";
		if (CoinInfo.Instance.m_BuyManMgr.m_Gazua.m_MadKing)
		{
			m_Titles[3].text = "매드킹";
			m_Titles[3].color = new Color32(248, 187, 29, 0);
			m_TitleNum++;
		}
		else
			m_Titles[3].text = "매드킹";

		m_Score = (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex - 14) + (int)(m_Profit / 10000) + (int)m_ProfitRate + (m_TitleNum * m_TitleNum) * 100;
		if (m_Score < 0)
			m_Score = 0;
		if (m_Score >= 0 && m_Score <= 49)
		{
			m_Floor = "\"이게 매매냐급 베팅\"";
			m_StarNum = 1;
		}
		else if (m_Score >= 50 && m_Score <= 149)
		{
			m_Floor = "\"매매 조무사급 배팅\"";
			m_StarNum = 2;
		}
		else if (m_Score >= 150 && m_Score <= 349)
		{
			m_Floor = "\"코린이급 베팅\"";
			m_StarNum = 3;
		}
		else if (m_Score >= 350 && m_Score <= 749)
		{
			m_Floor = "\"코인딜러급 베팅\"";
			m_StarNum = 4;
		}
		else if (m_Score >= 750 && m_Score <= 1549)
		{
			m_Floor = "\"거대 세력급 베팅\"";
			m_StarNum = 5;
		}
		else if (m_Score >= 1550 && m_Score <= 3149)
		{
			m_Floor = "\"자존심을 건 역대급 베팅\"";
			m_StarNum = 6;
		}
		else if (m_Score >= 3150)
		{
			m_Floor = "\"코인의 1인자급 베팅\"";
			m_StarNum = 7;
		}
	}
	// 정보 나타내기
	public void ShowResult()
	{
		CocosFunc m_Function = CoinInfo.Instance.m_CocosFunc;
		for (int i = 0; i < m_GreyTxt.Length; i++)
			m_Function.TextFadeIn(0.5f, 0.5f, ref m_GreyTxt[i]);
		m_Function.TextFadeIn(0.5f, 1.0f, ref m_CoinNumTxt);
		m_CoinNumTxt.text = (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex - 14).ToString() + "개";

		m_Function.TextFadeIn(0.5f, 1.0f, ref m_ProfitTxt);
		m_ProfitTxt.text = "₩" + string.Format("{0:n0}", m_Profit);

		m_Function.TextFadeIn(0.5f, 1.0f, ref m_ProfitRateTxt);
		m_ProfitRateTxt.text = string.Format("{0:F2}", m_ProfitRate) + "%";

		m_Function.TextFadeIn(0.5f, 1.0f, ref m_TitleNumTxt);
		m_TitleNumTxt.text = m_TitleNum.ToString();

		for(int i = 0; i < m_Titles.Length; i++)
			m_Function.TextFadeIn(0.5f, 1.0f, ref m_Titles[i]);

		m_Function.TextFadeIn(0.5f, 1.0f, ref m_ScoreTxt);
		m_ScoreTxt.text = m_Score.ToString();
	}
	// 별 생성
	IEnumerator MakeStarAndGrade()
	{
		for (int i = 0; i < m_StarNum; i++)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(8);
			m_StarTxt.text += "★ ";
			yield return new WaitForSecondsRealtime(0.2f);
		}
		CoinInfo.Instance.m_SoundMgr.PlaySound(9);

		CoinInfo.Instance.m_CocosFunc.TextFadeIn(0.5f, 1.0f, ref m_RateTxt);
		m_RateTxt.text = m_Floor;
	}
	
}
