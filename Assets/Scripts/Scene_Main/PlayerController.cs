using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	// プレイヤーのアニメーター
	private Animator m_animator;
	// コライダー管理クラス
	private PlayerColliderManager m_colliderManager;

	// プレイヤージャンプ中フラグ
	private bool m_isJump;

	public float STEPVAL = 2f;
	// 移動速度
	private float m_step;
	private Vector3 MOVEZ = new Vector3(0, 0, 1.0f);
	private Vector3 MOVEZ_2Square = new Vector3(0, 0, 2.0f);
	private Vector3 m_target;
	
	// 死亡中フラグ
	private bool m_isDeath;
	// 移動中フラグ
	private bool m_isMove;

	// Use this for initialization
	void Start ()
	{
		Init();
		m_colliderManager = GetComponent<PlayerColliderManager>();
	}

	/// <summary>
	/// 初期化処理
	/// </summary>
	private void Init()
	{
		m_isJump = false;
		m_isDeath = false;
		m_isMove = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !m_isMove)
		{
			// プレイヤーがIdleモーション時に操作を受け付ける
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Debug.Log("1キーを押した");
				StartJumpAction(1);
				
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				Debug.Log("2キーを押した");
				StartJumpAction(2);
			}
		}
		transform.position = Vector3.MoveTowards(transform.position, m_target, m_step * Time.deltaTime);
		if (transform.position == m_target)
		{
			m_isMove = false;
			// MoveTowards完了時、床をチェックする
			CheckFloor();
		}
		else
		{
			m_isMove = true;
		}
	}

	public void StartJumpAction(int num)
	{
		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !m_isMove)
		{
			m_animator.SetTrigger("Jump");

			m_step = STEPVAL * num;
			m_target = transform.position + MOVEZ * num;
		}
	}

	/// <summary>
	/// Jumpアニメーションイベントのコールバック
	/// </summary>
	public void Jump()
	{
		Debug.Log("Jump");
		m_isJump = true;
	}
	/// <summary>
	/// Landアニメーションイベントのコールバック
	/// </summary>
	public void Land()
	{
		Debug.Log("Land");
		m_isJump = false;
	}

	/// <summary>
	/// プレイヤーの足元の床をチェックする
	/// </summary>
	private void CheckFloor()
	{
		if (m_isDeath)
		{
			// 死亡中の場合はスルー
			return;
		}

		// 床をチェック
		if (m_colliderManager.m_state == m_colliderManager.DANGER)
		{
			// DANGERタグの床に触れた場合
			StartCoroutine(Death());
		}
		else if (m_colliderManager.m_state == m_colliderManager.GOAL)
		{
			// GOALタグの床に触れた場合
			StartCoroutine(Goal());
		}
	}

	private IEnumerator Goal()
	{
		m_animator.SetTrigger("Goal");
		yield return null;
	}

	/// <summary>
	/// 死亡処理
	/// </summary>
	public IEnumerator Death()
	{
		m_isDeath = true;

		m_animator.SetTrigger("Death");
		yield return new WaitForSeconds(1f);
		m_target = transform.position - MOVEZ;
		yield return new WaitForSeconds(1f);
		m_animator.SetTrigger("Idle");

		m_isDeath = false;
		yield return null;
	}
}
