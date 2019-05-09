/***********************************************************************/
/*! @file   StageTimer.cs
*************************************************************************
*   @brief  ステージの時間の測定する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : BaseObject {

    [SerializeField]
    private GameObject timeManagerObject; // @brief TimeManagerのオブジェクトを格納する
    private TimeManager timeManager; // @brief timrManagerObjectのスクリプトを格納する
    private bool timerStartFlag; // @brief タイマーの動作状況を管理する

    /// <summary>
    /// @brief 初期化
    /// </summary>
    private void TimerInitialize()
    {
        timeManager = timeManagerObject.GetComponent<TimeManager>();
        timerStartFlag = false;
        //timerStopFlag = false;
    }

    /// <summary>
    /// タイマーを起動する
    /// </summary>
    private void TimerStart()
    {
        if (Singleton<GameInstance>.Instance.IsShipMove &&
            !timerStartFlag)
        {
            timeManager.TimerSwich();
            timerStartFlag = !timerStartFlag;
        }
    }

    /// <summary>
    /// @brief タイマーを停止する
    /// </summary>
    private void TimerStop()
    {
        if (Singleton<GameInstance>.Instance.IsGoal)
        {
            timeManager.TimerSwich();
            Delete(gameObject);
        }
    }

    private void Start()
    {
        TimerInitialize();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        TimerStart();
        TimerStop();
    }
}
