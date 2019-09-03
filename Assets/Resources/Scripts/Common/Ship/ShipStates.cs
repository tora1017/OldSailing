/**********************************************************************************************/
/*@file       ShipStates.cs
*********************************************************************************************
* @brief      船に関連する列挙
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;

[Serializable()]
public class ShipStates
{

    private eCameraMode cameraPerspective; // @brief 視点の状態を保持
    private eShipState shipState;

    /// <summary>
    /// @brief  視点の状態を保持する変数のアクセサー
    /// @set    現在の状態を設定
    /// @get    現在の設定を出力
    /// </summary>
    public eCameraMode CameraMode
    {
        get { return cameraPerspective; }
        set { cameraPerspective = value; }
    }


    /// <summary>
    /// @brief  船の状態を保持する変数のアクセサー
    /// @set    現在の状態を設定
    /// @get    現在の設定を出力
    /// </summary>
    public eShipState ShipState
    {
        get { return shipState; }
        set { shipState = value; }
    }
}

#region 列挙体の宣言

/// <summary>
/// @brief 移動に関する列挙
/// </summary>
public enum eShipState
{
    STOP, // 止まっている
    START // 動いている
}

/// <summary>
/// @brief 操作方法に関する列挙
/// </summary>
public enum eShipController
{
    SWIPE, // スワイプ操作
    TILT,  // 傾き操作
}

/// <summary>
/// @brief 視点を表す列挙
/// </summary>
public enum eCameraMode
{
    FPS,
    TPS,
    GOAL
}
#endregion
