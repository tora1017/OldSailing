/***********************************************************************/
/*! @file   ShipManager.cs
*************************************************************************
*   @brief  船を管理するマネージャークラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : BaseObjectSingleton<ShipManager> {

    public virtual void Start()
    {
        ShipInitialize();
    }

    /// <summary>
    /// @brief 継承先で個別に実装する初期化
    /// </summary>
    protected virtual void ShipInitialize()
    {
       
    }

    private int HitMarker;


    // 船のリストを作成
    private List<GameObject> ShipList = new List<GameObject>();
}