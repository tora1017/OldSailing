/**************************************************************************************/
/*! @file   UpdateTimeText.cs
***************************************************************************************
@brief      TimeManagerのタイムを取得して、テキストに反映させます
*********************************************************************************************
* @note     2018-06-29 制作
*********************************************************************************************
* @author   Tsuchida Shun
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateTimeText : BaseObject
{
    [SerializeField] private Text text;              // @brief タイマーの時間を反映させたいテキスト
    [SerializeField] private GameObject obj;         // @brief テキストに反映させたい、TimeManagerの入ったオブジェクト
    private TimeManager timeManager;                 // @brief アクセス用
    /// <summary>
    /// @brief objのTimeManagerをアタッチします
    /// </summary>
    private void Start()
    {
        timeManager = obj.GetComponent<TimeManager>();
        text = this.GetComponent<Text>();
        timeManager.OnTimer = false;
    }

    /// <summary>
    /// @brief TimeManagerのMillTimeを取得してテキストに代入します。
    /// </summary>
    public void UpdateText()
    {
        // 常に上位の桁を表示するように参考演算で表示を変更
        text.text = (timeManager.MillTime > 10) ? timeManager.Minute.ToString("00") + "." + timeManager.MillTime.ToString("F2") :
            timeManager.Minute.ToString("00") + "." + "0" + timeManager.MillTime.ToString("F2");


    }

    /// <summary>
    /// @brief UpdateTextを呼びます。
    /// </summary>
    public override void OnUpdate()
    {
        // ここにレースがスタートしているかどうかの判断（カウントダウンが終わってからゴールするまでの間しか計測しないようにする？）
        if (Singleton<GameInstance>.Instance.IsShipMove)
        {
            timeManager.OnTimer = true;

            if (Singleton<GameInstance>.Instance.IsGoal || Singleton<GameInstance>.Instance.IsPorse)
            {
                timeManager.OnTimer = false;
                return;
            }

            UpdateText();
        }
    }

}