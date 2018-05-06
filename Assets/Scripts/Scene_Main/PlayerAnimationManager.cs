using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
	private PlayerController m_playerController;

	void Start()
	{
		m_playerController = this.transform.root.gameObject.GetComponent<PlayerController>();
	}

	void Update()
	{
		this.transform.localPosition = Vector3.zero;
	}

	/// <summary>
	/// Jumpアニメーションイベント
	/// </summary>
	public void Jump()
	{
		m_playerController.Jump();
	}
	/// <summary>
	/// Landアニメーションイベント
	/// </summary>
	public void Land()
	{
		m_playerController.Land();
	}
}
