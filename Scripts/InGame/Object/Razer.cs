using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : MonoBehaviour {

	Animator m_Animator;
	SpriteRenderer m_SpriteRender;

	private void Awake()
	{
		m_Animator = GetComponent<Animator>();
		m_SpriteRender = GetComponent<SpriteRenderer>();
		m_SpriteRender.enabled = false;
		m_Animator.enabled = false;
	}

	// 레이저 작동
	public void SetActiveAnimator()
	{
		m_SpriteRender.enabled = true;
		m_Animator.enabled = true;
	}

	// 애니메이터 정지
	public void StopAnimator()
	{
		m_SpriteRender.enabled = false;
		m_Animator.enabled = false;
	}

	// 모든 매도맨 삭제
	public void DeleteAllSellMan()
	{
		CoinInfo.Instance.m_SellManMgr.SellManDelete();
	}
}
