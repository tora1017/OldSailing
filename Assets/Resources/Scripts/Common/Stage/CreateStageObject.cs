/***********************************************************************/
/*! @file   CreateStageObject.cs
*************************************************************************
*   @brief  ステージを生成するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageObject : BaseObject
{

	private List<GameObject> StageObject = new List<GameObject>(); // 生成をしたいステージを格納する配列

    /// <summary>
    /// @brief ステージの初期化関数
    /// </summary>
    private void StageInitialize()
    {
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/Tutorial_Accele"));
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/Tutorial_Curve"));
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/SampleStage1"));
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/Stage_Easy"));
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/Stage_Normal"));
		StageObject.Add((GameObject)Resources.Load("Prefabs/Stage/Stage_Hard"));
    }

    /// <summary>
    /// @brief ステージの生成関数
    /// </summary>
    /// <param name="eStageType"></param>
    private void CreateStage(eStageType eStageType)
    {
        if(New(StageObject[(int)eStageType]) == null)
        {
			New(StageObject[(int)eStageType.eTutorialStage_Straight]);
			Debug.LogError("<color=red>" + eStageType + "が参照できません。パスが間違っていないか確認してください。</color>");
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        StageInitialize();
        CreateStage(GameInstance.Instance.StageType);
    }
}