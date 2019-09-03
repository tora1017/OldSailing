/***********************************************************************/
/*! @file   TitleShipAnimation.cs
*************************************************************************
*   @brief  タイトルの船を動かすスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleShipAnimation : BaseObject {

  
    private bool animaShipMoveFlag; // @brief 船が動くかどうか

    public void Start()
    {
        animaShipMoveFlag = false; // 初期はfalse 
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // 500分の1の確率で船を動かすフラグをtrueにする
        if (!animaShipMoveFlag && Random.Range(0, 20) == 0)
        {
            animaShipMoveFlag = true;
        }
        // 0.1ずつ前進
        if (animaShipMoveFlag)
        {
            this.transform.Translate(0, 0, -0.1f);
        }
        // 船のx座標が-50になったら初期位置の50に戻し、動けなくする
        if(this.transform.position.x < -50)
        {
            this.transform.SetPosX(50);
            animaShipMoveFlag = false;
        }
    }
}
