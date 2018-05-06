using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
	public int START = 0;
	public int GOAL = 1;
	public int SAFE = 2;
	public int DANGER = 3;

	private PlayerController m_playerController;

	public int m_state { get; set; }

	void Start()
	{
		m_state = SAFE;
		m_playerController = this.gameObject.GetComponent<PlayerController>();
	}

	/// <summary>
	/// 当たり判定スクリプト
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "floorDanger")
		{
			Debug.Log("OnTriggerEnter:floorDanger");
			m_state = DANGER;
		}
		else if (other.gameObject.tag == "floorGoal")
		{
			Debug.Log("OnTriggerEnter:floorGoal");
			m_state = GOAL;
		}
		else
		{
			m_state = SAFE;
		}
	}
}
