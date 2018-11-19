using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour {

	// Inspector
	[SerializeField] Image m_RedGauge;
	[SerializeField] Image m_BlueGauge;
	[SerializeField] Text m_BuyText;
	[SerializeField] Text m_SellText;


	// Function
	private void Update()
	{
		SetGauge();	
	}
	void SetGauge()
	{
		float redNum = CoinInfo.Instance.m_BuyManMgr.m_BuyMan_List.Count;
		float blueNum = CoinInfo.Instance.m_SellManMgr.m_SellMan_List.Count;
		m_BuyText.text = redNum.ToString();
		m_SellText.text = blueNum.ToString();
		float rate = redNum / (redNum + blueNum);
		if (Mathf.Abs(m_RedGauge.fillAmount - rate) > 0.1f)
		{
			if(m_RedGauge.fillAmount > rate)
				m_RedGauge.fillAmount -= Time.deltaTime*0.2f;
			else
				m_RedGauge.fillAmount += Time.deltaTime*0.2f;
		}
		else
		{
			if (m_RedGauge.fillAmount > rate)
				m_RedGauge.fillAmount -= Time.deltaTime * 0.2f;
			else
				m_RedGauge.fillAmount += Time.deltaTime * 0.2f;
		}
	}


}
