/***********************************************************************/
/*! @file   CreateAnimaShipObject.cs
*************************************************************************
*   @brief  タイトル用の船を生成するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnimaShipObject : BaseObject {

    [SerializeField]
    private GameObject animaShip; // @brief 生成する船を格納

    void Start () {
        GameObject gameObj = New<GameObject>(animaShip); // 生成
    }
}
