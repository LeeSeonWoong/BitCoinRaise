using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManMgr : MonoBehaviour
{
	// Inspector ///////////////////////////////////////////////////
	public GameObject pf_BuyMan;  //매수맨
	[SerializeField] BuyBtn m_BuyBtn; //구매버튼 스크립트
	[SerializeField] public Player m_Player;
	public Gazua m_Gazua;
	[HideInInspector] public List<GameObject> m_BuyMan_List;
	bool m_FlipSave = false;
	bool m_OnceCheck;
	float m_CheckTime;

	// Function ////////////////////////////////////////////////////
	private void Awake()
	{
		CoinInfo.Instance.m_BuyManMgr = this;
	}

	public void GameStart()
	{
		m_OnceCheck = false;
		m_FlipSave = false;
		m_CheckTime = 1.1f;
		m_BuyMan_List.Clear();
	}

	private void Update()
	{
		if (m_Gazua.m_Accel == 1.0f)
			m_Player.m_Animator.speed = LevelInfo.Instance.m_PlayerSpeed;

		m_CheckTime += Time.deltaTime;

		// 가즈아 체크
		m_Gazua.GazuaCheck(m_BuyMan_List.Count);
		if (CoinInfo.Instance.m_GazuaActive && m_Gazua.m_MadKing == false)
		{
			if (m_Gazua.m_ActiveTime > 30.0f)
				m_Gazua.m_MadKing = true;
			else
				m_Gazua.m_ActiveTime += Time.deltaTime;
		}
		else
			m_Gazua.m_ActiveTime = 0.0f;
	}

	// 매수버튼 기능 /////////////////////////////////////////////////
	public bool CreateBuyMan()
	{
		if (m_OnceCheck == false)
		{
			m_OnceCheck = true;
			return false;
		}
		else
		{
			MakeBuyMan();
			return true;
		}
	}
	public bool CreateBuyMan_Btn()
	{
		if (m_OnceCheck == false)
		{
			m_OnceCheck = true;
			return false;
		}
		else
		{
			if (CoinInfo.Instance.MoneyCheck())
			{
				if (m_BuyBtn.BuyCheck())
				{
					CoinInfo.Instance.m_SellBuyNum++;
					CoinInfo.Instance.BuyCoin();
					CoinInfo.Instance.m_SoundMgr.PlaySound(3);
					m_CheckTime = 0.0f;
					StartCoroutine("MakeBuyManC");
					return true;
				}
			}
			else
				m_BuyBtn.CurNoMoney();
		}
		return false;
	}

	IEnumerator MakeBuyManC()
	{
		yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
		MakeBuyMan();
	}
	public void MakeBuyMan()
	{
        float posY = (m_Player.m_FloorIndex - 6) * 0.6f;
		if (CoinInfo.Instance.m_GameOver)
		{
			float trash = Camera.main.transform.position.y % 0.6f + 3.7f;
			posY = Camera.main.transform.position.y - trash;
		}
		GameObject obj;
		if (m_FlipSave)//우측 생성
		{
			obj = Instantiate(pf_BuyMan, new Vector3(5.4f, posY - 0.1f, -3.0f), Quaternion.identity);
			obj.GetComponent<SpriteRenderer>().flipX = m_FlipSave;
		}
		else   //좌측 생성
		{
			obj = Instantiate(pf_BuyMan, new Vector3(1.77f, posY - 0.1f, -3.0f), Quaternion.identity);
			obj.GetComponent<SpriteRenderer>().flipX = m_FlipSave;
		}
		obj.GetComponent<Animator>().speed = LevelInfo.Instance.m_BuyManSpeed + CoinInfo.Instance.m_Skills[4] * 0.1f;
		if (CoinInfo.Instance.m_GazuaActive)
			obj.GetComponent<Animator>().speed *= 1.5f;
		m_BuyMan_List.Add(obj);
		m_FlipSave = m_FlipSave == true ? false : true;
	}

	// 게임초기 세팅
	public void GameInitSet()
	{
		StartCoroutine("BuyManInit");
	}

	IEnumerator BuyManInit()
	{
		yield return new WaitForSeconds(0.1f);
		// 2번코인 매수맨
		GameObject obj = Instantiate(pf_BuyMan, new Vector3(1.67f, 1.4f, -3.5f), Quaternion.identity);
		obj.GetComponent<SpriteRenderer>().flipX = false;
		m_BuyMan_List.Add(obj);

		// 6번코인 매수맨
		GameObject obj2 = Instantiate(pf_BuyMan, new Vector3(5.5f, 3.8f, -3.5f), Quaternion.identity);
		obj2.GetComponent<SpriteRenderer>().flipX = true;
		m_BuyMan_List.Add(obj2);

		// 10번코인 매수맨
		GameObject obj3 = Instantiate(pf_BuyMan, new Vector3(1.67f, 6.2f, -3.5f), Quaternion.identity);
		obj3.GetComponent<SpriteRenderer>().flipX = false;
		m_BuyMan_List.Add(obj3);

		StartCoroutine("BuyManLoopSpawn");
	}
	// 리스트 삭제
	public void BuyManDelete()
	{
		int num = m_BuyMan_List.Count;
		for (int i = 0; i < num; i++)
			m_BuyMan_List[0].gameObject.GetComponent<BuyMan>().DeleteBuyMan();
	}

	IEnumerator BuyManLoopSpawn()
	{
		int plusBuyMan = 0;
		if (LevelInfo.Instance.m_BuyManSellResist == 0)
			plusBuyMan = CoinInfo.Instance.m_SellManMgr.m_SellMan_List.Count / CoinInfo.Instance.m_CoinProperty[CoinInfo.Instance.m_SelectCoinNum].SellResist;
		else
			plusBuyMan = LevelInfo.Instance.m_BuyManSellResist;

		for (int i = 0; i < plusBuyMan+1; i++)
			StartCoroutine("MakeBuyManC");

		if (CoinInfo.Instance.m_Goods.m_ActiveCheck[1])// 사, 흑우 효과
			yield return new WaitForSeconds((LevelInfo.Instance.m_BuyManSpawnTime - (1 - (CoinInfo.Instance.m_Skills[5] * 0.01f))) * m_Gazua.m_Accel * 0.8f);
		else
			yield return new WaitForSeconds((LevelInfo.Instance.m_BuyManSpawnTime - (1 - (CoinInfo.Instance.m_Skills[5] * 0.01f))) * m_Gazua.m_Accel);

		StartCoroutine("BuyManLoopSpawn");
	}

	public void GazuaOn()
	{
		if (CoinInfo.Instance.m_GameOver == false)
		{
			m_Player.m_Animator.speed = LevelInfo.Instance.m_PlayerSpeed * 2.0f;
			m_Player.GazuaActive(true);
			CoinInfo.Instance.m_SoundMgr.SoundFadeOut(1.0f, 0);
			CoinInfo.Instance.m_SoundMgr.PlayDelay(1.0f, true, 10);
			for (int i = 0; i < m_BuyMan_List.Count; i++)
				m_BuyMan_List[0].GetComponent<Animator>().speed *= 1.5f;
		}
	}

	public void GazuaOff()
	{
		if (m_Gazua.m_Accel == 1.0f)
		{
			m_Player.m_Animator.speed = LevelInfo.Instance.m_PlayerSpeed;
			m_Player.GazuaActive(false);
			CoinInfo.Instance.m_SoundMgr.SoundFadeOut(1.0f, 10);
			CoinInfo.Instance.m_SoundMgr.PlayDelay(1.0f, true, 0);
			for (int i = 0; i < m_BuyMan_List.Count; i++)
				m_BuyMan_List[0].GetComponent<Animator>().speed = LevelInfo.Instance.m_BuyManSpeed + CoinInfo.Instance.m_Skills[4] * 0.1f;
		}
	}

	public void GazuaStop()
	{
		// 가즈아 중이면 
		if (m_Gazua.m_Accel == 0.6f)
		{
			m_Gazua.GazuaEnd();
			m_Player.m_Animator.speed = LevelInfo.Instance.m_PlayerSpeed;
			m_Player.GazuaActive(false);
			CoinInfo.Instance.m_SoundMgr.StopSound(10);
			CoinInfo.Instance.m_SoundMgr.PlaySoundLoop(0);
			for (int i = 0; i < m_BuyMan_List.Count; i++)
				m_BuyMan_List[0].GetComponent<Animator>().speed = LevelInfo.Instance.m_BuyManSpeed + CoinInfo.Instance.m_Skills[4] * 0.1f;
		}
	}
}
