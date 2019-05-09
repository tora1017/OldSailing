/***********************************************************************/
/*! @file   ChangeCamera.cs
*************************************************************************
*   @brief  船のカメラ視点を切り替えるクラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2018 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{

    /// <summary>
    /// @brief 視点変更ボタンに入れる
    /// @none  押されたボタンの引数により視点が変わる
    /// </summary>
    public void OnTap(int changeCamera)
    {
        // FPS
        if (changeCamera == 0)
        {
            Singleton<ShipStates>.Instance.CameraMode = eCameraMode.FPS;
        }
        else if (changeCamera == 1)
        {
            Singleton<ShipStates>.Instance.CameraMode = eCameraMode.TPS;
        }
    }
}
