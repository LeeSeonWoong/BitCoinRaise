using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AudioInfo
{
	public float Delay;
	public float save_time;
	public bool loop;
	public int index;
}
public class SoundMgr : MonoBehaviour {

	// Inspector
	AudioSource[] m_Audio;
	[SerializeField] AudioClip[] AudioClips = new AudioClip[5];
	public bool Mute = false;

	// Function
	private void Awake()
	{
		m_Audio = new AudioSource[AudioClips.Length];
		for(int i = 0; i < AudioClips.Length; i++)
		{
			m_Audio[i] = gameObject.AddComponent<AudioSource>();
			m_Audio[i].Stop();
			m_Audio[i].clip = AudioClips[i];
			m_Audio[i].loop = false;
			m_Audio[i].playOnAwake = false;
		}
		CoinInfo.Instance.m_SoundMgr = this;
	}
	public void PlaySound(int index)
	{
		if(Mute == false)
		{
			m_Audio[index].Play();
		}
	}

	public void StopSound(int index)
	{
		m_Audio[index].Stop();
	}
	public void PauseSound(int index)
	{
		m_Audio[index].Pause();
	}
	public void PlaySoundLoop(int index)
	{
		if(Mute == false)
		{
			m_Audio[index].loop = true;
			if (m_Audio[index].isPlaying == false)
			{
				m_Audio[index].Play();
			}
		}
	}

	public void PlayDelay(float delayTime, bool loop, int index)
	{
		AudioInfo info;
		info.Delay = delayTime;
		info.index = index;
		info.loop = loop;
		info.save_time = 0.0f;
		StartCoroutine("PlayDelayC", info);
	}

	IEnumerator PlayDelayC(AudioInfo info)
	{
		yield return new WaitForSeconds(info.Delay);
		if(Mute == false )
		{
			m_Audio[info.index].loop = info.loop;
			m_Audio[info.index].Play();
		}
	}

	// 사운드 FadeOut
	public void SoundFadeOut(float time, int index)
	{
		AudioInfo info;
		info.Delay = time;
		info.save_time = 0.0f;
		info.index = index;
		info.loop = false;
		StartCoroutine("SoundFadeOutC", info);
	}

	IEnumerator SoundFadeOutC(AudioInfo info)
	{
		info.save_time += Time.deltaTime;
		if(m_Audio[info.index] != null)
		{
			if(info.save_time < info.Delay)
			{
				float rate = 1 - (info.save_time / info.Delay);
				m_Audio[info.index].volume = rate;
				yield return null;
				StartCoroutine("SoundFadeOutC", info);
			}
			else
			{
				m_Audio[info.index].volume = 1.0f;
				if(info.index == 0)
					m_Audio[info.index].Pause();
				else
					m_Audio[info.index].Stop();
			}
		}
	}

	public void StopAllSound()
	{
		for (int i = 0; i < m_Audio.Length; i++)
			m_Audio[i].Stop();
	}

	public void MuteActive(bool value)
	{
		for (int i = 0; i < m_Audio.Length; i++)
			m_Audio[i].mute = value;
	}
}
