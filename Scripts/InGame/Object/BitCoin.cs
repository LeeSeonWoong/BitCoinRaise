using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BitCoin : MonoBehaviour {

	// Inspector
	SpriteRenderer m_SpriteRender;
	Animator m_Animator;

	[HideInInspector] public float m_HP;       // 코인 HP
	int m_CoinNum;  // 코인 이미지번호 ( 1, 2 )
	int m_CoinKind;	// 코인 종류
	int m_Price;    // 코인 가격
	[HideInInspector] public int m_Index;	// 코인 인덱스

	Sprite m_Sprite;

	private string spriteName1 = "CoinA";
	private string spriteName2 = "CoinB";
	private string CoinA_Damage1 = "CoinA_Damage1";
	private string CoinA_Damage2 = "CoinA_Damage2";
	private string CoinB_Damage1 = "CoinB_Damage1";
	private string CoinB_Damage2 = "CoinB_Damage2";

	// Function
	private void Awake()
	{
		m_HP = 3;	// 내구도
		m_SpriteRender = GetComponent<SpriteRenderer>();
		m_Animator = GetComponent<Animator>();
		m_Animator.enabled = false;
	}

	public void SetCoinImage(int CoinNum, int indexNum)
	{
		m_Index = indexNum;
		m_CoinNum = CoinNum;
		if (CoinNum == 0)//	Type A
		{
			m_Sprite = Resources.Load<Sprite>(spriteName1);
			m_SpriteRender.sprite = m_Sprite;
		}
		else if(CoinNum == 1)// Type B
		{
			m_Sprite = Resources.Load<Sprite>(spriteName2);
			m_SpriteRender.sprite = m_Sprite;
		}
	}

	public void CoinAttacked()
	{
		if (gameObject == CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count-1])
		{
			if (m_HP > 0)
			{
				m_HP -= LevelInfo.Instance.m_SellManDmg;
				SetImage();
				if (m_HP <= 0)
				{
					DeleteAni();
				}
			}
		}
	}

	public void DeleteAni()
	{
		if (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex % 5 == 0)
			TagDelete();
		CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Remove(gameObject);
		CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex -= 1;
		CoinInfo.Instance.m_MaxFloor -= 1;
		m_Animator.enabled = true;
	}
	public void CoinDelete()
	{
		Destroy(gameObject);
		CoinInfo.Instance.CoinBreak();
		CoinInfo.Instance.m_BitCoinMgr.ShowTag();
	}
	public void TagDelete()
	{
		Destroy(CoinInfo.Instance.m_BitCoinMgr.m_TagList[CoinInfo.Instance.m_BitCoinMgr.m_TagList.Count - 1].gameObject);
		CoinInfo.Instance.m_BitCoinMgr.m_TagList.RemoveAt(CoinInfo.Instance.m_BitCoinMgr.m_TagList.Count - 1);
	}
	public void SetImage()
	{
		// 이미지 세팅
		if(m_HP == 2)
		{
			if (m_CoinNum == 0)//
				m_Sprite = Resources.Load<Sprite>(CoinA_Damage1);
			else
				m_Sprite = Resources.Load<Sprite>(CoinB_Damage1);
			m_SpriteRender.sprite = m_Sprite;
		}
		else if(m_HP == 1)
		{
			if (m_CoinNum == 0)//
				m_Sprite = Resources.Load<Sprite>(CoinA_Damage2);
			else
				m_Sprite = Resources.Load<Sprite>(CoinB_Damage2);
			m_SpriteRender.sprite = m_Sprite;
		}
	}
}
