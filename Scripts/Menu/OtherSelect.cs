using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherSelect : MonoBehaviour {

	// Inspector
	[SerializeField] GameObject m_HistoryField;
	[SerializeField] GameObject m_HelpField;

	History m_History;

	// Function
	private void Awake()
	{
		m_History = m_HistoryField.GetComponent<History>();
		m_HistoryField.SetActive(false);
		m_History.SetHistory();
	}
	public void HistoryClick()
	{
		m_HistoryField.SetActive(true);
	}

	public void HistoryUpdate()
	{
		m_History.MakeNewHistory();
	}
	
	// 도움말 버튼 클릭
	public void HelpClick()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_HelpField.SetActive(true);
	}

	public void HelpOff()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		m_HelpField.SetActive(false);
	}
}
