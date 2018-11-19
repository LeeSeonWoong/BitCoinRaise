using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goods : MonoBehaviour {

	// Inspector
	[SerializeField] Text m_Title;
	[SerializeField] Text m_BoxText;
	[SerializeField] Image m_Box;
	[SerializeField] Image[] m_Icon = new Image[6];

	int m_Index;
	int[] m_ActiveNum = new int[6];
	[HideInInspector] public bool[] m_ActiveCheck = new bool[6];

	private string[] Icon_Name = new string[6] { "Icon_Goods/Icon_567", "Icon_Goods/Icon_BlackCow", "Icon_Goods/Icon_Brain",
		"Icon_Goods/Icon_Coupon", "Icon_Goods/Icon_Dishes", "Icon_Goods/Icon_Hand" };

	// Function
	private void Awake()
	{
		CoinInfo.Instance.m_Goods = this;
	}

	private void Start()
	{
		GameStart();
	}

	public void GameStart()
	{
		for (int i = 0; i < 6; i++)
		{
			m_Icon[i].color = new Color(m_Icon[i].color.r, m_Icon[i].color.g, m_Icon[i].color.b, 0.0f);
			m_Icon[i].transform.position = new Vector3(360, 640, m_Icon[i].transform.position.z);
			m_ActiveNum[i] = -1;
		}
		for (int i = 0; i < 6; i++)
			m_ActiveCheck[i] = false;
		m_Index = 0;
		m_Title.color = new Color(m_Title.color.r, m_Title.color.g, m_Title.color.b, 0.0f);
		m_BoxText.color = new Color(m_BoxText.color.r, m_BoxText.color.g, m_BoxText.color.b, 0.0f);
		m_Box.color = new Color(m_Box.color.r, m_Box.color.g, m_Box.color.b, 0.0f);
	}

	IEnumerator IconMove(GameObject obj)
	{
		yield return new WaitForSeconds(1.0f);
		if (m_Index >= 1)
		{
			for (int i = 0; i < m_Index; i++)
			{
				GameObject other = m_Icon[i].gameObject;
				CoinInfo.Instance.m_CocosFunc.MoveTo(0.5f, new Vector2(-65,0), true, ref other);
			}
		}
		Vector2 movePos = new Vector2(310, 410);
		CoinInfo.Instance.m_CocosFunc.MoveTo(1.0f, movePos, true, ref obj);
		CoinInfo.Instance.m_CocosFunc.ScaleTo(1.0f, new Vector2(0.5f, 0.5f), ref obj);
		yield return new WaitForSeconds(1.0f);
		CoinInfo.Instance.m_CocosFunc.ImageFadeOut(1.0f, ref m_Box);
		CoinInfo.Instance.m_CocosFunc.TextFadeOut(1.0f, ref m_Title);
		CoinInfo.Instance.m_CocosFunc.TextFadeOut(1.0f, ref m_BoxText);
		m_Index++;
	}

	public void SetGoods()
	{
		int Goods_Num = 0;
		bool check = true;
		if (m_Index >= 6)
			return;
		while (check)
		{
			Goods_Num = Random.Range(0, 6);
			check = false;
			for (int i = 0; i < m_Index; i++)
			{
				if (Goods_Num == m_ActiveNum[i])
				{
					check = true;
					break;
				}
			}
		}

		if (m_Index <= 5)
		{
			CoinInfo.Instance.m_SoundMgr.PlaySound(16);
			m_Icon[m_Index].sprite = Resources.Load<Sprite>(Icon_Name[Goods_Num]);
			m_ActiveNum[m_Index] = Goods_Num;
			CoinInfo.Instance.m_CocosFunc.ImageFadeIn(1.0f, ref m_Box);
			CoinInfo.Instance.m_CocosFunc.ImageFadeIn(1.0f, ref m_Icon[m_Index]);
			CoinInfo.Instance.m_CocosFunc.TextFadeIn(1.0f, 1.0f, ref m_Title);
			CoinInfo.Instance.m_CocosFunc.TextFadeIn(1.0f, 1.0f, ref m_BoxText);
			StartCoroutine("IconMove", m_Icon[m_Index].gameObject);
			if (Goods_Num == 0)
			{
				m_ActiveCheck[Goods_Num] = true;
				m_Title.text = "5,6,7";
				m_BoxText.text = "가즈아-의 기본 지속 시간이 50% 증가합니다.";
			}
			else if(Goods_Num == 1)
			{
				m_ActiveCheck[Goods_Num] = true;
				m_Title.text = "사, 흑우 어서!";
				m_BoxText.text = "매수맨의 등장 주기가 20% 감소합니다.";
			}
			else if(Goods_Num == 2)
			{
				m_ActiveCheck[Goods_Num] = true;
				m_Title.text = "뇌동매매";
				m_BoxText.text = "매수 버튼의 충전 속도가 50% 감소합니다.";
			}
			else if(Goods_Num == 3)
			{
				m_ActiveCheck[Goods_Num] = true;
				CoinInfo.Instance.m_RacoonNum += 2;
				m_Title.text = "안심하라구!";
				m_BoxText.text = "너굴맨 쿠폰을 2개 획득합니다.";
			}
			else if(Goods_Num == 4)
			{
				m_ActiveCheck[Goods_Num] = true;
				m_Title.text = "설거지";
				m_BoxText.text = "매도 시 50% 확률로 매수맨이 생성됩니다.";
			}
			else if (Goods_Num == 5)
			{
				m_ActiveCheck[Goods_Num] = true;
				m_Title.text = "큰손";
				m_BoxText.text = "매수 버튼의 충전 한도가 100% 증가합니다.";
			}
		}
	}
}
