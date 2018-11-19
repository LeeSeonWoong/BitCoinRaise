using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour {

	// Inspector
	[SerializeField] GameObject pf_HistoryObj;
	[SerializeField] GameObject m_Content;
	[SerializeField] CoinSelect m_CoinSelect;

	List<GameObject> m_HistoryObjList = new List<GameObject>();
	
	// Function
	public void SetHistory()
	{
		for(int i = 0; i < CoinInfo.Instance.m_HistoryList.Count; i++)
		{
			MakeNewHistory(i);
		}
	}

	public void MakeNewHistory()
	{
		MakeNewHistory(CoinInfo.Instance.m_HistoryList.Count - 1);
		if (m_HistoryObjList.Count == 21)
		{
			Destroy(m_HistoryObjList[0]);
			m_HistoryObjList.RemoveAt(0);
		}
	}

	public void MakeNewHistory(int index)
	{
		if (CoinInfo.Instance.m_HistoryList[index].Month == 0)
			return;
		GameObject obj = Instantiate(pf_HistoryObj, m_Content.transform);
		HistoryInfo info = obj.GetComponent<HistoryInfo>();
		SellHistory history = CoinInfo.Instance.m_HistoryList[index];

		info.m_SellDayTxt.text = history.Year.ToString() + "/" + history.Month.ToString() + "/" + history.Day.ToString();
		info.m_SellTimeTxt.text = history.Hour.ToString() + ":" + history.Minute.ToString() + ":" + history.second.ToString();
		info.m_CoinIcon.sprite = m_CoinSelect.m_Coins[history.CoinIndex].m_Icon.sprite;
		info.m_CoinNameTxt.text = m_CoinSelect.m_Coins[history.CoinIndex].m_CoinName.text;
		info.m_CoinSymTxt.text = m_CoinSelect.m_Coins[history.CoinIndex].m_CoinSym.text;
		if (history.Money > 0)
		{
			info.m_ProfitTxt.color = new Color32(255, 0, 0, 255);
			info.m_ProfitTxt.text = "+" + string.Format("{0:n0}", history.Money) + " 원(+" + string.Format("{0:F2}", history.Profit) + "%)";
		}
		else
		{
			info.m_ProfitTxt.color = new Color32(0, 0, 255, 255);
			info.m_ProfitTxt.text = string.Format("{0:n0}", history.Money) + " 원(" + string.Format("{0:F2}", history.Profit) + "%)";
		}
		m_HistoryObjList.Add(obj);
		obj.transform.SetAsFirstSibling();
	}
	public void Exit()
	{
		gameObject.SetActive(false);
	}
}
