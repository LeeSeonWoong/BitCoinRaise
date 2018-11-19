using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
	// Inspector
	[SerializeField] Player m_Player;
	[SerializeField] Button[] m_Button; // 메뉴, 매수, 너굴맨, 매도
	[SerializeField] Toggle m_Toggle;   // 음소거
	[SerializeField] Image m_Fade;      // 페이드
	[SerializeField] ResultBox m_Result;

	[SerializeField] GameObject m_Buffer;// 버퍼
	[SerializeField] GameObject m_CheckBox;// 체크박스
	[SerializeField] Image m_CheckBoxFade;// 체크박스 페이드
	[SerializeField] Text m_CheckBoxText;// 체크박스 텍스트
	[SerializeField] RacoonBtn m_RacoonScript;// 너굴맨 스크립트
	[SerializeField] Button m_RacoonBtn; // 너굴맨 버튼
	[SerializeField] Text m_Oktxt;       // OK 버튼 텍스트
	[SerializeField] Text m_Notxt;       // No 버튼 텍스트
	[SerializeField] Button m_Okbtn;     // Ok 버튼
	[SerializeField] Button m_Nobtn;     // No 버튼

	[SerializeField] AllSell m_AllSell;   // 일괄매도
	[SerializeField] GameObject m_SellBtnCheckBox;	// 매도버튼 체크박스

	GameObject m_PlayerObj;
	bool m_Racoon;
	int m_DownFloor;

	// Function
	private void Awake()
	{
		CoinInfo.Instance.m_GameOver_Obj = this;
		m_PlayerObj = m_Player.gameObject;
		m_CheckBox.SetActive(false);
	}

	// GameOver Phase01
	public void Phase01()
	{
		m_Fade.raycastTarget = true;
		m_AllSell.gameObject.SetActive(false);
		m_SellBtnCheckBox.SetActive(false);
		m_AllSell.m_FadeImage.color = new Color(0, 0, 0, 0);
		CoinInfo.Instance.m_GameOverNum += 1;
		m_DownFloor = 0;
		for (int i = 0; i < m_Button.Length; i++)
			m_Button[i].enabled = false;
		m_Toggle.enabled = false;
		m_Player.m_Animator.SetBool("FallEnd", false);
		m_Player.m_Animator.SetBool("FallCheck", true);
		StartCoroutine("Phase02");
	}

	IEnumerator Phase02()
	{
		// 8층 추락
		yield return new WaitForSecondsRealtime(1.0f);
		m_Player.gameObject.transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y - 1.2f, m_Player.transform.position.z);
		CoinInfo.Instance.m_CocosFunc.MoveToR(1.0f, new Vector2(0, -3.6f), true, ref m_PlayerObj);
		m_DownFloor += 8;
		Time.timeScale = 2.0f;
		yield return new WaitForSecondsRealtime(0.5f);
		Time.timeScale = 0.0f;
		yield return new WaitForSecondsRealtime(0.5f);

		// Phase03
		m_Fade.enabled = true;
		m_Buffer.SetActive(true);
		yield return new WaitForSecondsRealtime(1.0f);
		m_Buffer.SetActive(false);

		// Phase04
		Time.timeScale = 1.0f;
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_CheckBox.SetActive(true);
		m_Okbtn.enabled = false;
		m_Nobtn.enabled = false;
		m_Oktxt.color = new Color(m_Oktxt.color.r, m_Oktxt.color.g, m_Oktxt.color.b, 0.5f);
		m_Notxt.color = new Color(m_Oktxt.color.r, m_Oktxt.color.g, m_Oktxt.color.b, 0.5f);

		m_CheckBoxFade.enabled = true;
		m_RacoonBtn.enabled = false;
		
		int num = 1;
		int tick = 5;
		while (num <= 20)
		{
			// 0.25초당 20층추락
			m_DownFloor++;
			CoinInfo.Instance.m_CocosFunc.MoveToR(0.24f, new Vector2(0, -0.6f), true, ref m_PlayerObj);
			yield return new WaitForSecondsRealtime(0.25f);
			num++;
			tick = 5 - num / 4;
			m_CheckBoxText.text = "남은 대기시간: " + tick;
		}
		m_CheckBoxText.text = "남은 대기시간: 0";
		Time.timeScale = 0.0f;
		if (CoinInfo.Instance.m_RacoonNum > 0)
		{
			m_RacoonBtn.enabled = true;
			m_CheckBoxFade.enabled = false;
		}
		m_Okbtn.enabled = true;
		m_Nobtn.enabled = true;
		m_Oktxt.color = new Color(m_Oktxt.color.r, m_Oktxt.color.g, m_Oktxt.color.b, 1.0f);
		m_Notxt.color = new Color(m_Oktxt.color.r, m_Oktxt.color.g, m_Oktxt.color.b, 1.0f);
	}

	// 너굴맨 클릭버튼
	public void RacoonClick()
	{
		if (m_RacoonBtn.enabled && CoinInfo.Instance.m_RacoonNum > 0)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(13);
			m_RacoonBtn.enabled = false;
			CoinInfo.Instance.m_RacoonNum -= 1;
			m_Racoon = true;
			CoinInfo.Instance.m_SoundMgr.PlaySound(6);
		}
	}

	// 거래재개 버튼
	public void GameKeep()
	{
		if (m_RacoonBtn.enabled && CoinInfo.Instance.m_RacoonNum > 0)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(13);
			m_RacoonBtn.enabled = false;
			CoinInfo.Instance.m_RacoonNum -= 1;
			m_Racoon = true;
			CoinInfo.Instance.m_SoundMgr.PlaySound(6);
			///////////////////////////////////////////////////////////////////
			//CoinInfo.Instance.m_SoundMgr.PlaySound(11);
			Time.timeScale = 1.0f;
			m_Toggle.enabled = true;
			m_Fade.enabled = false;
			m_CheckBox.SetActive(false);
			m_Player.m_FirstStart = false;
			m_Player.m_Animator.SetBool("FallEnd", true);
			m_Player.m_Animator.SetBool("FallCheck", false);
			m_Player.m_State = (int)PlayerState.LeftDown;
			m_Player.SetPos();
			m_Player.StartCoroutine("Blink");
			for (int i = 0; i < m_Button.Length; i++)
				m_Button[i].enabled = true;
			m_Racoon = false;
			CoinInfo.Instance.m_GameOver = false;
			CoinInfo.Instance.m_SellManMgr.StartCoroutine("SpawnLevelUp");
			CoinInfo.Instance.m_SellManMgr.StartCoroutine("SpawnSellMan");
		}
	}

	// 거래중단 버튼
	public void GameEnd()
	{
		m_Fade.raycastTarget = false;
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		//일괄매도
		m_Fade.enabled = false;
		m_CheckBox.SetActive(false);
		m_AllSell.gameObject.SetActive(true);
		m_AllSell.ShowExpectMoney();
	}

	public void GameResult()
	{
		m_Fade.raycastTarget = false;
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		// 거래결과
		m_Fade.enabled = false;
		Time.timeScale = 0.0f;
		m_Result.gameObject.SetActive(true);
		m_Result.ShowResult();
	}

	public void GameStart()
	{
		m_Racoon = false;
		CoinInfo.Instance.m_GameOver = false;
		m_Toggle.enabled = true;
		for (int i = 0; i < m_Button.Length; i++)
			m_Button[i].enabled = true;
	}
}
