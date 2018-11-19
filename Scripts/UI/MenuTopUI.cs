using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTopUI : MonoBehaviour {

	// Inspector
	[SerializeField] Text m_CurTime;
	[SerializeField] Text m_CurDay;

	// 플레이어 탭
	[SerializeField] Text m_Level;
	[SerializeField] Text m_CurMoney;
	[SerializeField] Text m_NugulManNum;
	[SerializeField] Text m_PlayerName;

	bool once;
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

	// Function
	private void Start()
	{
		once = false;
	}

	public void PlayerUpdate()
	{
		m_PlayerName.text = CoinInfo.Instance.m_ID;
		m_Level.text = CoinInfo.Instance.m_Lv.ToString();
		m_CurMoney.text = string.Format("{0:n0}", CoinInfo.Instance.m_CurMoney) + " 원";
		m_NugulManNum.text = CoinInfo.Instance.m_RacoonNum.ToString() + " 매"; ;
	}

	private void Update()
	{
		PlayerUpdate();
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
