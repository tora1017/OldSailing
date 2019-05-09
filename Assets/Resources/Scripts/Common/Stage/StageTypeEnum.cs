/***********************************************************************/
/*! @file   StageTypeEnum.cs
*************************************************************************
*   @brief  ステージのタイプを判別するための列挙体を宣言
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @brief ステージの列挙体
/// @note  選ばれたステージを判断する用
/// </summary>
public enum eStageType
{
    eTutorialStage_Straight,
    eTutorialStage_Curve,
    eTutorialStage_TimeAttack,
    eStage_Easy,
    eStage_Normal,
    eStage_Hard,
    Null
}
