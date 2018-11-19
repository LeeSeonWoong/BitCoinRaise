using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtn : MonoBehaviour
{

	// Inspector
	[SerializeField] Text m_BuyLimit_Text;
	[SerializeField] Image m_BlackImg;
	[SerializeField] NoMoney m_NoMoney;
	[SerializeField] NoMoney m_NoBuy;

	int m_MaxBuy;
	int m_CurBuy;
	float m_Delay;
	bool m_UpdateFill;
	bool m_ActiveCheck;

	// Function
	private void Start()
	{
		GameStart();
	}

	public void GameStart()
	{
		m_ActiveCheck = false;
		m_UpdateFill = false;
		m_CurBuy = CoinInfo.Instance.m_Skills[0];
		m_MaxBuy = 10 + CoinInfo.Instance.m_Skills[1];
		m_BlackImg.fillAmount = 0.0f;
		CheckBuyLimit();
	}
	private void Update()
	{
		if (CoinInfo.Instance.m_Goods.m_ActiveCheck[5] && m_ActiveCheck == false)
		{
			m_ActiveCheck = true;
			m_BlackImg.fillAmount = 0.0f;
			m_UpdateFill = false;
			m_CurBuy += 1;
			CheckBuyLimit();
		}
		if (m_UpdateFill)
		{
			if(CoinInfo.Instance.m_Goods.m_ActiveCheck[2])
				m_BlackImg.fillAmount -= Time.deltaTime * 2.0f;
			else
				m_BlackImg.fillAmount -= Time.deltaTime;

			if (m_BlackImg.fillAmount <= 0)
			{
				m_BlackImg.fillAmount = 0.0f;
				m_UpdateFill = false;
				m_CurBuy += 1;
				CheckBuyLimit();
			}
		}
		m_BuyLimit_Text.text = "주문가능:" + m_CurBuy.ToString();
	}

	void CheckBuyLimit()
	{
		if (CoinInfo.Instance.m_Goods.m_ActiveCheck[5])
		{
			if (m_CurBuy < (10 + CoinInfo.Instance.m_Skills[1]) * 2)
			{
				m_BlackImg.fillAmount = 1;
				m_UpdateFill = true;
			}
		}
		else
		{
			if (m_CurBuy < 10 + CoinInfo.Instance.m_Skills[1])
			{
				m_BlackImg.fillAmount = 1;
				m_UpdateFill = true;
			}
		}
	}

	public bool BuyCheck()
	{
		if (m_CurBuy > 0)
		{
			m_CurBuy -= 1;
			if (CoinInfo.Instance.m_Goods.m_ActiveCheck[5])
			{
				if (m_CurBuy == ((10 + CoinInfo.Instance.m_Skills[1]) * 2) - 1)
					CheckBuyLimit();
			}
			else
			{
				if (m_CurBuy == 10 + CoinInfo.Instance.m_Skills[1] - 1)
					CheckBuyLimit();
			}
			return true;
		}
		else
		{
			CurNoBuy();
			CoinInfo.Instance.m_SoundMgr.PlaySound(4);
			return false;
		}
	}

	public void CurNoMoney()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(5);
		m_NoMoney.AnimatorOn();
	}

	public void CurNoBuy()
	{
		m_NoBuy.AnimatorOn();
	}
}
