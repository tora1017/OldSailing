/**************************************************************************************/
/*! @file    Timer.cs
***************************************************************************************
*@brief      デバッグ用クラス。関数処理の計測に使います。
***************************************************************************************
*@author     Ryo Sugiyama
***************************************************************************************
* Copyright  2018 Ryo Sugiyama All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Timer : BaseObject
{
    private static Stopwatch stopwatch = new Stopwatch();

    /// <summary>
    /// @brief 現在の処理時間の合計をfloatで各単位に変換したもの(上からミリ秒、秒)
    /// </summary>
    public static float TotalMilliseconds
    {
        get
        {
            return (float)stopwatch.Elapsed.TotalMilliseconds;
        }
    }
    public static float TotalSeconds
    {
        get
        {
            return (float)stopwatch.Elapsed.TotalSeconds;
        }
    }

    //計測中かどうかのフラグ
    public static bool IsRunning 
    {
        get
        { 
            return stopwatch.IsRunning; 
        } 
    }

    /// <summary>
    /// @brief 現在の処理時間の各単位だけをintで抜き出したもの(上からミリ秒、秒)
    /// </summary>
    public static int Milliseconds {
        get 
        { 
            return stopwatch.Elapsed.Milliseconds; 
        }
    }

    public static int Seconds 
    { 
        get
        { 
            return stopwatch.Elapsed.Seconds; 
        } 
    }

    /// <summary>
    /// @brief 計測開始
    /// </summary>
    public static void Start()
    {
        stopwatch.Start();
    }

    /// <summary>
    /// @brief 計測終了
    /// </summary>
    /// <returns>計測された秒数</returns>
    public static float Stop()
    {
        stopwatch.Stop();
        return TotalMilliseconds;
    }

    /// <summary>
    /// @brief タイマーのリセット
    /// </summary>
    public static void Reset()
    {
        stopwatch.Reset();
    }

    /// <summary>
    /// @brief タイマーのリスタート
    /// </summary>
    public static void ReStart()
    {
        Reset();

        Start();
    }

}