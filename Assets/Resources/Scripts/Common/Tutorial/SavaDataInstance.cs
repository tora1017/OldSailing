/**********************************************************************************************/
/*! @file     TutorialState.cs
*********************************************************************************************
* @brief      初回チュートリアルの状態を保持する。
*********************************************************************************************
* @note       バイナリファイルに出力させるオブジェクトです。
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;
using UnityEngine;
[Serializable()]
public class SaveDataInstance
{
    // @brief チュートリアルの状態を保持する変数
    private eTutorial tutorialState;

    /// <summary>
    /// @brief  チュートリアルの状態を保持する変数のアクセサー
    /// @set    現在の状態を設定
    /// @get    現在の設定を出力
    /// </summary>
    public eTutorial TutorialStatus
    {
        set 
        { 
            tutorialState = value;
            Debug.Log(tutorialState + "call state");
        }
        get { return tutorialState; }
    }



    private bool isGyro;       // @brief ジャイロ操作のフラグ

    public bool IsGyro
    {
        set { isGyro = value; }
        get { return isGyro; }
    }

    private bool isSwipe;      // @brief スワイプ操作のフラグ

    public bool ISSwipe
    {
        set { isSwipe = value; }
        get { return isSwipe; }
    }

    private float sensitivty;

    public float Sensitivty
    {
        set { sensitivty = value; }
        get { return sensitivty; }
    }

    /* サウンド関連 */
    private float maxBGMVolume;
    private float maxSEVolume;

    public float MaxBGMVolume
    {
        set { maxBGMVolume = value; }
        get { return maxBGMVolume; }
    }

    public float MaxSEVolume
    {
        set { maxSEVolume = value; }
        get { return maxSEVolume; }
    }
}

#region　列挙体の宣言

/// <summary>
/// @brief チュートリアルの列挙体
/// @note 初回チュートリアルの状態を列挙しています。
/// </summary>
public enum eTutorial
{
    eTutorial_ModeSelect,
    eTutorial_Straight,
    eTutorial_Curve,
    eTutorial_EndText,
    eTutorial_End,
    eTutorial_Null,
}
#endregion