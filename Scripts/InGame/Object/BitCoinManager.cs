using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitCoinManager : MonoBehaviour
{

	//Inspector
	[SerializeField] BuyManMgr m_BuyManMgr;
	[SerializeField] GameObject pf_MoneyTag;
	[SerializeField] GameObject pf_FloorTag;
	[SerializeField] Image m_Icon;
	[SerializeField] Text m_Price;

	public GameObject pf_BitCoin;

	[HideInInspector] public int m_MaxIndex;
	[HideInInspector] public List<GameObject> m_Coin_List = new List<GameObject>();
	[HideInInspector] public List<GameObject> m_TagList = new List<GameObject>();

	private string m_CoinPrice;
	float m_CurTagPosY;
	string[] Icon_Name = new string[10] { "Icon/Icon_Ada", "Icon/Icon_BitCoin", "Icon/Icon_BitCoinCash", "Icon/Icon_Dash", "Icon/Icon_Eos", "Icon/Icon_Ethereum",
	"Icon/Icon_Monero", "Icon/Icon_Qtum", "Icon/Icon_Ripple", "Icon/Icon_Tron"};

	GameObject m_CurTag;        // 현재가 태그

	// Function
	private void Awake()
	{
		CoinInfo.Instance.m_BitCoinMgr = this;
	}
	public void GameStart()
	{
		m_CurTagPosY = 0;
		m_Coin_List.Clear();
		m_MaxIndex = 0;
		CoinInit();
		IconSet();
		BottomInfoSet();
	}

	// 게임시작 세팅
	void CoinInit()
	{
		double money = CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum];
		for(int i = 0; i < 15; i++)
			money -= CoinInfo.Instance.ReturnPer(money);
		// 비트코인 타워 15층 세팅
		for (int i = 0; i < 15; i++)
		{
			GameObject obj = Instantiate(pf_BitCoin, new Vector3(3.6f, 0.3f + i * 0.6f, -3), Quaternion.identity);
			obj.GetComponent<BitCoin>().SetCoinImage(i % 2, i);
			m_Coin_List.Add(obj);
			m_MaxIndex = i;
			money += CoinInfo.Instance.ReturnPer(money);
			if (m_MaxIndex % 5 == 0)
			{
				GameObject tag = Instantiate(pf_FloorTag, new Vector3(0.2f, 0.6f * m_MaxIndex, -2), Quaternion.identity);
				tag.transform.position = new Vector3(0.2f, 0.6f * m_MaxIndex + 0.45f, -2);
				tag.GetComponent<TextMesh>().text = money.ToString();
				m_TagList.Add(tag);
			}
		}
		CoinInfo.Instance.m_MaxFloor = m_MaxIndex;
		m_BuyManMgr.GameInitSet();

		// MoneyTag 생성
		m_CurTag = Instantiate(pf_MoneyTag, new Vector3(0.2f, 0.6f * m_MaxIndex, -2.9f), Quaternion.identity);
		m_CurTag.transform.position = new Vector3(0.2f, 0.6f * m_MaxIndex + 0.45f, -2.9f);
		ShowTag();
	}

	// 매수맨의 코인생성 - 코인구매 ( Button UI용 함수 )
	public void MakeBuyMan()
	{
		m_BuyManMgr.CreateBuyMan();
	}

	public void BuyButtonClick()
	{
		m_BuyManMgr.CreateBuyMan_Btn();
	}

	public void CoinAdd()
	{
		if (m_Coin_List[m_Coin_List.Count - 1] != null)
		{
			float ypos = 0.3f + (m_MaxIndex + 1) * 0.6f - 0.6f;
			if (ypos - m_Coin_List[m_Coin_List.Count - 1].transform.position.y > 0.1f)
			{
				return;
			}
			CoinInfo.Instance.CoinUpdate();
			m_MaxIndex += 1;
			GameObject obj = Instantiate(pf_BitCoin, new Vector3(3.6f, ypos + 0.6f, -2), Quaternion.identity);
			obj.GetComponent<BitCoin>().SetCoinImage(m_MaxIndex % 2, m_MaxIndex);
			m_Coin_List.Add(obj);
			CoinInfo.Instance.m_MaxFloor = obj.GetComponent<BitCoin>().m_Index;
			ShowTag();
			// 5층 단위 태그 생성
			if(m_MaxIndex % 5 == 0)
			{
				GameObject tag = Instantiate(pf_FloorTag, new Vector3(0.2f, 0.6f * m_MaxIndex, -2), Quaternion.identity);
				tag.transform.position = new Vector3(0.2f, 0.6f * m_MaxIndex + 0.45f, -2);
				tag.GetComponent<TextMesh>().text = m_CoinPrice;
				m_TagList.Add(tag);
			}
		}
	}

	public void ShowTag()
	{
		BottomInfoSet();
		if (m_CurTag != null)
		{
			m_CurTagPosY = m_CurTag.transform.position.y;
			m_CurTag.transform.position = new Vector3(m_CurTag.transform.position.x, 0.6f * m_MaxIndex + 0.45f, m_CurTag.transform.position.z);
			m_CoinPrice = string.Format("{0:n0}", CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum]);
			m_CurTag.GetComponent<TextMesh>().text = m_CoinPrice;
			if (m_CurTagPosY > m_CurTag.transform.position.y)
				m_CurTag.GetComponentInChildren<SpriteRenderer>().color = new Color32(0, 0, 255, 255);
			else
				m_CurTag.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
		}
	}

	public void DeleteAllCoin()
	{
		Destroy(m_CurTag);
		int count = m_Coin_List.Count;
		for (int i = 0; i < count; ++i)
		{
			Destroy(m_Coin_List[0].gameObject);
			m_Coin_List.Remove(m_Coin_List[0]);
		}
		// 태그삭제
		count = m_TagList.Count;
		for(int i = 0; i < count; ++i)
		{
			Destroy(m_TagList[0].gameObject);
			m_TagList.Remove(m_TagList[0]);
		}
	}

	public void BottomInfoSet()
	{
		m_Price.text = string.Format("{0:n0}", CoinInfo.Instance.m_CoinsPrice[CoinInfo.Instance.m_SelectCoinNum]);
	}	

	public void IconSet()
	{
		m_Icon.sprite = Resources.Load<Sprite>(Icon_Name[CoinInfo.Instance.m_SelectCoinNum]);
	}
}
