using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour {

	///////////////////////////////////////////////////////////////////////////////////////////////
	// Inspector View 에 속성 값을 0 을 넣어 주면 0이 아닌 기본 설정값이 들어갑니다	    //////////////
	///////////////////////////////////////////////////////////////////////////////////////////////
	// ★★★★★★★★★★★★★★★★★★ [주의사항] ★★★★★★★★★★★★★★★★★★★★★★★★★

	// 1. Buyman_ClimbingSpeed, Buyman_SpawnTime 등 가호와 스킬이 반영되는 값들은
	// 입력한 값에 추가로 곱해져서 반영됩니다. 
	// ex) Inspector : Buyman_SpawnTime = 입력값 -> 시스템 : 입력값 * 스킬 값 * 가호 값

	// 2. Buyman_SellingResist, SellMan_BuyingResist -> 코인 속성을 받아서 작동하기 때문에 여기서 0이 아닌 값을 입력받은 경우
	// 입력된 값으로 변수가 일시적으로 치환된다. ( 코인속성은 데이터에서 저장하기 떄문에 영구적 아님 )

	// 3.SellMan_SpawnBonus -> 앞자리가 9층 단위로 생기는 매도벽 특성임.

	///////////////////////////////////////////////////////////////////////////////////////////////

	// Inspector /////////////////////////////////////////////////////////////////////////////////////
	// 플레이어
	[SerializeField] float Playing_Climbing_Speed;				// 영차 애니메이션 재생속도		(기본 = 1.0f -> 애니메이션 속도 )
	[SerializeField] float Playing_Climbing_BonusBuyman;		// 영차시 추가 매수맨 등장확률	(기본 = 1.0f, 1.0f = 100% 로 취급합니다)
	// 매수맨
	[SerializeField] float Buyman_ClimbingSpeed;                // 매수맨 이동속도				(기본 = 1.0.f )
	[SerializeField] float Buyman_SpawnTime;                    // 매수맨 기본 스폰시간			(기본 = 1.5초 )
	[SerializeField] int Buyman_SellingResist;					// 잔존 매도맨 비례 매수맨 추가 스폰량 (기본 =  코인별 속성값 )
	// 매도맨
	[SerializeField] int SellMan_HP;							// 매수맨 HP		(기본 = 3 )
	[SerializeField] float SellMan_Dmg;                         // 매수맨 공격력 (기본 = 1 )
	[SerializeField] float SellMan_FallingSpeed;                // 매수맨 하강속도 (기본 = -0.6f )
	[SerializeField] float SellMan_SpawnTime;                   // 매수맨 기본스폰시간 (기본 = 3초 )
	[SerializeField] int SellMan_SpawnBonus;					// 특정층 도달시 매도맨 추가 스폰량
	[SerializeField] int SellMan_BuyingResist;					// 잔존 매수맨 비례 매도맨 추가 스폰량
	// 가즈아
	[SerializeField] float Gazua_BasicTime;                     // 가즈아 초기 지속시간	(기본 = 10초 )
	[SerializeField] int Gazua_StartNum;						// 가즈아 시작에 필요 매수맨 수
	[SerializeField] int Gazua_EndNum;							// 가즈아 종료에 필요 매수맨 수
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////

	// Function
	private void Awake()
	{
		InfoInit();
		InfoSet();
	}

	public void InfoInit()
	{
		// 플레이어
		if (Playing_Climbing_Speed == 0.0f)
			Playing_Climbing_Speed = 1.0f;
		if (Playing_Climbing_BonusBuyman == 0.0f)
			Playing_Climbing_BonusBuyman = 1.0f;
		// 매수맨
		if (Buyman_ClimbingSpeed == 0.0f)
			Buyman_ClimbingSpeed = 1.0f;
		if (Buyman_SpawnTime == 0.0f)
			Buyman_SpawnTime = 1.5f;
		if (Buyman_SellingResist == 0)
			Buyman_SellingResist = 0;
		// 매도맨
		if (SellMan_HP == 0)
			SellMan_HP = 3;
		if (SellMan_Dmg == 0.0f)
			SellMan_Dmg = 1.0f;
		if (SellMan_FallingSpeed == 0.0f)
			SellMan_FallingSpeed = -0.6f;
		if (SellMan_SpawnTime == 0.0f)
			SellMan_SpawnTime = 2.0f;
		if (SellMan_SpawnBonus == 0)
			SellMan_SpawnBonus = 0;
		if (SellMan_BuyingResist == 0)
			SellMan_BuyingResist = 0;
		// 가즈아
		if (Gazua_BasicTime == 0.0f)
			Gazua_BasicTime = 10.0f;
		if (Gazua_StartNum == 0)
			Gazua_StartNum = 20;
		if (Gazua_EndNum == 0)
			Gazua_EndNum = 15;
	}

	public void InfoSet()
	{
		// 플레이어
		LevelInfo.Instance.m_PlayerSpeed = Playing_Climbing_Speed;
		LevelInfo.Instance.m_BonusBuyMan = Playing_Climbing_BonusBuyman;
		// 매수맨
		LevelInfo.Instance.m_BuyManSpeed = Buyman_ClimbingSpeed;
		LevelInfo.Instance.m_BuyManSpawnTime = Buyman_SpawnTime;
		LevelInfo.Instance.m_BuyManSellResist = Buyman_SellingResist;
		// 매도맨
		LevelInfo.Instance.m_SellManHP = SellMan_HP;
		LevelInfo.Instance.m_SellManDmg = SellMan_Dmg;
		LevelInfo.Instance.m_SellManFallSpeed = SellMan_FallingSpeed;
		LevelInfo.Instance.m_SellManSpawnTime = SellMan_SpawnTime;
		LevelInfo.Instance.m_SellManSpawnBonus = SellMan_SpawnBonus;
		LevelInfo.Instance.m_SellManBuyResist = SellMan_BuyingResist;
		// 가즈아
		LevelInfo.Instance.m_GazuaTime = Gazua_BasicTime;
		LevelInfo.Instance.m_GazuaStartNum = Gazua_StartNum;
		LevelInfo.Instance.m_GazuaEndNum = Gazua_EndNum;
	}
	private void Update()
	{
		InfoInit();
		InfoSet();
	}
}
