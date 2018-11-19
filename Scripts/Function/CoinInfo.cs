using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
class BitCoinData
{
	// 게임정보
	public double[] m_CoinsPrice = new double[10];        // 코인10종 가격
	public double[] m_CoinsSavePrice = new double[10];        // 코인10종 가격 세이브 갱신용

	// 플레이어 정보
	public string m_ID;
	public double m_CurMoney;            // 현재 보유금액
	public int m_Exp;                   // 경험치
	public int m_Lv;                    // 레벨
	public int m_RacoonNum;             // 너굴맨 갯수
	public bool m_First;                // 첫실행 체크
	public bool m_Child;				// 코린이 패키지 구매체크

	// 스킬정보
	public int[] m_Skills = new int[6];             // 스킬 6종 정보

	// 기타정보 ( 매매내역 )
	public SellHistory[] m_SellHistoryArray = new SellHistory[20];
}

public struct CoinProperty
{
	public int CoinNum;             // 코인번호

	public int SellResist;          // 매도저항
	public int BuyResist;           // 매수저항
	public int SellWall;            // 매도벽
	public int Bubble;              // 거품

	public float m_MaxPrice;        // 적정최대가
	public float m_MinPrice;		// 적정최소가
}

// 매매내역 저장 구조체
[Serializable]
public struct SellHistory
{
	public int Year;
	public int Month;
	public int Day;
	public int Hour;
	public int Minute;
	public int second;

	public int CoinIndex;       // 코인 인덱스
	public double Money;		// 결과 액수
	public double Profit;		// 실현 손익
}

public class CoinInfo
{
	private static CoinInfo _instance = null;
	private BitCoinData m_Data;
	private const string m_FilePath = "/BitCoinInfo3.dat";
	public BitCoinManager m_BitCoinMgr;
	public BuyManMgr m_BuyManMgr;
	public SellManMgr m_SellManMgr;
	public EffectMgr m_EffectMgr;
	public CocosFunc m_CocosFunc;
	public SoundMgr m_SoundMgr;
	public GameOver m_GameOver_Obj;
	public Goods m_Goods;
	public bool m_FirstGame;
	public bool m_ChildCheck;
	public string m_ID;

	// 매매내역
	public List<SellHistory> m_HistoryList = new List<SellHistory>();

	// 코인 10종(가격) , 선택된 코인
	public CoinProperty[] m_CoinProperty = new CoinProperty[10];
	public double[] m_CoinsPrice = new double[10];
	public double[] m_CoinsSavePrice = new double[10];        // 코인10종 가격 세이브 갱신용

	public int m_SelectCoinNum;

	public int m_MaxFloor;      //최고층 Index

	public double m_CurMoney;    //현재 보유 Money;
	public bool m_GameOver;     //게임오버체크
	public int m_BuyCoin;               //구매한 코인개수
	public double m_BuyCoinMoney;          //매수한 금액
	public List<double> m_RecordMoney = new List<double>();       //매수금액 기록

	public bool m_GazuaActive;  //가즈아 상태유무
	public bool m_Pause;        //포즈유무
	public double m_StartMoney;  //게임 시작보유금
	public bool m_InGameStart;	//인게임 시작유무

	public int m_Exp;           //경험치
	public int m_Lv;            //레벨
	public int m_ExpLimit;      //경험치 max
	public int m_RacoonNum;     //너굴맨 횟수

	// 스킬
	public int[] m_Skills = new int[6];

	// 가호 체크 변수
	public int m_SellBuyNum;        // 단타신
	public int m_GameOverNum;       // 익절의 마술사

	// Function
	public static CoinInfo Instance
	{
		get
		{
			if (_instance == null)
				_instance = new CoinInfo();
			return _instance;
		}
	}

	public CoinInfo()
	{
		if(!Load())
		{
			DataInit();
			Save();
		}

		CoinPropertySet();
	}

	public bool BuyCoin()
	{
		if (m_CurMoney > m_CoinsPrice[m_SelectCoinNum])
		{
			m_CurMoney -= m_CoinsPrice[m_SelectCoinNum];
			m_BuyCoinMoney += m_CoinsPrice[m_SelectCoinNum];
			m_RecordMoney.Add(m_CoinsPrice[m_SelectCoinNum]);
			m_BuyCoin++;
			return true;
		}
		return false;
	}

	public bool MoneyCheck()
	{
		if (m_CurMoney > m_CoinsPrice[m_SelectCoinNum])
			return true;
		else
			return false;
	}
	public void SellCoin()
	{
		m_BuyCoin--;
		m_CurMoney += m_CoinsPrice[m_SelectCoinNum];
		m_BuyCoinMoney -= m_RecordMoney[m_RecordMoney.Count - 1];
		m_RecordMoney.Remove(m_RecordMoney[m_RecordMoney.Count - 1]);
	}

	// 일괄매도
	public double SellAllCoin(bool show)
	{
		double money = m_BuyCoin * m_CoinsPrice[m_SelectCoinNum];
		double per = ReturnPer(m_CoinsPrice[m_SelectCoinNum]);
		double minus_money = Mathf.Pow((m_SellManMgr.m_SellMan_List.Count - m_BuyManMgr.m_BuyMan_List.Count), 2) / 2 * per;
		if (show)// 결과예상값 반환
			return (int)(money - minus_money);
		else //결과적용후 값 반환
		{
			m_BuyCoin = 0;
			m_CurMoney += money - minus_money;
			m_RecordMoney.Clear();
			return (int)(money - minus_money);
		}
	}

	public void CoinUpdate()
	{
		m_CoinsPrice[m_SelectCoinNum] += (int)ReturnPer(m_CoinsPrice[m_SelectCoinNum]);
	}

	public void CoinBreak()
	{
		m_CoinsPrice[m_SelectCoinNum] -= (int)ReturnPer(m_CoinsPrice[m_SelectCoinNum]);
	}

	public void LevelSet()
	{
		if (m_Lv == 1)
			m_ExpLimit = 500;
		else if (m_Lv == 2)
			m_ExpLimit = 1000;
		else if (m_Lv == 3)
			m_ExpLimit = 2000;
		else if (m_Lv >= 4)
			m_ExpLimit = 5000;
		if (m_Exp >= m_ExpLimit)
		{
			m_Exp = m_Exp - m_ExpLimit;
			m_Lv++;
			if (m_Lv == 1)
				m_ExpLimit = 500;
			else if (m_Lv == 2)
				m_ExpLimit = 1000;
			else if (m_Lv == 3)
				m_ExpLimit = 2000;
			else if (m_Lv >= 4)
				m_ExpLimit = 5000;
		}
	}

	public void LevelSetSkip(int LeftScore)
	{
		LevelSet();
		m_Exp += LeftScore;
		if (m_Exp >= m_ExpLimit)
		{
			m_Exp = m_Exp - m_ExpLimit;
			m_Lv++;
			if (m_Lv == 1)
				m_ExpLimit = 500;
			else if (m_Lv == 2)
				m_ExpLimit = 1000;
			else if (m_Lv == 3)
				m_ExpLimit = 2000;
			else if (m_Lv >= 4)
				m_ExpLimit = 5000;
		}
	}

	// 게임 시작시 초기화
	public void CoinInfoInit()
	{
		m_InGameStart = false;
		m_GazuaActive = false;
		m_Pause = false;
		m_RecordMoney.Clear();
		m_BuyCoinMoney = 0;
		m_BuyCoin = 0;
		m_GameOverNum = 0;
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + m_FilePath);

		BitCoinData data = new BitCoinData();
		// 코인가 저장
		for (int i = 0; i < 10; i++)
		{
			data.m_CoinsPrice[i] = m_CoinsPrice[i];
			data.m_CoinsSavePrice[i] = m_CoinsSavePrice[i];
		}
		// 스킬내역 저장
		for (int i = 0; i < 6; i++)
			data.m_Skills[i] = m_Skills[i];
		// 매매내역 저장
		for(int i = 0; i < m_HistoryList.Count; i++)
			data.m_SellHistoryArray[i] = m_HistoryList[i];
		data.m_ID = m_ID;
		data.m_CurMoney = m_CurMoney;
		data.m_Exp = m_Exp;
		data.m_Lv = m_Lv;
		data.m_RacoonNum = m_RacoonNum;
		data.m_First = m_FirstGame;
		data.m_Child = m_ChildCheck;
		
		bf.Serialize(file, data);

		file.Close();
	}

	public bool Load()
	{
		if (File.Exists(Application.persistentDataPath + m_FilePath))
		{
			BinaryFormatter bf = new BinaryFormatter();

			FileStream file = File.Open(Application.persistentDataPath + m_FilePath, FileMode.Open);
			m_Data = (BitCoinData)bf.Deserialize(file);
			if (m_Data == null)
			{
				file.Close();
				return false;
			}
			file.Close();
			LoadData();
			return true;
		}
		else
		{
			DataInit();
			Save();
			return false;
		}
	}

	public void LoadData()
	{
		for (int i = 0; i < 10; i++)
		{
			m_CoinsPrice[i] = m_Data.m_CoinsPrice[i];
			m_CoinsSavePrice[i] = m_Data.m_CoinsSavePrice[i];
		}
		for (int i = 0; i < 6; i++)
			m_Skills[i] = m_Data.m_Skills[i];
		m_ID = m_Data.m_ID;
		m_RacoonNum = m_Data.m_RacoonNum;
		m_CurMoney = m_Data.m_CurMoney;
		m_Exp = m_Data.m_Exp;
		m_Lv = m_Data.m_Lv;
		m_FirstGame = m_Data.m_First;
		m_ChildCheck = m_Data.m_Child;

		for (int i = 0; i < m_Data.m_SellHistoryArray.Length; i++)
			m_HistoryList.Add(m_Data.m_SellHistoryArray[i]);
	}

	public void DataInit()
	{
		m_ID = null;
		m_Lv = 4;
		m_Exp = 0;
		m_RacoonNum = 3;
		m_GazuaActive = false;
		m_Pause = false;
		m_GameOver = false;
		m_FirstGame = true;
		m_ChildCheck = false;

		m_CoinsPrice[0] = 300;			//에이다
		m_CoinsPrice[1] = 12000000;     //비트코인
		m_CoinsPrice[2] = 2400000;      //비트코인캐시
		m_CoinsPrice[3] = 840000;       //대시
		m_CoinsPrice[4] = 12000;        //이오스
		m_CoinsPrice[5] = 1200000;      //이더리움
		m_CoinsPrice[6] = 360000;       //모네로
		m_CoinsPrice[7] = 60000;        //퀸텀
		m_CoinsPrice[8] = 1200;         //리플
		m_CoinsPrice[9] = 60;           //트론

		for (int i = 0; i < 10; i++)
			m_CoinsSavePrice[i] = m_CoinsPrice[i];
		m_CurMoney = 2160;

		for (int i = 0; i < 6; i++)
			m_Skills[i] = 0;

		m_HistoryList.Clear();
	}

	public void DataReset()
	{
		DataInit();
		Save();
	}

	// 호가 시스템
	public double ReturnPer(double _money)
	{
		if (_money >= 0 && _money < 1000)
			return 1;
		else
		{
			int per = 0;
			double SaveMoeny = _money;
			while(SaveMoeny > 10)
			{
				SaveMoeny *= 0.1f;
				per++;
			}
			return Mathf.Pow(10, per-2);
		}
	}

	// 코인 가격 갱신(게임)
	public void CoinPriceUpdate_ByGame()
	{
		for(int i = 0; i < 10; i++)
		{
			if (i != m_SelectCoinNum)
			{
				if (m_CoinsPrice[i] >= m_CoinProperty[i].m_MinPrice &&
					m_CoinsPrice[i] < m_CoinProperty[i].m_MaxPrice)
				{
					float RandNum = UnityEngine.Random.Range(-10, 11);
					RandNum = 1 + RandNum * 0.01f;
					m_CoinsPrice[i] *= RandNum;
					int trash = (int)m_CoinsPrice[i] % (int)ReturnPer(m_CoinsPrice[i]);
					m_CoinsPrice[i] = (int)(m_CoinsPrice[i] - trash) / 1.0f;
				}
				else if(m_CoinsPrice[i] >= m_CoinProperty[i].m_MaxPrice)
				{
					float RandNum = UnityEngine.Random.Range(-90, -10);
					RandNum = 1 + RandNum * 0.01f;
					m_CoinsPrice[i] *= RandNum;
					int trash = (int)m_CoinsPrice[i] % (int)ReturnPer(m_CoinsPrice[i]);
					m_CoinsPrice[i] = (int)(m_CoinsPrice[i] - trash) / 1.0f;
				}
			}
		}

		Save();
	}
	// 코인가격갱신 (틱단위)
	public void CoinPriceUpdate_Tick()
	{
		for(int i = 0; i < 10; i++)
		{
			if(m_CoinsPrice[i] < m_CoinProperty[i].m_MaxPrice)
				m_CoinsPrice[i] += ReturnPer(m_CoinsPrice[i]) * UnityEngine.Random.Range(-2, 3);
			else
				m_CoinsPrice[i] += ReturnPer(m_CoinsPrice[i]) * UnityEngine.Random.Range(-5, 2);
		}
		Save();
	}
	
	// 매매내역 저장
	public void SaveHistory(double profit)
	{
		SellHistory sample;
		sample.Year = System.DateTime.Now.Year;
		sample.Month = System.DateTime.Now.Month;
		sample.Day = System.DateTime.Now.Day;
		sample.Hour = System.DateTime.Now.Hour;
		sample.Minute = System.DateTime.Now.Minute;
		sample.second = System.DateTime.Now.Second;

		sample.CoinIndex = m_SelectCoinNum;
		sample.Profit = profit;
		sample.Money = m_CurMoney - m_StartMoney;

		if(m_HistoryList.Count != 20)
			m_HistoryList.Add(sample);
		else
		{
			m_HistoryList.RemoveAt(0);
			m_HistoryList.Add(sample);
		}
	}

	// 코인 속석 세팅
	public void CoinPropertySet()
	{
		Hashtable SellResist_map = new Hashtable();
		SellResist_map["상"] = 5;
		SellResist_map["중"] = 7;
		SellResist_map["하"] = 10;

		Hashtable BuyResist_map = new Hashtable();
		BuyResist_map["상"] = 8;
		BuyResist_map["중"] = 10;
		BuyResist_map["하"] = 12;

		Hashtable SellWall_map = new Hashtable();
		SellWall_map["상"] = 5;
		SellWall_map["중"] = 4;
		SellWall_map["하"] = 3;

		Hashtable Bubble_map = new Hashtable();
		Bubble_map["상"] = 30;
		Bubble_map["중"] = 45;
		Bubble_map["하"] = 60;

		// 에이다
		m_CoinProperty[0].SellResist = (int)SellResist_map["상"];
		m_CoinProperty[0].BuyResist = (int)BuyResist_map["하"];
		m_CoinProperty[0].SellWall = (int)SellWall_map["하"];
		m_CoinProperty[0].Bubble = (int)Bubble_map["상"];
		m_CoinProperty[0].m_MinPrice = 250;
		m_CoinProperty[0].m_MaxPrice = 12500;

		// 비트코인
		m_CoinProperty[1].SellResist = (int)SellResist_map["상"];
		m_CoinProperty[1].BuyResist = (int)BuyResist_map["상"];
		m_CoinProperty[1].SellWall = (int)SellWall_map["상"];
		m_CoinProperty[1].Bubble = (int)Bubble_map["하"];
		m_CoinProperty[1].m_MinPrice = 10000000;
		m_CoinProperty[1].m_MaxPrice = 1000000000;

		// 비트코인 캐시
		m_CoinProperty[2].SellResist = (int)SellResist_map["상"];
		m_CoinProperty[2].BuyResist = (int)BuyResist_map["상"];
		m_CoinProperty[2].SellWall = (int)SellWall_map["중"];
		m_CoinProperty[2].Bubble = (int)Bubble_map["하"];
		m_CoinProperty[2].m_MinPrice = 2000000;
		m_CoinProperty[2].m_MaxPrice = 10000000;

		// 대시
		m_CoinProperty[3].SellResist = (int)SellResist_map["하"];
		m_CoinProperty[3].BuyResist = (int)BuyResist_map["중"];
		m_CoinProperty[3].SellWall = (int)SellWall_map["상"];
		m_CoinProperty[3].Bubble = (int)Bubble_map["중"];
		m_CoinProperty[3].m_MinPrice = 700000;
		m_CoinProperty[3].m_MaxPrice = 35000000;

		// 이오스
		m_CoinProperty[4].SellResist = (int)SellResist_map["상"];
		m_CoinProperty[4].BuyResist = (int)BuyResist_map["중"];
		m_CoinProperty[4].SellWall = (int)SellWall_map["중"];
		m_CoinProperty[4].Bubble = (int)Bubble_map["상"];
		m_CoinProperty[4].m_MinPrice = 10000;
		m_CoinProperty[4].m_MaxPrice = 500000;

		// 이더리움
		m_CoinProperty[5].SellResist = (int)SellResist_map["중"];
		m_CoinProperty[5].BuyResist = (int)BuyResist_map["상"];
		m_CoinProperty[5].SellWall = (int)SellWall_map["상"];
		m_CoinProperty[5].Bubble = (int)Bubble_map["하"];
		m_CoinProperty[5].m_MinPrice = 1000000;
		m_CoinProperty[5].m_MaxPrice = 50000000;

		// 모네로
		m_CoinProperty[6].SellResist = (int)SellResist_map["중"];
		m_CoinProperty[6].BuyResist = (int)BuyResist_map["상"];
		m_CoinProperty[6].SellWall = (int)SellWall_map["중"];
		m_CoinProperty[6].Bubble = (int)Bubble_map["중"];
		m_CoinProperty[6].m_MinPrice = 300000;
		m_CoinProperty[6].m_MaxPrice = 15000000;

		// 퀸텀
		m_CoinProperty[7].SellResist = (int)SellResist_map["중"];
		m_CoinProperty[7].BuyResist = (int)BuyResist_map["상"];
		m_CoinProperty[7].SellWall = (int)SellWall_map["상"];
		m_CoinProperty[7].Bubble = (int)Bubble_map["상"];
		m_CoinProperty[7].m_MinPrice = 50000;
		m_CoinProperty[7].m_MaxPrice = 2500000;

		// 리플
		m_CoinProperty[8].SellResist = (int)SellResist_map["하"];
		m_CoinProperty[8].BuyResist = (int)BuyResist_map["하"];
		m_CoinProperty[8].SellWall = (int)SellWall_map["중"];
		m_CoinProperty[8].Bubble = (int)Bubble_map["상"];
		m_CoinProperty[8].m_MinPrice = 1000;
		m_CoinProperty[8].m_MaxPrice = 50000;

		// 트론
		m_CoinProperty[9].SellResist = (int)SellResist_map["중"];
		m_CoinProperty[9].BuyResist = (int)BuyResist_map["하"];
		m_CoinProperty[9].SellWall = (int)SellWall_map["하"];
		m_CoinProperty[9].Bubble = (int)Bubble_map["상"];
		m_CoinProperty[9].m_MinPrice = 50;
		m_CoinProperty[9].m_MaxPrice = 2500;
	}
}


