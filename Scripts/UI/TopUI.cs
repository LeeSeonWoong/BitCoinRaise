using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : MonoBehaviour {

	// Inspector
	[SerializeField] Text m_CurTime;
	[SerializeField] Text m_CurDay;

	[SerializeField] Text m_CurMoney;
	[SerializeField] Text m_BuyMoney;
	[SerializeField] Text m_BuyCoin;

	[SerializeField] Text m_CurValueMoney; // 평가금액
	[SerializeField] Text m_CurValueRate;  // 평가손익
	[SerializeField] Text m_ProfitRate;    // 수익률

	//[SerializeField] Result m_Result;

	[HideInInspector] public double ProfitRate;
	string m_Day;
	int m_Hour;
	string m_HourS;
	int m_Min;
	string m_MinS;
	int m_Month;
	string m_MonthS;
	int m_MonthOfDay;
	string m_MonthOfDayS;
	int m_Week;
	string m_WeekS;
	bool once;

	// Function
	private void Start()
	{
		once = false;
	}
	private void Update()
	{
		if (Time.timeScale != 0.0f)
		{
			//////////////////////////////////////////////////////////
			// 금액 Update ///////////////////////////////////////////
			//////////////////////////////////////////////////////////
			m_CurMoney.text = string.Format("{0:n0}", CoinInfo.Instance.m_CurMoney);
			m_BuyMoney.text = string.Format("{0:n0}", CoinInfo.Instance.m_BuyCoinMoney);
			m_BuyCoin.text = CoinInfo.Instance.m_BuyCoin.ToString() + " 코인";

			m_CurValueMoney.text = string.Format("{0:n0}", CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum] * CoinInfo.Instance.m_BuyCoin);
			double CurValueRate = CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum] * CoinInfo.Instance.m_BuyCoin - CoinInfo.Instance.m_BuyCoinMoney;
			if (CurValueRate < 0)
				m_CurValueRate.color = new Color32(50, 50, 250, 255);
			else
				m_CurValueRate.color = new Color32(250, 50, 50, 255);
			m_CurValueRate.text = string.Format("{0:n0}", CurValueRate);
			ProfitRate = CurValueRate / CoinInfo.Instance.m_BuyCoinMoney * 100;
			if (CurValueRate == 0)
				ProfitRate = 0;
			m_ProfitRate.text = string.Format("{0:F2}", ProfitRate) + "%";
			if (ProfitRate < 0)
			{
				m_ProfitRate.color = new Color32(50, 50, 250, 255);
			}
			else
			{
				m_ProfitRate.color = new Color32(250, 50, 50, 255);
			}

			//////////////////////////////////////////////////////////
			// 시간 Update ///////////////////////////////////////////
			//////////////////////////////////////////////////////////
			if (once)
			{
				if (m_Min != System.DateTime.Now.Minute)
					once = false;
			}
			else if (once == false)
			{
				once = true;
				m_Hour = System.DateTime.Now.Hour;
				m_Min = System.DateTime.Now.Minute;
				m_Week = (int)System.DateTime.Now.DayOfWeek;
				m_Month = System.DateTime.Now.Month;
				m_MonthOfDay = System.DateTime.Now.Day;

				// 오전 / 오후
				if (m_Hour > 12)
				{
					m_Hour -= 12;
					m_Day = "오후 ";
				}
				else
					m_Day = "오전 ";

				// 시간
				if (m_Hour < 10)
					m_HourS = "0" + m_Hour.ToString() + ":";
				else
					m_HourS = m_Hour.ToString() + ":";

				// 분
				if (m_Min < 10)
					m_MinS = "0" + m_Min.ToString();
				else
					m_MinS = m_Min.ToString();

				// 월
				if (m_Month < 10)
					m_MonthS = "0" + m_Month.ToString() + ".";
				else
					m_MonthS = m_Month.ToString() + ".";

				// 일
				if (m_MonthOfDay < 10)
					m_MonthOfDayS = "0" + m_MonthOfDay.ToString() + " ";
				else
					m_MonthOfDayS = m_MonthOfDay.ToString() + " ";

				// 요일
				switch (m_Week)
				{
					case 1:
						m_WeekS = "월";
						break;
					case 2:
						m_WeekS = "화";
						break;
					case 3:
						m_WeekS = "수";
						break;
					case 4:
						m_WeekS = "목";
						break;
					case 5:
						m_WeekS = "금";
						break;
					case 6:
						m_WeekS = "토";
						break;
					case 7:
						m_WeekS = "일";
						break;
				}

				m_CurTime.text = m_Day + m_HourS + m_MinS;
				m_CurDay.text = m_MonthS + m_MonthOfDayS + m_WeekS;
			}
		}
	}
}
