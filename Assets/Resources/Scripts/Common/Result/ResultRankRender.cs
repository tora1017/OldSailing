/***********************************************************************/
/*! @file   ResultRankRender.cs
*************************************************************************
*   @brief  順位を表示する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultRankRender : BaseObject {

    [SerializeField]
    private Sprite[] rankSprite; // @brief 使用する画像を格納する配列

    private RankManager rankManagerScript; // @brief rankManagerのスクリプトを格納する変数

    /// <summary>
    /// @brief 初期化
    /// </summary>
    private void ResultRankInitialize()
    {
        rankManagerScript = GameObject.Find("GameInfo").GetComponent<RankManager>();
    }

    /// <summary>
    /// @brief 表示する画像の変更
    /// </summary>
    private void ReslutRankRender()
    {
        // 指定した船の順位を取得し分岐
        // 0はプレイヤーの番号
        switch (rankManagerScript.GetResultRank(0))
        {
            case 1:
                GetComponent<Image>().sprite = rankSprite[0];
                break;

            case 2:
                GetComponent<Image>().sprite = rankSprite[1];
                break;

            case 3:
                GetComponent<Image>().sprite = rankSprite[2];
                break;

            case 4:
                GetComponent<Image>().sprite = rankSprite[3];
                break;
        }
    }

    private void Start () {
        ResultRankInitialize();
        ReslutRankRender();
    }
}
