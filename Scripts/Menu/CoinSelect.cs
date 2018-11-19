using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CoinSelect : MonoBehaviour
{
	// Inspector
	[SerializeField] MenuControl m_MenuControl;		// 게임시작용 메뉴 컨트롤러
	[SerializeField] Coin m_RecCoin;				// 추천코인
	[SerializeField] Coin m_SelCoin;				// 선택코인
	[SerializeField] public Coin[] m_Coins;				// 코인10종
	[SerializeField] public GameObject m_CheckBox;  // 코인선택 체크박스
	[SerializeField] Text m_CheckBoxText;			// 코인설명 텍스트
	[SerializeField] Text m_NeedMoney;				// 필요한 돈
	[SerializeField] Button m_GazuaBtn;             // 가즈아 버튼
	[SerializeField] Text m_GazuaTxt;               // 가즈아 텍스트
	[SerializeField] Image m_Fade;                  // 페이드

	string[] m_CoinInfo = new string[10];

	// Function
	private void Start()
	{
		m_CheckBox.SetActive(false);
		m_CoinInfo[0] = "비트코인과 이더리움의 단점을 보안한 코인으로, 회계 감사가 공개되어있는 아주 투명한 코인이다.";
		m_CoinInfo[1] = "거래에 초점을 맞춘 최초의 가상 화폐로서, 코인판의 대장 역할을 하는 아주 듬직한 코인이다.";
		m_CoinInfo[2] = "비트코인의 거래 한계를 늘리기 위해 비트코인에서 파생된 코인으로, 역대급 펌핑이 있었던 아주 재밌는 코인이다.";
		m_CoinInfo[3] = "1000개 이상의 코인을 보유할 시, 개발 방향에 대해 투표를 할 수 있는 아주 민주적인 코인이다.";
		m_CoinInfo[4] = "이더리움 개발자들의 차기작으로, 거래 속도가 아주 빠르며 이용 수수료가 무료인 아주 혜자 코인이다.";
		m_CoinInfo[5] = "비트코인을 다양한 분야에서 활용이 가능하도록 확장한 코인으로, 여러 대기업들과 협력 관계를 맺은 아주 안전한 코인이다.";
		m_CoinInfo[6] = "거래 시 거래자의 정보가 공개되지 않는 특징이 있어 김정은이 채굴하기로 유명한 아주 다크한 코인이다.";
		m_CoinInfo[7] = "비트코인과 이더리움을 잘 섞어 만든 코인으로, 중국에서 아주 인기가 높은 아이돌 코인이다.";
		m_CoinInfo[8] = "국제 이체와 환전의 편리성을 위해 마든 코인으로, 여러 금융 인프라가 구축되어있는 아주 전망 좋은 코인이다.";
		m_CoinInfo[9] = "엔터테이먼트에 초점을 맞춰 20년 장기 개발 계획을 가지고 있는 아주 잠재적인 코인이다.";
	}

	// 체크박스 버튼//////////////////////////////////////////////////////////////////////
	public void NoBtn()
	{
		Time.timeScale = 1.0f;
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		m_Fade.raycastTarget = false;
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.5f, ref m_Fade);
		m_CheckBox.SetActive(false);
	}
	public void OkBtn() // 게임시작
	{
		SavePrice();
		Time.timeScale = 1.0f;
		m_Fade.raycastTarget = false;
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(0.5f, ref m_Fade);
		CoinInfo.Instance.m_SoundMgr.PlaySound(13);
		m_CheckBox.SetActive(false);
		m_MenuControl.StartCoroutine("StartDelay");
	}

	/////////////////////////////////////////////////////////////////////////////////////
	// 코인별 선택버튼////////////////////////////////////////////////////////////////////
	// 추천코인 버튼
	public void RecBtn()
	{
		if (m_RecCoin.m_CoinNum == 0)
			AdaBtn();
		else if (m_RecCoin.m_CoinNum == 1)
			BitBtn();
		else if (m_RecCoin.m_CoinNum == 2)
			BchBtn();
		else if (m_RecCoin.m_CoinNum == 3)
			DshBtn();
		else if (m_RecCoin.m_CoinNum == 4)
			EosBtn();
		else if (m_RecCoin.m_CoinNum == 5)
			EthBtn();
		else if (m_RecCoin.m_CoinNum == 6)
			MroBtn();
		else if (m_RecCoin.m_CoinNum == 7)
			QtmBtn();
		else if (m_RecCoin.m_CoinNum == 8)
			RipBtn();
		else if (m_RecCoin.m_CoinNum == 9)
			TroBtn();
	}

	public void AdaBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 0;
		CheckMoney();
	}

	public void BitBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 1;
		CheckMoney();
	}

	public void BchBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 2;
		CheckMoney();
	}

	public void DshBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 3;
		CheckMoney();
	}

	public void EosBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 4;
		CheckMoney();
	}

	public void EthBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 5;
		CheckMoney();
	}

	public void MroBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 6;
		CheckMoney();
	}

	public void QtmBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 7;
		CheckMoney();
	}

	public void RipBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 8;
		CheckMoney();
	}

	public void TroBtn()
	{
		CoinInfo.Instance.m_SelectCoinNum = 9;
		CheckMoney();
	}

	public void CheckMoney()
	{
		Time.timeScale = 0.0f;
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_Fade.raycastTarget = true;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_Fade);
		m_SelCoin.m_Icon.sprite = m_Coins[CoinInfo.Instance.m_SelectCoinNum].m_Icon.sprite;
		m_SelCoin.m_CoinName.text = m_Coins[CoinInfo.Instance.m_SelectCoinNum].m_CoinName.text;
		m_SelCoin.m_CoinSym.text = m_Coins[CoinInfo.Instance.m_SelectCoinNum].m_CoinSym.text;
		m_CheckBox.SetActive(true);
		m_CheckBoxText.text = m_CoinInfo[CoinInfo.Instance.m_SelectCoinNum];
		m_NeedMoney.text = string.Format("{0:n0}", CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum]) + " 원";
		// 현재돈이 최소 코인가 이상인 경우
		if (CoinInfo.Instance.m_CurMoney >= CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum])
		{
			m_GazuaTxt.color = new Color(1, 1, 1, 1);
			m_GazuaBtn.enabled = true;
		}
		else// 돈이 최소 코인가보다 적은 경우
		{
			m_GazuaTxt.color = new Color(1, 1, 1, 0.6f);
			m_GazuaBtn.enabled = false;
		}
	}

	// 코인정보 업데이트(틱)
	public void CoinUpdateTick()
	{
		for (int i = 0; i < 10; i++)
		{
			double sample = CoinInfo.Instance.m_CoinsPrice[i];
			m_Coins[i].m_CurTxt.text = string.Format("{0:n0}", sample) + " 원";
			double dif = CoinInfo.Instance.m_CoinsPrice[i] - CoinInfo.Instance.m_CoinsSavePrice[i];
			double ChangePer = dif / CoinInfo.Instance.m_CoinsPrice[i] * 100.0f;
			if (CoinInfo.Instance.m_CoinsSavePrice[i] == 0 || dif == 0)
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(0, 0, 0, 255);
				m_Coins[i].m_ChangeTxt.text = "+0 원 (+0.00%)";
			}
			else if (dif > 0)
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(255, 0, 0, 255);
				m_Coins[i].m_ChangeTxt.text = "+" + string.Format("{0:n0}", dif) + " 원 (+" + string.Format("{0:F2}", ChangePer) + "%)";
			}
			else
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(0, 0, 255, 255);
				m_Coins[i].m_ChangeTxt.text = string.Format("{0:n0}", dif) + " 원 (" + string.Format("{0:F2}", ChangePer) + "%)";
			}
		}
		RecCoinUpdate();
		StartCoroutine("CoinUpdate_Tick");
	}
	// 코인정보 업데이트(게임단위)
	public void CoinUpdate()
	{
		for (int i = 0; i < 10; i++)
		{
			double sample = CoinInfo.Instance.m_CoinsPrice[i];
			m_Coins[i].m_CurTxt.text = string.Format("{0:n0}", sample) + " 원";
			double dif = CoinInfo.Instance.m_CoinsPrice[i] - CoinInfo.Instance.m_CoinsSavePrice[i];
			double ChangePer = dif / CoinInfo.Instance.m_CoinsPrice[i] * 100.0f;
			if(CoinInfo.Instance.m_CoinsSavePrice[i] == 0 || dif == 0)
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(0, 0, 0, 255);
				m_Coins[i].m_ChangeTxt.text = "+0 원 (+0.00%)";
			}
			else if (dif > 0)
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(255, 0, 0, 255);
				m_Coins[i].m_ChangeTxt.text = "+" + string.Format("{0:n0}", dif) + " 원 (+" + string.Format("{0:F2}", ChangePer ) + "%)";
			}
			else
			{
				m_Coins[i].m_ChangeTxt.color = new Color32(0, 0, 255, 255);
				m_Coins[i].m_ChangeTxt.text = string.Format("{0:n0}", dif) + " 원 (" + string.Format("{0:F2}", ChangePer ) + "%)";
			}
		}
		RecCoinUpdate();
		StartCoroutine("CoinUpdate_Tick");
	}

	// 코인 정보 업데이트 틱단위
	IEnumerator CoinUpdate_Tick()
	{
		if (CoinInfo.Instance.m_InGameStart == false)
		{
			yield return new WaitForSeconds(5.0f);
			CoinInfo.Instance.CoinPriceUpdate_Tick();
			CoinUpdateTick();
			RecCoinUpdate();
		}
	}
	// 추천코인 갱신
	public void RecCoinUpdate()
	{
		double money = CoinInfo.Instance.m_CurMoney;
		money *= 0.01f;
		if (money > CoinInfo.Instance.m_CoinsPrice[1])
			CopyCoinInfo(1);
		else if(money > CoinInfo.Instance.m_CoinsPrice[2])
			CopyCoinInfo(2);
		else if (money > CoinInfo.Instance.m_CoinsPrice[5])
			CopyCoinInfo(5);
		else if (money > CoinInfo.Instance.m_CoinsPrice[3])
			CopyCoinInfo(3);
		else if (money > CoinInfo.Instance.m_CoinsPrice[6])
			CopyCoinInfo(6);
		else if (money > CoinInfo.Instance.m_CoinsPrice[7])
			CopyCoinInfo(7);
		else if (money > CoinInfo.Instance.m_CoinsPrice[4])
			CopyCoinInfo(4);
		else if (money > CoinInfo.Instance.m_CoinsPrice[8])
			CopyCoinInfo(8);
		else if (money > CoinInfo.Instance.m_CoinsPrice[0])
			CopyCoinInfo(0);
		else
			CopyCoinInfo(9);

		//if(money >= 0 && money < 5000)					// 트론
		//	CopyCoinInfo(9);
		//else if(money >= 5000 && money < 25000)			// 에이다
		//	CopyCoinInfo(0);
		//else if(money >= 25000 && money < 100000)		// 리플
		//	CopyCoinInfo(8);
		//else if (money >= 100000 && money < 1000000)	// 이오스
		//	CopyCoinInfo(4);
		//else if (money >= 1000000 && money < 5000000)   // 퀸텀
		//	CopyCoinInfo(7);
		//else if (money >= 5000000 && money < 30000000)  // 모네로
		//	CopyCoinInfo(6);
		//else if (money >= 30000000 && money < 70000000)  // 대시
		//	CopyCoinInfo(3);
		//else if (money >= 70000000 && money < 100000000) // 이더리움
		//	CopyCoinInfo(5);
		//else if (money >= 100000000 && money < 200000000)// 비트코인캐시
		//	CopyCoinInfo(2);
		//else if (money >= 200000000)					 // 비트코인
		//	CopyCoinInfo(1);
	}

	public void CopyCoinInfo(int num)
	{
		m_RecCoin.m_Icon.sprite = m_Coins[num].m_Icon.sprite;
		m_RecCoin.m_CoinName.text = m_Coins[num].m_CoinName.text;
		m_RecCoin.m_CoinNum = m_Coins[num].m_CoinNum;
		m_RecCoin.m_CoinSym.text = m_Coins[num].m_CoinSym.text;
		m_RecCoin.m_CurTxt.text = m_Coins[num].m_CurTxt.text;
		m_RecCoin.m_ChangeTxt.color = m_Coins[num].m_ChangeTxt.color;
		m_RecCoin.m_ChangeTxt.text = m_Coins[num].m_ChangeTxt.text;
	}

	public void BtnsLock(bool _lock)
	{
		for (int i = 0; i < 10; i++)
			m_Coins[i].gameObject.GetComponent<Button>().interactable = _lock;
		m_RecCoin.gameObject.GetComponent<Button>().interactable = _lock;
	}

	public void SavePrice()
	{
		for (int i = 0; i < 10; i++)
			CoinInfo.Instance.m_CoinsSavePrice[i] = CoinInfo.Instance.m_CoinsPrice[i];
	}
}
