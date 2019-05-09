/**********************************************************************************************/
/*@file       ReturnTitle.cs
*********************************************************************************************
* @brief      シーンを移動する
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitle : BaseObject
{
	[SerializeField] private string returnScene;	// 移動するシーンの名前（パス）

	/// <summary>
	/// @brief シーン移動
	/// </summary>
	public void MoveScene()
	{
		SceneManager.LoadScene(returnScene);
        BaseObjectSingleton<GameInstance>.Instance.IsPorse = false;
	}
}