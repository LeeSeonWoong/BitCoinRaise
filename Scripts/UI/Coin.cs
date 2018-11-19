using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

	// Inspector
	public Text m_CurTxt;
	public Text m_ChangeTxt;
	public int m_CoinNum;
	public Image m_Icon;

	public Text m_CoinName;
	public Text m_CoinSym;

	[HideInInspector] public double m_SavePrice;

	private void Start()
	{
		m_SavePrice = CoinInfo.Instance.m_CoinsPrice[m_CoinNum];
	}
}

