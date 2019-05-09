/***********************************************************************/
/*! @file   CanvasBase.cs
*************************************************************************
*   @brief  canvasに関するメソッドをまとめるBaseクラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2018 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBase : BaseObject {

    protected GameObject prefab; // @brief 生成するプレハブリスト

    /// <summary>
    /// @brief Canvas内にオブジェクトを生成するメソッド
    /// </summary>
    /// <param name="obj"> Canvas上に生成するオブジェクト </param>
    public void NewCanvasInGameObject(GameObject obj)
    {
        // 変数にオブジェクトを代入
        prefab = New(obj) as GameObject;

        // 自身の子要素として登録
        prefab.transform.SetParent(this.transform, false);
    }
}
