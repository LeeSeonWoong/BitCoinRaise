using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Inpsector
	[SerializeField] GameObject m_Slide;
	[SerializeField] GameObject m_CheckBox;
	[SerializeField] Text m_CheckBoxTxt;
	[SerializeField] Image m_FadeImgIn;
	[SerializeField] Image m_FadeImgOut;
	[SerializeField] GameObject m_AllSell;
	[SerializeField] Text m_ExpectMny;
	[SerializeField] public Mute m_Mute;
	[SerializeField] Mute m_InGameMute;

	[SerializeField] SkillSelect m_SkillSelect;
	[SerializeField] Text m_ChildeText;

	bool m_ButtonOn;
	int m_ButtonIndex;

	// Function
	public void Start()
	{
		if (CoinInfo.Instance.m_ChildCheck == false)
			m_ChildeText.text = "계정당 1회 구매 가능(0 / 1)";
		else
			m_ChildeText.text = "계정당 1회 구매 가능(1 / 1)";
	}
	public void GameStart()
	{
		m_ButtonIndex = -1;
		m_ButtonOn = true;
		m_FadeImgOut.enabled = false;
		m_Slide.transform.position = new Vector3(980.0f, 640.0f, -5.0f);
		m_FadeImgIn.color = new Color(m_FadeImgIn.color.r, m_FadeImgIn.color.g, m_FadeImgIn.color.b, 0.0f);
		m_CheckBox.SetActive(false);
		m_AllSell.SetActive(false);
		if(CoinInfo.Instance.m_ChildCheck == false)
			m_ChildeText.text = "계정당 1회 구매 가능(0 / 1)";
		else
			m_ChildeText.text = "계정당 1회 구매 가능(1 / 1)";
	}

	// 코린이 패키지
	public void ChildBtn()
	{
		if (CoinInfo.Instance.m_ChildCheck == false)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(14);
			m_ButtonIndex = 1;
			CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
			m_CheckBox.SetActive(true);
			m_CheckBoxTxt.text = "<color=#FAFA32>코린이 패키지</color>를 구매하시겠습니까?";
		}
	}

	public void Money100Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 100;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#FAC832FF>100만원</color> 쿠폰을 구매하시겠습니까?";
	}

	public void Money500Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 500;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#FAC832FF>500만원</color> 쿠폰을 구매하시겠습니까?";
	}

	public void Money1000Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 1000;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#FAC832FF>1000만원</color> 쿠폰을 구매하시겠습니까?";
	}

	public void Buy10Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 10;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#00C832>너굴맨 쿠폰 10 매</color>를 구매하시겠습니까?";
	}

	public void Buy40Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 40;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#00C832>너굴맨 쿠폰 40 매</color>를 구매하시겠습니까?";
	}

	public void Buy150Btn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_ButtonIndex = 150;
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_CheckBox.SetActive(true);
		m_CheckBoxTxt.text = "<color=#00C832>너굴맨 쿠폰 150 매</color>를 구매하시겠습니까?";
	}

	public void OkBtn()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(13);
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("ImageFadeInC");
		m_FadeImgIn.color = new Color(m_FadeImgIn.color.r, m_FadeImgIn.color.g, m_FadeImgIn.color.b, 0.0f);
		m_CheckBox.SetActive(false);
		// 인앱결제 적용예정
		if (m_ButtonIndex == 10)
			CoinInfo.Instance.m_RacoonNum += 10;
		else if (m_ButtonIndex == 40)
			CoinInfo.Instance.m_RacoonNum += 40;
		else if (m_ButtonIndex == 150)
			CoinInfo.Instance.m_RacoonNum += 150;
		else if(m_ButtonIndex == 100)
			CoinInfo.Instance.m_CurMoney += 1000000;
		else if (m_ButtonIndex == 500)
			CoinInfo.Instance.m_CurMoney += 5000000;
		else if (m_ButtonIndex == 1000)
			CoinInfo.Instance.m_CurMoney += 10000000;
		else if (m_ButtonIndex == 1)//코린이 패키지
		{
			CoinInfo.Instance.m_ChildCheck = true;
			CoinInfo.Instance.m_RacoonNum += 50;
			CoinInfo.Instance.m_CurMoney += 10000000;
			m_ChildeText.text = "계정당 1회 구매 가능(1 / 1)";
		}
		CoinInfo.Instance.Save();
		m_SkillSelect.MenuStart();
	}

	public void NoBtn()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("ImageFadeInC");
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
		m_FadeImgIn.color = new Color(m_FadeImgIn.color.r, m_FadeImgIn.color.g, m_FadeImgIn.color.b, 0.0f);
		m_CheckBox.SetActive(false);
	}

	public void AllSellBtn()
	{
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, ref m_FadeImgIn);
		m_AllSell.SetActive(true);
		string money = string.Format("{0:n0}", CoinInfo.Instance.SellAllCoin(true).ToString());
		m_ExpectMny.text = "예상 체결 금액: " + "<color=#FFFFFF>" + string.Format("{0:n0}", money) + "</color>"
			+ "<color=#FFBB00>" + "(" + CoinInfo.Instance.m_BuyCoin + "코인)" + "</color>";
		CoinInfo.Instance.m_SoundMgr.PlaySound(7);
	}
	public void AllSellNoBtn()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("ImageFadeInC");
		m_FadeImgIn.color = new Color(m_FadeImgIn.color.r, m_FadeImgIn.color.g, m_FadeImgIn.color.b, 0.0f);
		m_AllSell.SetActive(false);
		CoinInfo.Instance.m_SoundMgr.PlaySound(11);
	}
	public void SetPause()
	{
		if (m_ButtonOn)
		{
			m_ButtonOn = false;
			CoinInfo.Instance.m_SoundMgr.PlaySound(12);
			if (CoinInfo.Instance.m_Pause == false)
			{
				m_Mute.MuteCheck();
				m_FadeImgOut.enabled = true;
				Vector3 pos = m_Slide.transform.position;//980 640 -5
				MoveBy(0.5f, new Vector2(pos.x - 620, pos.y), true, ref m_Slide);
				StartCoroutine("Pause");
			}
			else if (CoinInfo.Instance.m_Pause)
			{
				StartCoroutine("ButtonCheck");
				m_InGameMute.MuteCheck();
				m_FadeImgOut.enabled = false;
				CoinInfo.Instance.m_Pause = false;
				Time.timeScale = 1.0f;
				Vector3 pos = m_Slide.transform.position;
				MoveBy(0.5f, new Vector2(pos.x + 620, pos.y), true, ref m_Slide);
				if (CoinInfo.Instance.m_GazuaActive == false)
					CoinInfo.Instance.m_SoundMgr.PlaySound(0);
				else
					CoinInfo.Instance.m_SoundMgr.PlaySound(10);
			}
		}
	}
	public void MoveBy(float time, Vector2 MoveValue, bool check, ref GameObject obj)
	{
		Vector2 objpos = obj.transform.position;
		Vector2 distance = MoveValue - objpos;
		CocosInfo value;
		value.obj = obj;
		value.dis_pos = distance;
		value.end_pos = MoveValue;
		value.time = time;
		value.check1 = check;
		value.save_time = 0.0f;
		value.SpeedX = value.dis_pos.x / value.time;
		value.SpeedY = 0;
		StartCoroutine("MoveToC", value);
	}

	IEnumerator ButtonCheck()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		m_ButtonOn = true;
	}
	IEnumerator Pause()
	{
		yield return new WaitForSeconds(0.5f);
		m_ButtonOn = true;
		if (CoinInfo.Instance.m_GazuaActive == false)
			CoinInfo.Instance.m_SoundMgr.PauseSound(0);
		else
			CoinInfo.Instance.m_SoundMgr.PauseSound(10);
		if (CoinInfo.Instance.m_Pause == false)
		{
			Time.timeScale = 0.0f;
			CoinInfo.Instance.m_Pause = true;
		}
	}

	IEnumerator MoveToC(CocosInfo info)
	{
		info.save_time += Time.deltaTime;
		if (info.obj != null) // 대상이 Destroy 될경우 예외처리
		{
			Vector3 pos = info.obj.GetComponent<RectTransform>().position;
			if (info.save_time < info.time)
			{
				info.obj.GetComponent<RectTransform>().position = new Vector3(pos.x + Time.deltaTime * info.SpeedX, pos.y + Time.deltaTime * info.SpeedY, pos.z);
				yield return null;
				StartCoroutine("MoveToC", info);
			}
			else if (info.check1)
			{
				info.obj.GetComponent<RectTransform>().position = new Vector3(info.end_pos.x, info.end_pos.y, pos.z);
			}
		}
	}
}
