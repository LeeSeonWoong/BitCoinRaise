using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoney : MonoBehaviour {

	//Inspector
	Animator m_Animator;

	//Function
	private void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Animator.enabled = false;
	}
	public void AnimatorOn()
	{
		m_Animator.enabled = true;
	}
	public void AnimatorOff()
	{
		m_Animator.enabled = false;
	}
}
