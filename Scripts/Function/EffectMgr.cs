using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMgr : MonoBehaviour {

	// Inspector ////////////////////////////////////////////////////////////////////
	public GameObject MakeCoinEffect;

	Dictionary<string, GameObject> m_EffectDic = new Dictionary<string, GameObject>();

	// Function //////////////////////////////////////////////////////////////////////
	private void Start()
	{
		CoinInfo.Instance.m_EffectMgr = this;
		m_EffectDic.Add("MakeCoinEffect", MakeCoinEffect);
	}

	public void CallEffect(string EffectName, Vector2 pos, bool flip)
	{
		GameObject obj = Instantiate(m_EffectDic[EffectName], new Vector3(pos.x, pos.y, -4), Quaternion.identity);
		obj.GetComponent<SpriteRenderer>().flipX = flip;
	}
}
