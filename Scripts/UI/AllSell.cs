using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSell : MonoBehaviour
{
	// Inspector
	[SerializeField] Text m_ExpectTxt;
	[SerializeField] ResultBox m_Result;       //결과창
	[SerializeField] GameObject m_SellBtn_box; // 매도버튼 판매창
	[SerializeField] GameObject m_GameOver_box; // 게임오버 선택창
	[SerializeField] public Image m_FadeImage;

	bool m_SellBtn;
	
	// Function
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			NoBtn();
	}
	public void Start()
	{
		m_Result.gameObject.SetActive(false);
	}
	public void ShowExpectMoney()
	{
		m_FadeImage.raycastTarget = true;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_FadeImage);
		string money = string.Format("{0:n0}", CoinInfo.Instance.SellAllCoin(true).ToString());
		m_ExpectTxt.text = "예상 체결 금액: " + "<color=#FFFFFF>" + string.Format("{0:n0}", money) + "</color>"
			+ "<color=#FFBB00>" + "(" + CoinInfo.Instance.m_BuyCoin + "코인)" + "</color>";
	}

	public void ShowExpectMoney_SellBtn()
	{
		m_FadeImage.raycastTarget = true;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_FadeImage);
		m_SellBtn = true;
		string money = string.Format("{0:n0}", CoinInfo.Instance.SellAllCoin(true).ToString());
		m_ExpectTxt.text = "예상 체결 금액: " + "<color=#FFFFFF>" + string.Format("{0:n0}", money) + "</color>"
			+ "<color=#FFBB00>" + "(" + CoinInfo.Instance.m_BuyCoin + "코인)" + "</color>";
	}
	public void OkBtn()
	{
		Time.timeScale = 0.0f;
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.5f, ref m_FadeImage);
		CoinInfo.Instance.m_SoundMgr.PlaySound(13);
		CoinInfo.Instance.SellAllCoin(false);
		gameObject.SetActive(false);
		m_Result.gameObject.SetActive(true);
		m_Result.ShowResult();
		m_SellBtn = false;
	}

	public void NoBtn()
	{
		m_FadeImage.raycastTarget = false;
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.5f, ref m_FadeImage);
		CoinInfo.Instance.m_Pause = false;
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		if (m_SellBtn)
		{
			Time.timeScale = 1.0f;
			m_SellBtn = false;
		}
		else
		{
			m_GameOver_box.SetActive(true);
		}
		gameObject.SetActive(false);
	}
}
