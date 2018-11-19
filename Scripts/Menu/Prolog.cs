using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prolog : MonoBehaviour {

	// Inspector
	[SerializeField] InputField m_ID;
	[SerializeField] GameObject m_Check;            // 인풋필드창
	[SerializeField] Text m_CheckBoxText;			// 인풋필드 확인텍스트
	[SerializeField] Image m_Fade;
	[SerializeField] GameObject m_Dollar;
	[SerializeField] GameObject m_MoneyTxt;         // 2달러 금액 텍스트
	[SerializeField] GameObject m_TargetCheckBox;	// 목표 체크박스

	// 대화 체크박스
	[SerializeField] Image m_RacoonMan;
	[SerializeField] Image m_Line;
	[SerializeField] Image m_Back;
	[SerializeField] Text m_NPC_Name;
	[SerializeField] Text m_Speech;

	// 메뉴창
	[SerializeField] GameObject m_MenuScene;

	bool[] m_PhaseCheck = new bool[10];
	
	// Function
	private void Update()
	{
		if (m_PhaseCheck[9])
		{
			if (m_PhaseCheck[0] && m_Check.active)
			{
				if (m_ID.text.Length != 0)
				{
					m_CheckBoxText.color = new Color(1, 1, 1, 1);
				}
				else
				{
					m_CheckBoxText.color = new Color(0.5f, 0.5f, 0.5f, 1);
				}
			}

			if (Input.GetMouseButtonDown(0))
			{
				if (m_PhaseCheck[0] == false)
				{
					m_PhaseCheck[0] = true;
					CheckBoxOn();
				}
				else if (m_PhaseCheck[1] == false && m_PhaseCheck[2])
				{
					m_PhaseCheck[1] = true;
					m_PhaseCheck[2] = false;
					Speech2();
				}
				else if (m_PhaseCheck[1] && m_PhaseCheck[2] == false)
				{
					m_PhaseCheck[2] = true;
					Speech3();
				}
				else if (m_PhaseCheck[2] && m_PhaseCheck[3] == false)
				{
					m_PhaseCheck[3] = true;
					Speech4();
				}
				else if (m_PhaseCheck[3] && m_PhaseCheck[4] == false)
				{
					m_PhaseCheck[4] = true;
					Speech5();
				}
				else if (m_PhaseCheck[4] && m_PhaseCheck[5] == false)
				{
					m_PhaseCheck[5] = true;
					Speech6();
				}
			}
		}
	}
	public void SaveID()
	{
		if (m_ID.text.Length != 0)
		{
			CoinInfo.Instance.m_ID = m_ID.text;
			CoinInfo.Instance.m_FirstGame = true;
			CoinInfo.Instance.Save();
			m_Check.SetActive(false);
			Speech1();
		}
	}

	public void Phase01()
	{
		gameObject.SetActive(true);
		GameObject racoon = m_RacoonMan.gameObject;
		CoinInfo.Instance.m_CocosFunc.MoveTo(1.0f, new Vector2(480, 0), false, ref racoon);
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(2.5f, ref m_Line);
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(2.5f, ref m_Back);
		CoinInfo.Instance.m_CocosFunc.TextFadeIn(2.5f, 1.0f, ref m_NPC_Name);
		CoinInfo.Instance.m_CocosFunc.TextFadeIn(2.5f, 1.0f, ref m_Speech);
		CoinInfo.Instance.m_CocosFunc.TypingEffect("허허... 눈동자를 보아하니 근심이 많아 \n보이는군. 자네, 이름이 뭔가?",0.05f, ref m_Speech);
	}

	IEnumerator StartDelay()
	{
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(1.0f, ref m_Fade);
		yield return new WaitForSecondsRealtime(1.1f);
		Phase01();
		yield return new WaitForSecondsRealtime(2.5f);
		m_PhaseCheck[9] = true;
	}
	public void CheckBoxOn()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("TypingEffectC");
		m_Speech.text = "허허... 눈동자를 보아하니 근심이 많아 \n보이는군. 자네, 이름이 뭔가?";
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(0.5f, 0.5f, ref m_Fade);
		StartCoroutine("CheckBoxOnC");
	}

	IEnumerator CheckBoxOnC()
	{
		yield return new WaitForSeconds(0.5f);
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_Check.SetActive(true);
	}

	IEnumerator Delay1()
	{
		yield return new WaitForSeconds(0.3f);
		m_PhaseCheck[2] = true;
	}
	public void Speech1()
	{
		StartCoroutine("Delay1");
		m_Fade.color = new Color(1, 1, 1, 0);
		m_Speech.text = "<color=#FFCD29FF>" + m_ID.text + "</color>";
		CoinInfo.Instance.m_CocosFunc.TypingEffect(" 이라···정말 저주받은 이름이군. 근심이 많을 수 밖에 없겠어", 0.05f, ref m_Speech);
	}

	public void Speech2()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("TypingEffectC");
		m_Speech.text = "";
		CoinInfo.Instance.m_CocosFunc.TypingEffect("자, 이걸 받으세. 돈을 불러오는 행운의\n부적이라네.", 0.05f, ref m_Speech);
		m_Dollar.SetActive(true);
	}

	public void Speech3()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("TypingEffectC");
		m_Speech.text = "";
		CoinInfo.Instance.m_CocosFunc.TypingEffect("물론 공짜는 아닐세. 세상에 공짜가 없다는 건 자네도 겪어봤지 않나? 꼭 성공해서\n100만원으로 되갚길 바라네.", 0.05f, ref m_Speech);
	}

	public void Speech4()
	{
		m_MoneyTxt.SetActive(true);
		m_Dollar.SetActive(false);
		Text moneyTxt = m_MoneyTxt.GetComponent<Text>();
		CoinInfo.Instance.m_SoundMgr.PlaySound(8);
		CoinInfo.Instance.m_CocosFunc.MoveTo(0.5f, new Vector2(0, 100), true, ref m_MoneyTxt);
		CoinInfo.Instance.m_CocosFunc.TextFadeOut(0.5f, ref moneyTxt);
	}

	public void Speech5()
	{
		GameObject obj = m_RacoonMan.gameObject;
		CoinInfo.Instance.m_CocosFunc.MoveTo(0.5f, new Vector2(-600, 0), true, ref obj);
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("TypingEffectC");
        CoinInfo.Instance.m_CocosFunc.TextFadeOut(1.0f, ref m_NPC_Name);
        m_Speech.text = "";
		CoinInfo.Instance.m_CocosFunc.TypingEffect("의문의 인물이 <color=#00C832>2달러</color>를 주고 떠났다.\n100만원? 후후···웃기는군. 100억으로\n되갚아주지. 난 <color=#FFCD29FF>" + m_ID.text + "</color>이니까.", 0.05f, ref m_Speech);
	}

	public void Speech6()
	{
		CoinInfo.Instance.m_CocosFunc.StopCoroutine("TypingEffectC");
		m_Speech.text = "의문의 인물이 <color=#00C832>2달러</color>를 주고 떠났다.\n100만원? 후후···웃기는군. 100억으로\n되갚아주지. 난 <color=#FFCD29FF>" + m_ID.text + "</color>이니까.";
		CoinInfo.Instance.m_SoundMgr.PlaySound(14);
		m_TargetCheckBox.SetActive(true);
		StartCoroutine("EndProlog");
	}

	IEnumerator EndProlog()
	{
		yield return new WaitForSeconds(3.0f);
		CoinInfo.Instance.m_CocosFunc.ImageFadeIn(1.5f, ref m_Fade);
		CoinInfo.Instance.m_FirstGame = false;
		CoinInfo.Instance.Save();
		m_MenuScene.SetActive(true);
		Camera.main.GetComponent<MenuControl>().MenuUpdate();
		yield return new WaitForSeconds(1.5f);
		gameObject.SetActive(false);
	}
}
