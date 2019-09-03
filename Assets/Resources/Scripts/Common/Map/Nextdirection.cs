/******************************************************
 * ! @file Nextdirection
 * ****************************************************
 * @brief 次のマーカーの方向を示すスクリプト
 * ****************************************************
 * @author reina sawai
 ****************************************************/
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextdirection : MarkerBase
{

    /* @brief ターゲット(マーカー)の方向へ向く宣言*/
    [SerializeField]   private Transform target;

    /// <summary>
    /// @brief Baseobjectの実装
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        ChangeyazirusiPosition();
    }

    /// <summary>
    /// @brief 矢印とTargetの位置を取得
    /// </summary>
    private void ChangeyazirusiPosition()
    {
        //Targetに入れた位置の取得
        Vector3 _target = target.position;
        //矢印の位置の取得
        _target.y = this.transform.position.y;
        this.transform.LookAt(_target);
        //矢印のx軸を180度足す(矢印を正面にさせるため)
        this.transform.Rotate(180, 0, 0);
    }

}
