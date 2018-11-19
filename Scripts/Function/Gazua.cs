using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazua : MonoBehaviour {

	// Inspector
	[HideInInspector] public float m_Accel;
	[HideInInspector] public float m_ActiveTime;
	[HideInInspector] public bool m_MadKing;

	bool m_OnTime;
	bool m_Cool;

	// Function
	private void Start()
	{
		GameStart();
	}
	public void GameStart()
	{
		m_Accel = 1.0f;
		m_OnTime = false;
		m_Cool = false;
		m_MadKing = false;
		m_ActiveTime = 0.0f;
	}
	public void GazuaCheck(int BuyManNum)
	{
		if (m_Cool == false)
		{
			if (m_OnTime == false)
			{
				if (BuyManNum >= LevelInfo.Instance.m_GazuaStartNum && CoinInfo.Instance.m_GazuaActive == false && CoinInfo.Instance.m_GameOver == false)
				{
					m_Accel = 0.6f;
					CoinInfo.Instance.m_GazuaActive = true;
					CoinInfo.Instance.m_BuyManMgr.GazuaOn();
					StartCoroutine("GazuaKeepTime");
				}
			}
			else
			{
				if (BuyManNum >= LevelInfo.Instance.m_GazuaEndNum)
				{ return; }
				else
				{
					GazuaEnd();
				}
			}
		}
	}

	public void GazuaEnd()
	{
		m_Accel = 1.0f;
		CoinInfo.Instance.m_GazuaActive = false;
		m_OnTime = false;
		m_Cool = true;
		CoinInfo.Instance.m_BuyManMgr.GazuaOff();
		StartCoroutine("GazuaCoolTime");
	}
	public void GazuaStop()
	{
		m_Accel = 1.0f;
		m_OnTime = true;
		m_Cool = true;
		CoinInfo.Instance.m_GazuaActive = false;
		CoinInfo.Instance.m_BuyManMgr.m_Player.GazuaActive(false);
		CoinInfo.Instance.m_BuyManMgr.m_Player.m_Animator.speed = LevelInfo.Instance.m_PlayerSpeed;
	}

	// 가즈아 기본 지속시간
	IEnumerator GazuaKeepTime()
	{
		if(CoinInfo.Instance.m_Goods.m_ActiveCheck[0] == false)
			yield return new WaitForSecondsRealtime(LevelInfo.Instance.m_GazuaTime + CoinInfo.Instance.m_Skills[3]);
		else
			yield return new WaitForSecondsRealtime((LevelInfo.Instance.m_GazuaTime + CoinInfo.Instance.m_Skills[3]) * 1.5f );
		m_OnTime = true;
	}

	IEnumerator GazuaCoolTime()
	{
		yield return new WaitForSecondsRealtime(10.0f);
		m_Cool = false;
	}
}
