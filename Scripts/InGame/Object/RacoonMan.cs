using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacoonMan : MonoBehaviour {

	// Inspecotr
	[SerializeField] Razer m_Razer;
	[HideInInspector] public bool m_Active;
	GameObject instance;

	// Function
	private void Awake()
	{
		instance = gameObject;
	}

	// 너굴맨 작동
	public void RacoonManActive()
	{
		m_Active = true;
		float ypos = CoinInfo.Instance.m_BitCoinMgr.m_Coin_List[CoinInfo.Instance.m_BitCoinMgr.m_Coin_List.Count - 1].transform.position.y + 1.2f;
		transform.position = new Vector3(-1.3f, ypos, transform.position.z);
		StartCoroutine("MoveRacoon");
	}
	IEnumerator MoveRacoon()
	{
		CoinInfo.Instance.m_CocosFunc.MoveToX(0.35f, new Vector2(2.4f, 0.0f), true, ref instance);
		yield return new WaitForSeconds(0.65f);
		RazerActive();
		yield return new WaitForSeconds(1.0f);
		CoinInfo.Instance.m_CocosFunc.MoveToX(0.4f, new Vector2(-2.4f, 0.0f), true, ref instance);
		yield return new WaitForSeconds(0.4f);
		m_Active = false;
	}
	// 레이저 작동시작
	public void RazerActive()
	{
		m_Razer.SetActiveAnimator();
	}
}
