 /*********************************************************************************************/
/*@file       AIControler.cs
*********************************************************************************************
* @brief      AIの挙動を制御するクラス
*********************************************************************************************
* @note       継承不可
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

sealed public class AIControler : MarkerBase
{

	/// <summary>
	/// @brief 実行速度を速くするための座標構造体
	/// </summary>
	private struct Point
	{
		public float x, z;

		public Point(float x, float z)
		{
			this.x = x;
			this.z = z;
		}
	}

	/// <summary>
	///  @brief AIの行動の状態
	/// </summary>
	private enum eAIStatus
	{
		eTURNING,               // @brief 旋回 
		eNORMAL,                // @brief 通常
		NULL                    // @brief どれでもない
	}


	/* AIに使用する変数についての宣言 */

	private float ownRad;          // @brief AIの現在の角度
	private float turnRad;         // @brief 旋回する角度(ラジアン値)
	private float turnDeg;         // @brief 旋回する角度(度数値)

    private eAIStatus AIMovingType;     // @brief 状態の変更を表す
    
	private readonly Vector3 rotateL = new Vector3(0f, -2f, 0f);        // @brief 左旋回用変数
	private readonly Vector3 rotateR = new Vector3(0f, 2f, 0f);         // @brief 右旋回用変数
	private          float aISpeed = 20.0f;      // @brief AIの直進速度
    private float aITopSpeed;   // @brief AIの最大速度
    private float aITurnSpeed;  // @brief AIの旋回速度


	/* マーカーの座標やリストについての宣言 */
    
	private List<Point> markerPos = new List<Point>();         // @brief マーカーの座標を格納するリスト



	/// <summary>
	/// @brief MarkerBaseの実装
	/// @note  AI関連の初期化
	/// </summary>
	protected override void MarkerInitialize()
	{
		base.MarkerInitialize();
		currentMarker = 0;
		currentHitMarker = 1;
		GetMarkerPoint();
		GetNextTurnRad();
		transform.rotation = Quaternion.Euler(0, turnDeg, 0);
        aITopSpeed = Random.Range(19.0f, 21.0f);   // @brief AIの最大速度
        aITurnSpeed = Random.Range(19.0f, 20.5f);  // @brief AIの旋回速度

	}

	/// <summary>
	/// @brief BaseObjectの実装
	/// </summary>
	public override void OnUpdate()
	{
		base.OnUpdate();

        if(Singleton<ShipStates>.Instance.ShipState == eShipState.STOP)
        {
            transform.position -= transform.forward * aITopSpeed * Time.deltaTime;    
        }   

		if (!Singleton<GameInstance>.Instance.IsShipMove) return;
		
		switch (AIMovingType)
		{

			case eAIStatus.eTURNING:
				Turning(GetNextTurnRad());
				break;

			case eAIStatus.NULL:
				Move();
				break;
		}
	}

	/// <summary>
	/// @brief マップのマーカーの全ての座標を取得
	/// </summary>
	private void GetMarkerPoint()
	{
		for (int i = 0; i < hitMarkerList.Count; i++)
		{
			// リストへ格納
			markerPos.Add(new Point(hitMarkerList[i].gameObject.transform.position.x,
									hitMarkerList[i].gameObject.transform.position.z));
		}
	}

	/// <summary>
	/// @brief 現在のAIの向いている角度と、そこから次のマーカーまでの角度を求める
	/// </summary>
	/// <returns> 度数値で旋回角度 </returns>
	private float GetNextTurnRad()
	{
		// 現在の角度を格納
		ownRad = transform.localEulerAngles.y;

		// 次のマーカーまでの角度を計算
		turnRad = Mathf.Atan2(markerPos[currentHitMarker].z - transform.position.z,
							  markerPos[currentHitMarker].x - transform.position.x);

		// 度数に変換
		turnDeg = RadToDeg(turnRad) * -1;

		return turnDeg;
	}

	/// <summary>
	/// @brief ラジアン値を度数に変換
	/// </summary>
	/// <returns> 度数値 </returns>
	/// <param name="rad"> 度数に変換したいラジアン値 </param>
	private float RadToDeg(float rad)
	{
		// 誤差の調整のため＋８７
		return rad * Mathf.Rad2Deg + 87;
	}


	/// <summary>
	/// @brief 引数で角度を渡すとそこまで旋回する
	/// </summary>
	/// <param name="deg"> 旋回する角度 </param>
	private void Turning(float deg)
	{
       
		/*　次のマーカーまでの角度が１度以上あったら旋回　*/

		// 左旋回
		if (Mathf.DeltaAngle(ownRad, deg) < 0)
		{
			transform.Rotate(rotateL);
			aISpeed = aITopSpeed;
		}

		// 右旋回
		else if (Mathf.DeltaAngle(ownRad, deg) > 1)
		{
			transform.Rotate(rotateR);
			aISpeed = aITopSpeed;
		}
		// 直進
		else
		{
			aISpeed = aITopSpeed;
		}

		Move();
	}

	/// <summary>
	/// @brief 直進
	/// </summary>
	private void Move()
	{
		transform.position += transform.forward * -(aISpeed) * Time.deltaTime;
	}



	/// <summary>
	/// Ons the trigger enter.
	/// </summary>
	/// <param name="other"> アタッチされているオブジェクト以外 </param>
	private void OnTriggerEnter(Collider other)
	{
		// 当たったゲームオブジェクトが、目的のマーカーの場所と一致した場合
		if (other.gameObject == hitMarkerList[currentHitMarker].gameObject)
		{        
			// スタートとゴールが同じ場所にあった時にゴール判定にならないようにする処理
			if (other.tag == "goal")
			{
				AIMovingType = eAIStatus.NULL;
				isGoal = true;
                currentHitMarker = 1;
			}
			else
			{
				// AIの状態を旋回に変更し、マーカーの参照位置を次のマーカーへ移動
				AIMovingType = eAIStatus.eTURNING;
				currentHitMarker += 1;
			}
		}

		if(other.gameObject == hitMarkerList[currentMarker].gameObject && other.tag != "goal")
		{
			// 現在通ったマーカーの総数を計算
			currentMarker++;
		}
	}
}

