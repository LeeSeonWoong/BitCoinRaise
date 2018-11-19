using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
	// 싱글턴 구축
	private static LevelInfo _instance = null;

	public static LevelInfo Instance
	{
		get
		{
			if (_instance == null)
				_instance = new LevelInfo();
			return _instance;
		}
	}

	// 플레이어
	public float m_PlayerSpeed;
	public float m_BonusBuyMan;
	// 매수맨
	public float m_BuyManSpeed;
	public float m_BuyManSpawnTime;
	public int m_BuyManSellResist;
	// 매도맨
	public int m_SellManHP;
	public float m_SellManDmg;
	public float m_SellManFallSpeed;
	public float m_SellManSpawnTime;
	public int m_SellManSpawnBonus;
	public int m_SellManBuyResist;
	// 가즈아
	public float m_GazuaTime;
	public int m_GazuaStartNum;
	public int m_GazuaEndNum;
}

