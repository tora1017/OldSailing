/***********************************************************************/
/*! @file   ClearTime.cs
*************************************************************************
*   @brief  クリア時間を表示する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : BaseObject {

    [SerializeField]
    private Sprite[] numSprite; // @brief 画像を格納する配列

    [SerializeField]
    private GameObject clearTimeImage; // @brief 画像を表示するオブジェクトを格納する

    private TimeManager timeManager; // @brief timeManagerObjectのスクリプトを格納する
    private int[] imageNum; // @brief 表示する画像の番号
    private int ii; // @brief 配列を参照するための変数
    private int millTime; // @brief クリア時間を格納する変数
    private int minuteTime; // @brief 分以上のクリア時間を格納する変数

    /// <summary>
    /// @brief 初期化処理
    /// </summary>
    private void ClearTimeInitialize()
    {
        timeManager = GameObject.Find("Timer").GetComponent<TimeManager>();
        imageNum = new int[8];
        millTime = (int)(timeManager.MillTime * 100);
        minuteTime = (int)timeManager.Minute;
        ii = 0;
    }

    /// <summary>
    /// @brief クリア時間の格納
    /// </summary>
    private void SetClearTime()
    {
        // 6桁の秒単位の記録と固定位置に「：」をセット
        for(ii=0;ii<8;ii++)
        {
            if (ii == 2 || ii == 5)
            {
                imageNum[ii] = 10;
                continue;
            }
            // 秒以下のセット用
            if (ii < 6)
            {
                imageNum[ii] = (millTime % 10);
                millTime = (millTime / 10);
            }
            // 分のセット用
            else
            {
                imageNum[ii] = (minuteTime % 10);
                Debug.Log(imageNum[ii]);
                minuteTime = (minuteTime / 10);

            }
        }
    }

    /// <summary>
    /// @brief クリア時間の描画
    /// </summary>
    private void ClearTimeRender()
    {
        for (ii = 0; ii < 8; ii++)
        {
            //複製
            RectTransform timeImage = (RectTransform)New(clearTimeImage).transform;
            timeImage.SetParent(this.transform, false);
            timeImage.localPosition = new Vector2(
                timeImage.localPosition.x - timeImage.sizeDelta.x * ii,
                timeImage.localPosition.y);
            timeImage.GetComponent<Image>().sprite = numSprite[imageNum[ii]];
        }
    }

    private void Start()
    {
        ClearTimeInitialize();
        SetClearTime();
        ClearTimeRender();
    }
}
