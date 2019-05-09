/**********************************************************************************************/
/*! @file     ShipStartupMove.cs
*********************************************************************************************
* @brief      InGameでの船の発進時のセットアップ
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStartupMove : BaseObject
{

    public void Start()
    {
        StartCoroutine(ShipSetup());
    }

    /// <summary>
    /// @brief 読み込み中の空白時間を埋めるコルーチン
    /// </summary>
    public IEnumerator ShipSetup()
    {
        
        yield return new WaitForSeconds(10.5f);
        BaseObjectSingleton<GameInstance>.Instance.IsCountDown = true;

    }
}
