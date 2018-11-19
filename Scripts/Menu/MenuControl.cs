using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

	// 게임 매니저
	[SerializeField] GameMgr m_GameMgr;

	// InputManger
	public GameObject InputManager;

	// Menu창
	public GameObject MenuScene;

	// TopUI
	[SerializeField] MenuTopUI m_TopUI;

	// 프롤로그
	[SerializeField] Prolog m_Prolog;

	// 코인버튼
	int m_BtnNum;
	[SerializeField] Button m_CoinBtn;			// 코인탭 버튼
	[SerializeField] Button m_SkillBtn;			// 스킬탭 버튼
	[SerializeField] Button m_CouponBtn;		// 쿠폰탭 버튼
	[SerializeField] Button m_OtherBtn;         // 기타탭 버튼

	ColorBlock m_SelectColor;
	ColorBlock m_UnSelectColor;

	// Game창
	public GameObject InGame;

	// 선택창 모음 ( 코인 / 스킬 / 쿠폰 / 기타 )
	[SerializeField] GameObject m_Selects;
	[SerializeField] CoinSelect m_CoinSelect;
	[SerializeField] SkillSelect m_SkillSelect;
	[SerializeField] CouponSelect m_CouponSelect;
	[SerializeField] OtherSelect m_OtherSelect;


	//임시 시작 연출
	[SerializeField] Image m_Fade;                  // 페이드


	private void Awake()
	{
		//  시작메뉴
		if (CoinInfo.Instance.m_FirstGame == false)
		{
			m_Prolog.gameObject.SetActive(false);
			MenuScene.SetActive(true);
		}
		else// 프롤로그
			m_Prolog.gameObject.SetActive(true);

		//  인풋매니저
		InputManager.SetActive(false);

		//  인게임
		InGame.SetActive(false);

		m_SelectColor = new ColorBlock();
		m_SelectColor.normalColor = new Color(1, 1, 1, 1);
		m_SelectColor.highlightedColor = new Color(1, 1, 1, 1);
		m_SelectColor.colorMultiplier = 1;
		m_SelectColor.pressedColor = new Color(1, 1, 1, 1);
		m_SelectColor.disabledColor = new Color32(200, 200, 200, 128);

		m_UnSelectColor = new ColorBlock();
		m_UnSelectColor.normalColor = new Color(1, 1, 1, 0.35f);
		m_UnSelectColor.highlightedColor = new Color(1, 1, 1, 1);
		m_UnSelectColor.colorMultiplier = 1;
		m_UnSelectColor.pressedColor = new Color(1, 1, 1, 1);
		m_UnSelectColor.disabledColor = new Color32(200, 200, 200, 128);

	}

	public void Start()
	{
		if (CoinInfo.Instance.m_FirstGame == false)
			MenuUpdate();
		else
		{
			PrologOn();
			m_OtherSelect.HelpClick();
			MenuScene.SetActive(false);
		}
	}

	public void PrologOn()
	{
		CoinInfo.Instance.Save();
		m_Prolog.StartCoroutine("StartDelay");
	}

	public void MenuUpdate()
	{
		// 코인선택 업데이트
		m_CoinBtn.colors = m_SelectColor;
		CoinSelectOn();
		m_CoinSelect.BtnsLock(true);
		m_CoinSelect.RecCoinUpdate();
		m_CoinSelect.CoinUpdate();

		// 스킬선택 업데이트
		m_SkillBtn.colors = m_UnSelectColor;
		m_SkillSelect.MenuStart();
		// 쿠폰/기타
		m_CouponBtn.colors = m_UnSelectColor;
		m_OtherBtn.colors = m_UnSelectColor;

	}
	//	코인 선택창 On 함수
	public void CoinSelectOn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(12);
		float x = m_Selects.transform.position.x;
		if (m_Selects.transform.position.x != 0)
		{
			CoinInfo.Instance.m_CocosFunc.MoveByX(0.3f, new Vector2(0.0f, 0.0f), true, ref m_Selects);
		}

		m_BtnNum = 0;
		m_CoinBtn.colors = m_SelectColor;
		m_SkillBtn.colors = m_UnSelectColor;
		m_CouponBtn.colors = m_UnSelectColor;
		m_OtherBtn.colors = m_UnSelectColor;
	}

	// 스킬 선택창 On 함수
	public void SkillSelectOn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(12);
		if (m_Selects.transform.position.x !=- 720f)
			CoinInfo.Instance.m_CocosFunc.MoveByX(0.3f, new Vector2(-720f, 0), true, ref m_Selects);

		m_CoinBtn.colors = m_UnSelectColor;
		m_SkillBtn.colors = m_SelectColor;
		m_CouponBtn.colors = m_UnSelectColor;
		m_OtherBtn.colors = m_UnSelectColor;
		m_BtnNum = 1;
	}
	// 쿠폰 선택창 On 함수
	public void CouponSelectOn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(12);
		if (m_Selects.transform.position.x !=  - 1440f)
			CoinInfo.Instance.m_CocosFunc.MoveByX(0.3f, new Vector2(- 1440f, 0), true, ref m_Selects);

		m_CoinBtn.colors = m_UnSelectColor;
		m_SkillBtn.colors = m_UnSelectColor;
		m_CouponBtn.colors = m_SelectColor;
		m_OtherBtn.colors = m_UnSelectColor;
		m_BtnNum = 2;
	}
	// 기타 선택창 On 함수
	public void OtherSelectOn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(12);
		if (m_Selects.transform.position.x != - 2160f)
			CoinInfo.Instance.m_CocosFunc.MoveByX(0.3f, new Vector2(- 2160f, 0), true, ref m_Selects);

		m_CoinBtn.colors = m_UnSelectColor;
		m_SkillBtn.colors = m_UnSelectColor;
		m_CouponBtn.colors = m_UnSelectColor;
		m_OtherBtn.colors = m_SelectColor;
		m_BtnNum = 3;
	}

	public void BtnColorUpdt()
	{
		if(m_BtnNum == 0)// 코인탭 버튼
		{
			m_CoinBtn.colors = m_SelectColor;
			m_SkillBtn.colors = m_UnSelectColor;
			m_CouponBtn.colors = m_UnSelectColor;
			m_OtherBtn.colors = m_UnSelectColor;
		}
		else if(m_BtnNum == 1)// 스킬탭 버튼
		{
			m_CoinBtn.colors = m_UnSelectColor;
			m_SkillBtn.colors = m_SelectColor;
			m_CouponBtn.colors = m_UnSelectColor;
			m_OtherBtn.colors = m_UnSelectColor;
		}
		else if(m_BtnNum == 2)// 쿠폰탭 버튼
		{
			m_CoinBtn.colors = m_UnSelectColor;
			m_SkillBtn.colors = m_UnSelectColor;
			m_CouponBtn.colors = m_SelectColor;
			m_OtherBtn.colors = m_UnSelectColor;
		}
		else if (m_BtnNum == 3)// 기타탭
		{
			m_CoinBtn.colors = m_UnSelectColor;
			m_SkillBtn.colors = m_UnSelectColor;
			m_CouponBtn.colors = m_UnSelectColor;
			m_OtherBtn.colors = m_SelectColor;
		}
	}

	// 게임 스타트
	public void GameStart()
	{
		CoinInfo.Instance.m_InGameStart = true;
		m_CoinSelect.SavePrice();
		MenuScene.SetActive(false);
		InGame.SetActive(true);
		InputManager.SetActive(true);
		m_GameMgr.InitInGame();
		CoinInfo.Instance.m_SoundMgr.PlaySoundLoop(0);
	}

    IEnumerator StartDelay()
    {
		m_CoinSelect.BtnsLock(false);
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(1.7f, ref m_Fade);
		yield return new WaitForSeconds(2.0f);
		GameStart();
    }

	// 인게임 종료
	public void GameExit()
	{
		m_Fade.color = new Color(0, 0, 0, 0);
		CoinInfo.Instance.m_InGameStart = false;
		m_GameMgr.GameOverSet();
		Camera.main.gameObject.GetComponent<CameraControl>().player.m_CameraMove = false;
		Camera.main.transform.position = new Vector3(3.6f, 6.4f, -10);
		Time.timeScale = 1.0f;
		MenuScene.SetActive(true);
		InGame.SetActive(false);
		InputManager.SetActive(false);
		m_OtherSelect.HistoryUpdate();
		MenuUpdate();
		CoinInfo.Instance.m_SoundMgr.StopAllSound();
	}
}
