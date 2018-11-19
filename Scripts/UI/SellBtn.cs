using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellBtn : MonoBehaviour
{

	// Inspector
	[SerializeField] GameObject m_CheckBox;
	[SerializeField] Image m_FadeImage;
	[SerializeField] AllSell m_AllSell;
	bool m_OnClick;
	float m_Timer;
	bool m_AllSellCheck;

	// Function
	private void Awake()
	{
		m_CheckBox.SetActive(false);
	}
	public void GameStart()
	{
		m_FadeImage.raycastTarget = false;
		m_OnClick = false;
		m_Timer = 0.0f;
	}

	public void Update()
	{
		if (m_OnClick)
		{
			m_Timer += Time.fixedDeltaTime;
			if (m_Timer >= 1.0f)
			{
				m_OnClick = false;
				m_Timer = 0.0f;
				OKbtn();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape) && m_CheckBox.active)
			NoBtn();
	}
	public void BtnClick()
	{
		if (CoinInfo.Instance.m_GameOver == false)
		{
			if (m_OnClick == false)
				m_OnClick = true;
		}
	}
	public void BtnUp()
	{
		if (CoinInfo.Instance.m_GameOver == false)
		{
			if (m_AllSell.gameObject.active == false)
			{
				m_OnClick = false;
				SellBtnClick();
				m_Timer = 0.0f;
			}
		}
	}

	public void SellBtnClick()
	{
		if (CoinInfo.Instance.m_BuyCoin > 1 && CoinInfo.Instance.m_BuyManMgr.m_Player.m_ReverseOn == false && CoinInfo.Instance.m_BuyManMgr.m_Player.StateCheck())
		{
			CoinInfo.Instance.m_BuyManMgr.m_Player.DownOneFloor();
			CoinInfo.Instance.m_SellBuyNum++;
			CoinInfo.Instance.SellCoin();
			if (CoinInfo.Instance.m_Goods.m_ActiveCheck[4] == false)
				StartCoroutine("SellBtnMakeSellMan");
			else
			{
				if (Random.Range(0, 2) == 1)
					CoinInfo.Instance.m_BuyManMgr.StartCoroutine("MakeBuyManC");
			}
			CoinInfo.Instance.m_SoundMgr.PlaySound(7);
		}
		else if (CoinInfo.Instance.m_BuyCoin == 1 && m_CheckBox.active == false && CoinInfo.Instance.m_BuyManMgr.m_Player.m_ReverseOn == false) // 현재 보유 코인이 1개 미만 == 1개 인 경우
		{
			CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_FadeImage);
			m_FadeImage.raycastTarget = true;
			m_CheckBox.SetActive(true);
			CoinInfo.Instance.m_SoundMgr.PlaySound(7);
		}
	}

	IEnumerator SellBtnMakeSellMan()
	{
		yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
		CoinInfo.Instance.m_SellManMgr.MakeSellMan();
	}

	public void OKbtn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(13);
		m_CheckBox.SetActive(false);
		m_AllSell.gameObject.SetActive(true);
		m_AllSell.ShowExpectMoney_SellBtn();
	}

	public void NoBtn()
	{
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.5f, ref m_FadeImage);
		m_FadeImage.raycastTarget = false;
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		m_CheckBox.SetActive(false);
	}
}
