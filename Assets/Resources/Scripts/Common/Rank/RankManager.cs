/**********************************************************************************************/
/*@file       RankManager.cs
*********************************************************************************************
* @brief      レース中の順位を判断するスクリプト
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MarkerBase
{
	// GameObject型のフィールドを保持するため、構造体ではなくクラス宣言
	// ShipObjectクラスのフィールドをRankManagerクラスで使用できるようにするため、publicになってます
	// クラスがprivateなので、RankManagerクラス以外では使えないようになっています
	private class ShipObject
	{
		
		public GameObject ship;         // @brief 船オブジェクト
		public float distance;          // @brief マーカーまでの距離
		public int rank;                // @brief 現在のランク
        public int resultRank;          // @brief ゴール時のランク
        public MarkerBase markerBase;   // @brief Player,Enemy両方で使うcurrentMarkerの基底クラス

		public ShipObject(GameObject ship, float distance, int rank, MarkerBase markerBase)
		{
			this.ship = ship;
			this.distance = distance;
			this.rank = rank;
			this.markerBase = markerBase;
		}
	}

    /* 船オブジェクトに関する宣言 */

	private readonly List<ShipObject> allShip             = new List<ShipObject>();     // @brief 全ての船オブジェクトの格納先
	private          List<GameObject> allPlayerShipObject = new List<GameObject>();     // @brief 自機を格納するgameobject型のリスト
	private          List<GameObject> allEnemyShipObject  = new List<GameObject>();     // @brief 敵機を格納するgameobject型のリスト   
	private GameObject parentAllShipName;                                               // @brief 船オブジェクトの親のおbジェクトを格納する用の変数
    
	private int goaledShipNum;  // @brief すでにゴールした船の総数
    private bool allGoal;       // @brief 全ての船がゴールしたか
    private bool callOnce;      // @brief 一度だけ呼びたい関数に使う（他にいい方法あるかも）

	private RankImageRender rankImageRender;  //brief コンポーネント取得先

    /// <summary>
    /// @brief MarkerBaseの実装
    /// </summary>
	protected override void MarkerInitialize()
	{
		base.MarkerInitialize();

        Singleton<SoundPlayer>.Instance.PlaySE("StartRase");

        	// ランク画像を処理しているスクリプトのコンポーネント取得
		GameObject rank = GameObject.Find("Rank");
		rankImageRender = rank.GetComponent<RankImageRender>();

		// マーカーの個数の初期化
		currentMarker = 0;

        // 順位の処理で使う真偽値の初期化
        allGoal = false;
        callOnce = false;

        // 船オブジェクトの取得
		GetShipObject();
	}


	/// <summary>
	/// @brief BaseObjectの実装
	/// </summary>
	public override void OnUpdate()
	{
		base.OnUpdate();

		DistanceShipToMarker();

		CalcRank();

		// ゴールしている場合としていない場合で分岐
		// ship[0] は　player の番号
		if (allShip[0].markerBase.IsGoal)
			rankImageRender.ChangeRankSprite(allShip[0].resultRank);
		else
			rankImageRender.ChangeRankSprite(allShip[0].rank);
	}

	/// <summary>
	/// @brief インスペクター上に存在する船のオブジェクトを全てリストに格納する
	/// </summary>
	private void GetShipObject()
	{
		// 親オブジェクトを取得し
		parentAllShipName = GameObject.Find("AllShip");

		// 取得した親オブジェクトの子も取得する(Player)
		allPlayerShipObject = GameObjectExtension.GetGameObject(parentAllShipName, "Player");

		// 取得した親オブジェクトの子も取得する(Enemy)
		allEnemyShipObject = GameObjectExtension.GetGameObject(parentAllShipName, "Enemy");

		// Playerが一番最初にリストに格納されるように先に処理する
		for (int i = 0; i < allPlayerShipObject.Count; i++)
		{
			allShip.Add(new ShipObject(allPlayerShipObject[i], 0.0f, 1, allPlayerShipObject[i].GetComponent<MarkerColliderTrigger>()));
		}

		// その後ろに敵の処理
		for (int i = 0; i < allEnemyShipObject.Count; i++)
		{
			allShip.Add(new ShipObject(allEnemyShipObject[i],  0.0f, 1, allEnemyShipObject[i].GetComponent<AIControler>()));
		}

	}

    /// <summary>
    /// @brief 次へ向かうマーカーの距離を計算
    /// @note  すでにゴールしていれば計算しない
    /// </summary>
	private void DistanceShipToMarker()
	{
		
		for (int i = 0; i < allShip.Count; i++)
		{
			// ゴールしている場合、リスト外参照でエラーになるため
            // 計算せずに次のリストを参照する
			if (allShip[i].markerBase.IsGoal) continue;

			// ピタゴラスの定理
			allShip[i].distance = ((allShip[i].ship.transform.position.x - hitMarkerList[allShip[i].markerBase.CurrentMarker].transform.position.x) *
			                       (allShip[i].ship.transform.position.x - hitMarkerList[allShip[i].markerBase.CurrentMarker].transform.position.x) +
			                       (allShip[i].ship.transform.position.z - hitMarkerList[allShip[i].markerBase.CurrentMarker].transform.position.z) *
			                       (allShip[i].ship.transform.position.z - hitMarkerList[allShip[i].markerBase.CurrentMarker].transform.position.z));
		}	 
	} 

    /// <summary>
    /// @brief 通ったマーカー・次のマーカーまでの距離から、ランクを計算する
    /// @note  処理の順番は絶対に変えないでください
    /// </summary>
	private void CalcRank()
	{
		/* 計算するにあたっての初期化 */
        // ゴールした船の数
        goaledShipNum = 0;

        // ゴールしている船を数える
		for (int i = 0; i < allShip.Count; i++)
		{
            if (allShip[i].markerBase.IsGoal)
            {
                goaledShipNum++;
                allGoal = true;
            }
            if (!allShip[i].markerBase.IsGoal)
            {
                allGoal = false;
            }

		}

        // 順位を１で初期化
        // ゴールしている場合は初期化しない
        for (int i = 0; i < allShip.Count; i++)
        {
            if (!allShip[i].markerBase.IsGoal)
            {
                allShip[i].rank = 1;
                allShip[i].rank += goaledShipNum;
            }
        }

        /* 実際の計算 */

        // 通ったマーカーの数で順位を計算
		for (int i = 0; i < allShip.Count; i++)
		{
			for (int j = 1; j < allShip.Count; j++)
			{
                // ゴールしている船は計算しない
                if (allShip[j].markerBase.IsGoal) continue;

                if (i != j)
				{
					// 通ったマーカーが少ない方の順位を加算
					if (allShip[i].markerBase.CurrentMarker < allShip[j].markerBase.CurrentMarker)
						allShip[i].rank++;
                        
                    if (allShip[i].markerBase.CurrentMarker > allShip[j].markerBase.CurrentMarker)
                        allShip[j].rank++;
                    
				}
			}
		}
        
        // 距離で順位を計算
		for (int i = 0; i < allShip.Count; i++)
		{
			for (int j = 1; j < allShip.Count; j++)
			{
                // ゴールしている船は計算しない
                if (allShip[j].markerBase.IsGoal) continue;

				// 通っているマーカーの数が同じなら
				if (i != j && allShip[i].markerBase.CurrentMarker == allShip[j].markerBase.CurrentMarker)
				{
					// 距離が開いている方の順位を加算する
					// 同じ時は何もしない
					if (allShip[j].distance < allShip[i].distance) allShip[i].rank++;

					if (allShip[j].distance > allShip[i].distance) allShip[j].rank++;
				}
			}
		}

        // すでにゴールしている船の数だけ順位を加算
		for (int i = 0; i < allShip.Count; i++) 
		{
            if (allGoal && !callOnce)
            {
                // ゴール時の順位を保存
                allShip[i].resultRank = allShip[i].rank;
                BaseObjectSingleton<GameInstance>.Instance.Rank = allShip[i].resultRank;
                callOnce = true;
            }
        }
	}

    /// <summary>
    /// @brief 指定した船の順位を取得する
    /// </summary>
    /// <param name="shipId">船の番号</param>
    /// <returns>船の順位</returns>
    public int GetResultRank(int shipId)
    {
        return allShip[shipId].resultRank;
    }
}