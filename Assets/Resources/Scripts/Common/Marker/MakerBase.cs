/**********************************************************************************************/
/*@file       MarkerBase.cs
*********************************************************************************************
* @brief      Marker系の基底クラス
*********************************************************************************************
* @note       MarkerBaseを継承する場合は,Start()は使用せずにMarkerInitialize() を使用してください。
**********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class MarkerBase : BaseObject
{

	protected GameObject markerObjName;       // @brief markerのオブジェクトの名前を取得
	protected int currentMarker;              // @brief 現在の配列番号
	protected int currentHitMarker;           // @brief 現在の配列番号

	protected List<GameObject> markerList;    // @brief markerのリスト
	protected List<GameObject> hitMarkerList; // @brief hitMarkerのリスト

	protected bool isGoal;            // @brief ゴールしたかどうか

	/// <summary>
	/// @brief すでに通ったマーカーの個数を返すアクセサ
	/// </summary>
	/// <get> すでに通ったマーカーの個数 </get>
	public virtual int CurrentMarker
	{
		get { return currentMarker; }
	}

	/// <summary>
	/// @brief ゴールしたかどうかを返すアクセサ
	/// </summary>
	/// <value>
	/// <c> true  </c> すでにゴールした
	/// <c> false </c> まだゴールしてない  
	/// </value>
	public bool IsGoal
	{
		get { return isGoal; }
	}


	public virtual void Start()
	{
		MarkerInitialize();
	}


	/// <summary>
	/// @brief 継承先で個別に実装する初期化
	/// </summary>
	protected virtual void MarkerInitialize()
	{
		markerList = new List<GameObject>();

		hitMarkerList = new List<GameObject>();

		// 親オブジェクトを取得し
		markerObjName = GameObject.Find("MarkerObj");

		// 取得した親オブジェクトの子も取得する(Marker)
		markerList = GameObjectExtension.GetGameObject(markerObjName, "Marker");

		// 取得した親オブジェクトの子も取得する(GameObject)
		hitMarkerList = GameObjectExtension.GetGameObject(markerObjName);

		isGoal = false;

	}
}
