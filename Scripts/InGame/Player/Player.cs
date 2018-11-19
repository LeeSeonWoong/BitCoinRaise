using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum PlayerState
{
	LeftDown = 20, LeftUp = 21, RightDown = 22, RightUp = 23
}

public class Player : MonoBehaviour, InputListener
{
	// 터치제어
	public TouchManager m_TouchManager;     //멀티터치용
	TouchInfo[] touchPoint = new TouchInfo[10];
	public GameObject m_Gazua;
	public BitCoinManager m_CoinManager;    //비트코인 리스트
	public BackGround m_BackGround;
	public Goods m_Goods;					// 가호
	public Text ForDebug;
	public Image m_FadeImage;

	[HideInInspector] public bool m_SwipeCheck;
	[HideInInspector] public Animator m_Animator;
	GameObject instance;            //자기자신
	[HideInInspector] public bool m_CameraMove = false;     //카메라 무브 On/Off
	[HideInInspector] public bool m_FirstStart;                                     //게임 재시작 체크
	SpriteRenderer m_SpriteRender;

	// 캐릭터 상태
	[HideInInspector] public float m_State;
	[HideInInspector] public int m_FloorIndex;               //코인층
	int m_LimitFloor;                                        //최고 하층 체크
	int m_GoodsNum;
	[HideInInspector] public bool m_ReverseOn;										// 내려가기체크

	// Function
	private void Awake()
	{
		m_Animator = GetComponent<Animator>();
		m_SpriteRender = GetComponent<SpriteRenderer>();
	}

	public void GameStart()
	{
		m_ReverseOn = false;
		m_GoodsNum = 0;
		m_TouchManager.listner = this;//나 자신을 인풋리스너 주체로 등록
		instance = gameObject;
		m_State = (int)PlayerState.LeftDown;
		CoinInfo.Instance.m_StartMoney = CoinInfo.Instance.m_CurMoney;
		m_FirstStart = false;
		m_SwipeCheck = false;
		m_LimitFloor = 8;
		transform.position = new Vector3(transform.position.x, 4.9f, transform.position.z);
		AniInit();
		SetPos();
		m_CameraMove = true;
		CoinInfo.Instance.BuyCoin();
	}
	public void SetPos()
	{
		float ypos = transform.position.y - 0.1f;
		int ynum = (int)(ypos / 0.6f);
		m_FloorIndex = ynum;
		transform.position = new Vector3(transform.position.x, m_FloorIndex * 0.6f + 0.1f, transform.position.z);
	}

	public void ValueSet(Vector2Int idnS, Vector2 pos)
	{
		if (m_FadeImage.raycastTarget || CoinInfo.Instance.m_Pause || (idnS.y == (int)TouchPhase.Ended))
			return;

		touchPoint[idnS.x].id = idnS.x;     //ID값
		touchPoint[idnS.x].state = idnS.y;  //State값

		//Began은 Only 모션체크
		if (touchPoint[idnS.x].state == (int)TouchPhase.Began && ClimbCheck())
		{
			touchPoint[idnS.x].pos = pos;
			touchPoint[idnS.x].first_pos = pos;
		}
		//Moved는 Swipe 체크
		if (touchPoint[idnS.x].state == (int)TouchPhase.Moved && ClimbCheck())
		{
			touchPoint[idnS.x].pos = pos;
			SwipeCheck(touchPoint[idnS.x]);
		}
	}

	void SwipeCheck(TouchInfo info)
	{
		if (m_ReverseOn == false)
		{
			// x좌표 거리 차가 50 미만,  y좌표 거리 차가 200 이상
			if (Mathf.Abs(info.first_pos.x - info.pos.x) < 100.0f && info.first_pos.y - info.pos.y > 100.0f)
			{
				if (m_State == (int)PlayerState.LeftDown && RightBox(info.first_pos))
				{
					if (RightBox(info.first_pos) && RightBox(info.pos))
					{
						if (m_SwipeCheck == false)
						{
							m_FloorIndex += 1;
							m_State = (int)PlayerState.RightUp;
							m_Animator.SetBool("LeftDownToRightUp", true);
							m_Animator.SetBool("RightDownToLeftUp", false);
						}
						if (m_State == (int)PlayerState.RightUp)
							m_Animator.SetBool("LeftUpToLeftDown", false);
					}
				}
				else if (m_State == (int)PlayerState.RightDown && LeftBox(info.first_pos))
				{
					if (LeftBox(info.first_pos) && LeftBox(info.pos))
					{
						if (m_SwipeCheck == false)
						{
							m_FloorIndex += 1;
							m_State = (int)PlayerState.LeftUp;
							m_Animator.SetBool("RightDownToLeftUp", true);
							m_Animator.SetBool("LeftDownToRightUp", false);
						}
						if (m_State == (int)PlayerState.LeftUp)
							m_Animator.SetBool("RightUpToRightDown", false);

					}
				}
			}
		}
	}

	void MoveUp()
	{
		if (m_FirstStart == false)
		{
			m_FirstStart = true;
			AniInit();
			AniInit_R();
		}
		else
		{
			if (m_ReverseOn == false)
			{
				Cha_Sound();
				// 한층마다 1개의 BuyMan
				if (Random.Range(0, 100) <= (int)(LevelInfo.Instance.m_BonusBuyMan * 100))
					CoinInfo.Instance.m_BuyManMgr.StartCoroutine("MakeBuyManC");
				Vector3 pos = this.transform.position; pos.y += 0.6f;
				this.transform.position = pos;
			}
		}
	}

	bool RightBox(Vector2 pos)
	{
		if (pos.x > 360.0f && pos.x < 720.0f && pos.y > 240.0f && pos.y < 1080.0f)
			return true;
		else
			return false;
	}

	bool LeftBox(Vector2 pos)
	{
		if (pos.x > 0.0f && pos.x < 360.0f && pos.y > 240.0f && pos.y < 1080.0f)
			return true;
		else
			return false;
	}

	// 등반 가능체크
	bool ClimbCheck()
	{
		if (m_FloorIndex < m_CoinManager.m_MaxIndex)
			return true;
		else
			return false;
	}
	public bool StateCheck()
	{
		if (m_State == (int)PlayerState.LeftDown || m_State == (int)PlayerState.RightDown)
			return true;
		else
			return false;
	}
	/////////////////////////////  디버그 체크용 	/////////////////////////////
	public void RightUpEnd()
	{
		m_Animator.SetBool("RightUpToRightDown", true);
	}
	public void RightDownEnd()
	{
		m_State = (int)PlayerState.RightDown;
		m_SwipeCheck = true;
		StartCoroutine("SwipeDelay");
	}
	public void LeftUpEnd()
	{
		m_Animator.SetBool("LeftUpToLeftDown", true);
	}
	public void LeftDownEnd()
	{
		m_State = (int)PlayerState.LeftDown;
		m_SwipeCheck = true;
		StartCoroutine("SwipeDelay");
	}
	// 리버스 애니메이션
	public void SetReverse()
	{
		//m_ReverseOn = false;
	}
	public void MoveDown()
	{
		Vector3 pos = this.transform.position; pos.y -= 0.6f;
		this.transform.position = pos;
	}
	public void RightDownReverseEnd()
	{
		m_Animator.SetBool("RightUpToLeftDown", true);
		m_Animator.SetBool("RightDownToRightUp", false);
	}
	public void LeftDownReverseEnd()
	{
		m_Animator.SetBool("LeftUpToRightDown", true);
		m_Animator.SetBool("LeftDownToLeftUp", false);
	}
	public void DownOneFloor()
	{
		if (m_ReverseOn == false)
		{
			AniInit();
			if (m_State == (int)PlayerState.LeftDown)
			{
				m_ReverseOn = true;
				StartCoroutine("ReverseC");
				m_State = (int)PlayerState.LeftUp;
				m_FloorIndex -= 1;
				m_Animator.SetBool("LeftDownToLeftUp", true);
				m_Animator.SetBool("RightUpToLeftDown", false);
			}
			else if (m_State == (int)PlayerState.RightDown)
			{
				m_ReverseOn = true;
				StartCoroutine("ReverseC");
				m_State = (int)PlayerState.RightUp;
				m_FloorIndex -= 1;
				m_Animator.SetBool("RightDownToRightUp", true);
				m_Animator.SetBool("LeftUpToRightDown", false);
			}
		}
	}
	IEnumerator ReverseC()
	{
		yield return new WaitForSeconds(0.4f);
		AniInit_R();
		m_ReverseOn = false;
	}
	void DebugSwipeMove()
	{
		if (m_ReverseOn == false)
		{
			if (ClimbCheck() && m_SwipeCheck == false)
			{
				if (m_State == (int)PlayerState.LeftDown)
				{
					m_FloorIndex += 1;
					m_State = (int)PlayerState.RightUp;
					m_Animator.SetBool("LeftDownToRightUp", true);
					m_Animator.SetBool("RightDownToLeftUp", false);
				}
				else if (m_State == (int)PlayerState.RightDown)
				{
					m_FloorIndex += 1;
					m_State = (int)PlayerState.LeftUp;
					m_Animator.SetBool("RightDownToLeftUp", true);
					m_Animator.SetBool("LeftDownToRightUp", false);
				}
			}
			if (m_State == (int)PlayerState.RightUp)
				m_Animator.SetBool("LeftUpToLeftDown", false);
			else if (m_State == (int)PlayerState.LeftUp)
				m_Animator.SetBool("RightUpToRightDown", false);
		}
	}
	IEnumerator SwipeDelay()
	{
		yield return new WaitForSeconds(0.1f);
		m_SwipeCheck = false;
	}

	//////////////////////////////////////////////////////////////////////////////////////
	private void Update()
	{
		if (CoinInfo.Instance.m_Pause == false)
		{
			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				DebugSwipeMove();
			}
		}
		// 가호체크
		if(m_FloorIndex == 100 && m_GoodsNum == 0)
		{
			m_GoodsNum++;
			m_Goods.SetGoods();
		}
		else if(m_FloorIndex == 230 && m_GoodsNum == 1)
		{
			m_GoodsNum++;
			m_Goods.SetGoods();
		}

		// 게임오버 체크
		if (m_FloorIndex > m_CoinManager.m_MaxIndex && CoinInfo.Instance.m_GameOver == false)
		{
			CoinInfo.Instance.m_BuyManMgr.GazuaStop();

			// 최하층과 28층이상 거리를 벌린경우만 게임오버실행
			if (m_FloorIndex - m_LimitFloor > 28)
				GameOver();
			else
			{
				// 결과창
				GameResult();
				return;
			}
		}
	}

	void Yeoung_Sound()
	{
		CoinInfo.Instance.m_SoundMgr.PlaySound(Random.Range(17,20));
	}

	void Cha_Sound()
	{
		// 스킬3번
		if (Random.Range(1, 101) <= CoinInfo.Instance.m_Skills[2] * 5)
			CoinInfo.Instance.m_BuyManMgr.StartCoroutine("MakeBuyManC");
	}

	public void GameOver()
	{
		CoinInfo.Instance.m_GameOver_Obj.Phase01();
		CoinInfo.Instance.m_GameOver = true;
		Time.timeScale = 0.0f;
	}

	public void GazuaActive(bool Active)
	{
		m_Gazua.GetComponent<SpriteRenderer>().enabled = Active;
	}
	public void GameResult()
	{
		CoinInfo.Instance.m_GameOver = true;
		Time.timeScale = 0.0f;
		CoinInfo.Instance.SellAllCoin(false);
		CoinInfo.Instance.m_GameOver_Obj.GameResult();
	}

	IEnumerator Blink()
	{
		m_SpriteRender.enabled = false;
		yield return new WaitForSecondsRealtime(0.2f);
		m_SpriteRender.enabled = true;
		yield return new WaitForSecondsRealtime(0.2f);
		m_SpriteRender.enabled = false;
		yield return new WaitForSecondsRealtime(0.2f);
		m_SpriteRender.enabled = true;
		yield return new WaitForSecondsRealtime(0.2f);
		m_SpriteRender.enabled = false;
		yield return new WaitForSeconds(0.2f);
		m_SpriteRender.enabled = true;
	}
	
	public void AniInit()
	{
		m_Animator.SetBool("LeftUpToLeftDown", false);
		m_Animator.SetBool("RightUpToRightDown", false);
		m_Animator.SetBool("LeftDownToRightUp", false);
		m_Animator.SetBool("RightDownToLeftUp", false);
	}
	public void AniInit_R()
	{
		// Reverse
		m_Animator.SetBool("RightUpToLeftDown", false);
		m_Animator.SetBool("LeftDownToLeftUp", false);
		m_Animator.SetBool("LeftUpToRightDown", false);
		m_Animator.SetBool("RightDownToRightUp", false);
	}
}
