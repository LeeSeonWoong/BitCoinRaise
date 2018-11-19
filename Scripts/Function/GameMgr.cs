using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour {

	// Inspector
	[SerializeField] Player m_Player;
	[SerializeField] ResultBox m_Result;
	[SerializeField] SellManMgr m_SellMgr;
	[SerializeField] BuyManMgr m_BuyMgr;
	[SerializeField] BitCoinManager m_CoinMgr;
	[SerializeField] SellBtn m_SellBtn;
	[SerializeField] BuyBtn m_BuyBtn;
	[SerializeField] GameOver m_GameOver;
	[SerializeField] Gazua m_Gazua;
	[SerializeField] Menu m_Menu;
	[SerializeField] Mute m_InGameMute;
	[SerializeField] Mute m_MenuMute;

	// Function
	public void InitInGame()
	{
		CoinInfo.Instance.CoinInfoInit();

		m_Menu.GameStart();
		m_GameOver.GameStart();
		m_Gazua.GameStart();
		m_CoinMgr.GameStart();
		m_Player.GameStart();
		m_Result.ResultInit();
		m_BuyMgr.GameStart();
		m_SellMgr.GameStart();
		CoinInfo.Instance.m_Goods.GameStart();
		m_BuyBtn.GameStart();
		m_SellBtn.GameStart();
		m_InGameMute.MuteCheck();
	}

	public void GameOverSet()
	{
		m_MenuMute.MuteCheck();
		m_Menu.GameStart();
		CoinInfo.Instance.m_SoundMgr.StopAllSound();
	}
	public void DataReset()
	{
		CoinInfo.Instance.DataReset();
		GameOverSet();
		SceneManager.LoadScene("Title");
		//Application.Quit();
	}
}
