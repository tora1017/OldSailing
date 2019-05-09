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
**********************************************************************************************
using System;

[Serializable()]
public class TutorialState
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
        set { tutorialState = value; }
        get { return tutorialState; }
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
*/