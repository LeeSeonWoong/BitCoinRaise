using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
	public GameObject pf_BG1;
	public GameObject pf_BG2;
	[SerializeField] Player m_Player;

	Transform m_BG1_Trasnf;
	Transform m_BG2_Trasnf;
	Transform m_Player_Transf;

	private void Start()
	{
		m_BG1_Trasnf = pf_BG1.GetComponent<Transform>();
		m_BG2_Trasnf = pf_BG2.GetComponent<Transform>();
		m_Player_Transf = m_Player.gameObject.GetComponent<Transform>();
	}
	private void Update()
	{
		if (m_BG1_Trasnf.position.y < m_Player_Transf.position.y)
			m_BG2_Trasnf.position = new Vector3(m_BG2_Trasnf.position.x, m_BG1_Trasnf.position.y + 19.8f, m_BG1_Trasnf.position.z);
		else if (m_BG2_Trasnf.position.y < m_Player_Transf.position.y)
			m_BG1_Trasnf.position = new Vector3(m_BG1_Trasnf.position.x, m_BG2_Trasnf.position.y + 19.8f, m_BG1_Trasnf.position.z);
	}

}
