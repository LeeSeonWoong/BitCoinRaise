using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacoonBtn : MonoBehaviour {

	// Inspecotr
	[SerializeField] Text m_Limit_Text;
	[SerializeField] Image m_BlackImg;
	[SerializeField] RacoonMan m_RacoonMan;

	// Function
	private void Start()
	{
		if (CoinInfo.Instance.m_RacoonNum != 0)
			m_BlackImg.enabled = false;
	}

	public void RacoonManCall()
	{
		if (CoinInfo.Instance.m_RacoonNum != 0 && m_RacoonMan.m_Active == false)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(6);
			m_BlackImg.enabled = true;
			CoinInfo.Instance.m_RacoonNum--;
			m_RacoonMan.RacoonManActive();
			StartCoroutine("CheckBlack");
		}
	}

	private void Update()
	{
		m_Limit_Text.text = "사용가능:" + CoinInfo.Instance.m_RacoonNum.ToString();
	}

	IEnumerator CheckBlack()
	{
		yield return new WaitForSeconds(2.05f);
		if (CoinInfo.Instance.m_RacoonNum != 0)
		{
			m_BlackImg.enabled = false;
		}
	}
}
