using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    InputListener m_Listener;

    public InputListener listner { set { m_Listener = value;  } }

	bool DebugMode = true;
	bool TouchEnable;
	Vector2 ScreePos;

	private void Awake()
	{
		TouchEnable = false;
	}

	void Update ()
    {
		// 0 == begin / 1 == end 로 칭한다
		//if (Input.GetMouseButtonDown(0) && TouchEnable == false) // TouchBegin
		//{
		//	TouchEnable = true;
		//	ScreePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	ScreePos.x *= 100;
		//	ScreePos.y *= 100;
		//	m_Listener.ValueSet((int)TouchState.Begin, ScreePos);
		//	if (DebugMode)
		//	{
		//		Debug.Log("MousePos :" + ScreePos.x + " " + ScreePos.y);
		//		Debug.Log("SceendWidth:" + Screen.width + "/ScreenHeight:" + Screen.height);
		//	}
		//}
		//else if (Input.GetMouseButtonUp(0) && TouchEnable) // TouchEnd
		//{
		//	TouchEnable = false;
		//	ScreePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	ScreePos.x *= 100;
		//	ScreePos.y *= 100;
		//	m_Listener.ValueSet((int)TouchState.End, ScreePos);
		//}
	}
}
