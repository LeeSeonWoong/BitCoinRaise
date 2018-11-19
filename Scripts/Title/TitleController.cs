using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour{

	[SerializeField] SoundMgr m_SoundMgr;
    public Animator FadeOutAnimator;
	void Start ()
	{
		m_SoundMgr.PlaySoundLoop(0);
	}
	
	
	public void SceneLoad()
	{
		if (FadeOutAnimator.enabled == false)
		{
			m_SoundMgr.PlaySound(1);
			FadeOutAnimator.enabled = true;
			m_SoundMgr.SoundFadeOut(2.0f, 0);
			StartCoroutine("Delay");
		}
	}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MenuScene");
    }
}
