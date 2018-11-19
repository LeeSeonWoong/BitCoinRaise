using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SellMan_State
{
	Fall, Land, Move, AttackMan, AttackCoin, Death
}

public class SellMan : MonoBehaviour
{
	// Inspector
	Animator m_Animator;
	SpriteRenderer m_SpriteRenderer;
	int m_State;
	GameObject instance;
	GameObject m_BuyMan;
	BoxCollider2D m_BoxCollider2D;
    public bool SeaType = false;
	[HideInInspector] public int m_HP;
	[HideInInspector] public float m_Damage;

	// Function
	private void Start()
	{
		instance = gameObject;
		m_State = (int)SellMan_State.Fall;
		m_Animator = GetComponent<Animator>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_BoxCollider2D = GetComponent<BoxCollider2D>();
		m_HP = LevelInfo.Instance.m_SellManHP;
		m_Damage = LevelInfo.Instance.m_SellManDmg;
	}
	public void DownMove()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + LevelInfo.Instance.m_SellManFallSpeed, transform.position.z);
	}
	private void Update()
	{
		// 추락상태인 경우
		if (m_State == (int)SellMan_State.Fall)
		{
			if (CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].transform.position.y > transform.position.y - 1.25f)
			{
				m_Animator.SetBool("EnemyLand", true);
				m_State = (int)SellMan_State.Land;
                if(SeaType == true)
                {
                    ParticleSystem.MainModule PS = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
                    PS.loop = false;
                }
			}
			else
			{
				if (m_SpriteRenderer.flipX)
				{
					transform.position = new Vector3(transform.position.x - Time.deltaTime * 0.5f, transform.position.y, transform.position.z);
					if (transform.position.x < 2.4f)
						m_SpriteRenderer.flipX = false;
				}
				else
				{
					if (transform.position.x > 5.9f)
						m_SpriteRenderer.flipX = true;
					transform.position = new Vector3(transform.position.x + Time.deltaTime * 0.5f, transform.position.y, transform.position.z);
				}

			}
		}
		// OnLand인 경우
		else
		{
			float y = CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].transform.position.y;
			float xpos = transform.position.x;
			if (xpos < 2.1f)
				xpos = 2.1f;
			else if (xpos > 4.7f)
				xpos = 4.7f;
			transform.position = new Vector3(xpos, y + 1.25f, transform.position.z);
			// 만약 매도맨의 상태가 Move 나 AttackCoin이 아닌경우 -> 코인 공격
			if (m_State != (int)SellMan_State.Move || m_State != (int)SellMan_State.AttackMan ||
				m_State != (int)SellMan_State.AttackCoin)
			{
				m_State = (int)SellMan_State.AttackCoin;
				m_Animator.SetBool("EnemyAttackCoin", true);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (m_HP <= 0)
			return;
		// collision이 매도맨인 경우 제외
		if (!collision.GetComponent<BuyMan>())
		{
			return;
		}
		else if (!collision.GetComponent<BuyMan>().m_Targeting && m_State != (int)SellMan_State.Move && m_State != (int)SellMan_State.Fall)
		{
			m_BuyMan = collision.gameObject;
			if (m_BuyMan.GetComponent<BuyMan>().m_SafeDeath)
				return;
			m_BuyMan.GetComponent<BuyMan>().m_Targeting = true;
			m_BoxCollider2D.enabled = false;
			m_State = (int)SellMan_State.Move;
			if (m_BuyMan.GetComponent<SpriteRenderer>().flipX)
			{
				m_SpriteRenderer.flipX = false;
				CoinInfo.Instance.m_CocosFunc.MoveByX(0.65f, new Vector2(4.7f, transform.position.y), false, ref instance);
			}
			else
			{
				m_SpriteRenderer.flipX = true;
				CoinInfo.Instance.m_CocosFunc.MoveByX(0.65f, new Vector2(2.1f, transform.position.y), false, ref instance);
			}
			m_Animator.SetBool("EnemyMove", true);
		}
	}

	public void SetStateAttackMan()
	{
		m_State = (int)SellMan_State.AttackMan;
		m_Animator.SetBool("EnemyAttackMan", true);
	}

	public void SetStateDeath()
	{
		Destroy(gameObject);
		CoinInfo.Instance.m_SellManMgr.m_SellMan_List.Remove(gameObject);
	}

	public void SetStateAttackCheck()
	{
		if (m_BuyMan != null)
		{
			CoinInfo.Instance.m_BuyManMgr.m_BuyMan_List.Remove(m_BuyMan.gameObject);
			m_BuyMan.GetComponent<BuyMan>().m_Death = true;
			m_BuyMan.GetComponent<BuyMan>().m_Animator.SetBool("BuyManDeath", true);
            if (CoinInfo.Instance.m_BitCoinMgr.m_MaxIndex <= 220)
            {
                m_BuyMan.transform.GetChild(0).gameObject.SetActive(true);
            }

        }
		else
			SetStateDeath();
	}

	public void AttackCoin()
	{
		m_HP -= 1;
		CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].GetComponent<BitCoin>().CoinAttacked();
		if (m_HP == 0)
			m_Animator.SetBool("EnemyFadeOut", true);
	}

	public void ReturnAttackCoin()
	{
		//m_State = (int)SellMan_State.Move;
	}
}
