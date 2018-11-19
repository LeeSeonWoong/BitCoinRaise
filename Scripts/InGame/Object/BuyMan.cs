using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMan : MonoBehaviour {

	// Inspector
	[HideInInspector] public int m_MaxFloor;

	Player m_Player;
	[HideInInspector] public Animator m_Animator;
	SpriteRenderer m_SpriteRenderer;
	[HideInInspector] public bool m_Death;
	[HideInInspector] public bool m_SafeDeath;
	[HideInInspector] public bool m_Targeting;

    private float FallingCons;

	// Function
	private void Start()
	{
		m_Targeting = false;
		m_Death = false;
		m_SafeDeath = false;
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_Animator = GetComponent<Animator>();
		m_Player = Camera.main.gameObject.GetComponent<CameraControl>().player;
        FallingCons = Random.Range(0.6f, 1.2f);
	}

	public void MoveUp()
	{
		if (CoinInfo.Instance.m_MaxFloor * 0.6f - this.transform.position.y > 0)
		{
			Vector3 pos = this.transform.position;
			pos.y += 0.6f;
			this.transform.position = pos;
		}
		else
		{
			Vector3 pos = this.transform.position;
			pos.y += 0.6f;
			this.transform.position = pos;
			m_Animator.SetBool("MakeCoin", true);
		}
	}

	public void MakeCoin()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		m_SafeDeath = true;
		Vector3 pos = CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count-1].transform.position;
		CoinInfo.Instance.m_BitCoinMgr.CoinAdd();
		CoinInfo.Instance.m_EffectMgr.CallEffect("MakeCoinEffect",new Vector2(pos.x, pos.y+1), m_SpriteRenderer.flipX);
	}

	public void DeleteBuyMan()
	{
		Destroy(gameObject);
		CoinInfo.Instance.m_BuyManMgr.m_BuyMan_List.Remove(gameObject);
	}

	private void Update()
	{
		if(m_Death)
		{
			if (m_SpriteRenderer.flipX)
				transform.position = new Vector3(transform.position.x + Time.deltaTime * FallingCons, transform.position.y - Time.deltaTime * 4.0f, transform.position.z);
			else
				transform.position = new Vector3(transform.position.x - Time.deltaTime * FallingCons, transform.position.y - Time.deltaTime * 4.0f, transform.position.z);

			// 추락하다가 삭제
			if (transform.position.y < Camera.main.transform.position.y - 20.0f)
				Destroy(gameObject);
		}
	}
}
