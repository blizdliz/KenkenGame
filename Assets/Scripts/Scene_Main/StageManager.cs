using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common.unity.Singleton;

/// <summary>
/// ステージを管理する
/// </summary>
public class StageManager : SingletonMonoBehaviour<StageManager>
{
	const int FLOOR_START = 0;
	const int FLOOR_END = 1;
	const int FLOOR_SAFE = 2;
	const int FLOOR_DANGER = 3;

	// 床の数
	public int m_floorNum;

	// 床の構成を格納するリスト
	private List<int> m_floorList;
	// 強制的にSafe床をセットするフラグ
	private bool m_forcedSafe;

	[SerializeField]
	private GameObject[] m_floorObjs;

	/// <summary>
	/// 初期化処理
	/// </summary>
	public void Init()
	{
		// 床のリストを初期化
		InitFloorList();
		// 床を生成
		CreateFloorObj();
	}

	/// <summary>
	/// 床のリストを初期化する
	/// </summary>
	public void InitFloorList()
	{
		m_floorList  = new List<int>();
		m_forcedSafe = false;

		// リストにスタートのアイテムを追加
		m_floorList.Add(FLOOR_START);

		for (int i = 0; i < m_floorNum; i++)
		{
			int item = FLOOR_SAFE;
			if (m_forcedSafe)
			{
				// 強制Safe床フラグがONのとき
				item = FLOOR_SAFE;
			}
			else
			{
				// それ以外のとき
				item = Random.Range(FLOOR_SAFE, FLOOR_DANGER + 1);
			}
			// リストにアイテムを追加
			m_floorList.Add(item);

			// Safe床以外がセットされた場合は強制Safe床設置フラグをONにする
			if (item != FLOOR_SAFE)
			{
				m_forcedSafe = true;
			}
			else
			{
				m_forcedSafe = false;
			}
		}

		// リストにゴールのアイテムを追加
		m_floorList.Add(FLOOR_END);
	}

	/// <summary>
	/// 床のオブジェクトを生成する
	/// </summary>
	private void CreateFloorObj()
	{
		for (int i = 0; i < m_floorList.Count; i++)
		{
			GameObject obj = GameObject.Instantiate(m_floorObjs[m_floorList[i]]);
			obj.transform.position = new Vector3(0, 0.01f, obj.transform.localScale.x * i);
		}
	}
}
