using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct TouchInfo
{
	public int id;				//터치 아이디
	public int state;			//터치 상태 ( Begin / Move / End )
	public Vector2 pos;			//갱신된 터치 포지션
	public Vector2 first_pos;   //저장된 터치 포지션
	//public bool Check;			//터치 Enable 체크
}


public class TouchManager : MonoBehaviour {

	InputListener m_Listener;
	public InputListener listner { set { m_Listener = value; } }

	// 디버그용
	bool ForDebug = true;
	public Text TouchDebug;

	private void Update()
	{
		//////////////// TEST ////////////////////
		//if (ForDebug)
		//{
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		Vector2 ScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//		ScreenPos.x *= 100;
		//		ScreenPos.y *= 100;
		//		m_Listener.ValueSet(new Vector2Int(0, (int)TouchPhase.Began), ScreenPos);
		//	}
		//	else if (Input.GetMouseButtonUp(0))
		//	{
		//		Vector2 ScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//		ScreenPos.x *= 100;
		//		ScreenPos.y *= 100;
		//		m_Listener.ValueSet(new Vector2Int(0, (int)TouchPhase.Moved), ScreenPos);
		//	}
		//}
		////////////////////////////////////////////

		int count = Input.touchCount;// 터치 횟수 저장
		if (count == 0)
		{
			TouchDebug.text = null;
			TouchDebug.text += "null";
			return;
		}
		
		for (int i = 0; i < count; i++)
		{
			Touch touch = Input.GetTouch(i);
			int id = touch.fingerId; // 손가락 ID 정보 저장

			Vector2 ScreenPos = Camera.main.ScreenToWorldPoint(touch.position);
			ScreenPos.x *= 100;
			ScreenPos.y -= Camera.main.transform.position.y - 6.4f;
			ScreenPos.y *= 100;

			//디버그
			if (ForDebug)
			{
				TouchDebug.text = null;
				TouchDebug.text += "ID:" + id + "i:" + i + "pos:" + ScreenPos.x + ", " + ScreenPos.y + "pha:" + (int)touch.phase;
			}
			//상태에 따라 이벤트 호출
			m_Listener.ValueSet(new Vector2Int(id, (int)touch.phase),ScreenPos);
		}
	}

	
}
