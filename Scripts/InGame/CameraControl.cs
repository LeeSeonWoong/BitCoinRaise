using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Player player;

	private void Awake()
	{
		Screen.SetResolution(720, 1280, false);
	}
	
	void Update ()
	{
		//플레이어 CameraMove가 On이면
		//if (CoinInfo.Instance.m_GameOver == false)
		{
			if (player.m_CameraMove)
			{
				Vector3 playerPos = player.transform.position;
				playerPos.z = transform.position.z;
				//if (playerPos.y >= 6.4f)
				{
					gameObject.transform.position = playerPos;
				}
			}
		}
	}
}
