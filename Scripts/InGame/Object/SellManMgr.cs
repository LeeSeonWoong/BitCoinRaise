using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManMgr : MonoBehaviour {

	// Inspector
	[SerializeField] GameObject pf_SellMan; // 매도맨
	[SerializeField] GameObject pf_SellMan_Sea; // 해양 매도맨
	[HideInInspector] public List<GameObject> m_SellMan_List = new List<GameObject>();
	int m_Level;
	bool m_FlipX;


	// Function
	private void Awake()
	{
		CoinInfo.Instance.m_SellManMgr = this;
	}

	public void GameStart()
	{
		m_Level = 0;
		m_SellMan_List.Clear();
		m_FlipX = false;
		StartCoroutine("SpawnLevelUp");
		StartCoroutine("SpawnSellMan");
		StartCoroutine("Bubble");
	}

	// 거품
	IEnumerator Bubble()
	{
		yield return new WaitForSeconds(CoinInfo.Instance.m_CoinProperty[CoinInfo.Instance.m_SelectCoinNum].Bubble);
		StartCoroutine("CreateSellMan");
		StartCoroutine("Bubble");
	}

	IEnumerator SpawnLevelUp()
	{
		if (CoinInfo.Instance.m_GameOver == false)
		{
			m_Level++;
			yield return new WaitForSeconds(CoinInfo.Instance.m_CoinProperty[CoinInfo.Instance.m_SelectCoinNum].Bubble);
			StartCoroutine("SpawnLevelUp");
		}
	}

	
    IEnumerator SpawnSellMan()
    {
        if (CoinInfo.Instance.m_GameOver == false)
        {
            StartCoroutine("CreateSellMan");
            int limit = m_Level;
			int Resist = 0;
			// 매수저항
			if (LevelInfo.Instance.m_SellManBuyResist == 0)
				Resist = CoinInfo.Instance.m_BuyManMgr.m_BuyMan_List.Count / CoinInfo.Instance.m_CoinProperty[CoinInfo.Instance.m_SelectCoinNum].SellResist;
			else
				Resist = LevelInfo.Instance.m_SellManBuyResist;
			limit += Resist;
			// 매도벽
			if (SellWallCheck() && LevelInfo.Instance.m_SellManSpawnBonus == 0) // 매도벽 체크 = 9/9/9층체크
				limit += CoinInfo.Instance.m_CoinProperty[CoinInfo.Instance.m_SelectCoinNum].SellWall;
			else
				limit += LevelInfo.Instance.m_SellManSpawnBonus;
			for (int i = 0; i < limit; ++i)
                StartCoroutine("CreateSellMan");
            yield return new WaitForSeconds(LevelInfo.Instance.m_SellManSpawnTime);
            StartCoroutine("SpawnSellMan");
        }
    }

	IEnumerator CreateSellMan()
	{
		yield return new WaitForSeconds( Random.Range(0.0f, 2f));
		m_FlipX = Random.Range(0.0f, 1.0f) < 0.5f ? true : false;
		if (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex <= 220)
			MakeSellManSea();
		else
			MakeSellManNormal();
	}

	public void MakeSellMan()
	{
		m_FlipX = Random.Range(0.0f, 1.0f) < 0.5f ? true : false;
		if (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex <= 220)
			MakeSellManSea();
		else
			MakeSellManNormal();
	}

	public void MakeSellManSea()
	{
		float Coin_y = CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].transform.position.y;
		GameObject obj;//22~45
		if (Camera.main.transform.position.y - Coin_y > 3.3f)
		{
			obj = Instantiate(pf_SellMan_Sea, new Vector3(4.15f, Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
			obj.GetComponent<SpriteRenderer>().flipX = true;
            obj.GetComponent<SellMan>().SeaType = true;
            m_SellMan_List.Add(obj);
		}
		else
		{
			if (m_FlipX)
			{
				obj = Instantiate(pf_SellMan_Sea, new Vector3(Random.Range(5.6f, 6.6f), Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
				obj.GetComponent<SpriteRenderer>().flipX = true;
                obj.GetComponent<SellMan>().SeaType = true;
                m_SellMan_List.Add(obj);
			}
			else if (m_FlipX == false)
			{
				obj = Instantiate(pf_SellMan_Sea, new Vector3(Random.Range(0.6f, 1.6f), Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
				obj.GetComponent<SpriteRenderer>().flipX = false;
                obj.GetComponent<SellMan>().SeaType = true;
                m_SellMan_List.Add(obj);
			}
		}

	}
	public void MakeSellManNormal()
	{
		float Coin_y = CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].transform.position.y;
		GameObject obj;//22~45
		if (Camera.main.transform.position.y - Coin_y > 3.3f)
		{
			obj = Instantiate(pf_SellMan, new Vector3(4.15f, Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
			obj.GetComponent<SpriteRenderer>().flipX = true;
			m_SellMan_List.Add(obj);
		}
		else
		{
			if (m_FlipX)
			{
				obj = Instantiate(pf_SellMan, new Vector3(Random.Range(5.6f, 6.6f), Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
				obj.GetComponent<SpriteRenderer>().flipX = true;
				m_SellMan_List.Add(obj);
			}
			else if (m_FlipX == false)
			{
				obj = Instantiate(pf_SellMan, new Vector3(Random.Range(0.6f, 1.6f), Camera.main.transform.position.y + 7.0f, -3.0f), Quaternion.identity);
				obj.GetComponent<SpriteRenderer>().flipX = false;
				m_SellMan_List.Add(obj);
			}
		}
	}
	// 매도맨 리스트 삭제
	public void SellManDelete()
	{
		int num = m_SellMan_List.Count;
		for (int i = 0; i < num; i++)
			m_SellMan_List[0].gameObject.GetComponent<SellMan>().SetStateDeath();
	}

	// 매도벽체크
	public bool SellWallCheck()
	{
		double money = CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum];
		int tenNum = 0;
		while (money >= 10)
		{
			money /= 10;
			tenNum++;
		}
		if (CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum] / Mathf.Pow(10, tenNum) == 9)
			return true;
		else
			return false;
	}
}
