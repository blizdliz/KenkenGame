using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common.unity.Singleton;

public class SceneMain_SceneManager : SingletonMonoBehaviour<SceneMain_SceneManager>
{
	// シーン遷移管理用クラス
	private AllSceneManager m_allSceneManager;

	// Use this for initialization
	void Start()
	{
		GameObject allSceneManagerObj = GameObject.FindGameObjectWithTag("AllSceneManager");
		if (allSceneManagerObj == null)
		{
			// AllSceneManagerオブジェクトを生成
			allSceneManagerObj = Instantiate(Resources.Load("Prefabs/AllSceneManager")) as GameObject;
		}
		// AllSceneManagerクラスを取得
		m_allSceneManager = allSceneManagerObj.GetComponent<AllSceneManager>();

		// GUIクラスの初期化

		// ステージマネージャーの初期化
		StageManager.Instance.Init();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
