/**********************************************************************************************/
/*@file       SlopeShipControl.cs
*********************************************************************************************
* @brief      ユーザーが加速度センサーを用いた場合の処理を行う
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2017 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeShipControl : BaseObject
{
	// ※ 動作確認のため、変数すべてにSerializeFieldをつけています。不要な場合はなくても大丈夫です。

	// private:
	[SerializeField] private float slopeVector;			// @brief 加速度センサーのｘ軸の値を取得
	[SerializeField] private bool onSlope;				// @brief 加速度センサーを使用するかどうかのフラグ

	// Accessor
	[SerializeField] private string slopeDir;			// @brief 傾けた距離
	[SerializeField] private float moveAcceleration;	// @brief 移動加速度（→slopeVectorが大きいほど高くなる）

	/// <summary>
	/// @brief スワイプの方向
	/// @get スワイプの方向を取得
	/// @set スワイプの方向を代入
	/// </summary>
	public string AccessorSlopeDir
	{
		get { return slopeDir; }
		private set { slopeDir = value; }
	}
	/// <summary>
	/// @brief 移動加速度
	/// @get 移動加速度を取得
	/// @set 傾きの度合に応じて、移動加速度をセット
	/// </summary>
	public float AccessorMoveAcceleration
	{
		get { return moveAcceleration; }
		private set { moveAcceleration = 1 + Mathf.Abs(value); }
	}

	/// <summary>
	/// @brief 変数の初期化
	/// </summary>
	void Start()
	{
		onSlope = false;
		slopeDir = "None";
	}
	/// <summary>
	/// @brief 加速度センサー処理
	/// </summary>
	public override void OnUpdate()
	{
		if (onSlope)
		{
			slopeVector = Input.acceleration.x;
			SetSlope();
			AccessorMoveAcceleration = slopeVector;
			Move();
		}
		else if (AccessorSlopeDir != "None")
		{
			AccessorSlopeDir = "None";
		}
	}

	/// <summary>
	/// @brief 加速度センサーの値に応じて、移動させる方向をセット
	/// </summary>
	void SetSlope()
	{
		if (slopeVector > 0.1) { AccessorSlopeDir = "Right"; }
		else if (slopeVector < -0.1) { AccessorSlopeDir = "Left"; }
		else { AccessorSlopeDir = "None"; }
	}
	/// <summary>
	/// @brief Test：移動確認
	/// </summary>
	void Move()
	{
		switch (AccessorSlopeDir)
		{
			case "Left":
				this.transform.position -= new Vector3(0.1f * AccessorMoveAcceleration, 0.0f, 0.0f);
				break;

			case "Right":
				this.transform.position += new Vector3(0.1f * AccessorMoveAcceleration, 0.0f, 0.0f);
				break;
		}
	}
}
