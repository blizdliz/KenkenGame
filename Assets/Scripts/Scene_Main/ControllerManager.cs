using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common.unity.Singleton;

public class ControllerManager : SingletonMonoBehaviour<ControllerManager>
{
	[SerializeField]
	private PlayerController m_playerController;

	/// <summary>
	/// 1マスジャンプ
	/// </summary>
	public void PointerDown_1Jump()
	{
		Debug.Log("PointerDown_1Jump");
		m_playerController.StartJumpAction(1);
	}

	/// <summary>
	/// 2マスジャンプ
	/// </summary>
	public void PointerDown_2Jump()
	{
		Debug.Log("PointerDown_2Jump");
		m_playerController.StartJumpAction(2);
	}
}
