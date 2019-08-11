/***********************************************************************/
/*! @file   NextArrowsRenderer.cs
*************************************************************************
*   @brief  次のブイまでのガイドの矢印を描写する
*************************************************************************
*   @author Takumi Adachi
*************************************************************************
*   Copyright © 2019 Takumi Adachi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextArrowsRenderer : MarkerBase {

    private Vector3 startPosition;      //計算の開始位置
    private Vector3 goalPosition;       //計算の目標位置
    private Vector3 yondPosition;       //目標のその先の位置 
    private GameObject playerObject;    //プレイヤーを格納する
    private MarkerBase markerbase;      //MarkerBaseを格納する
    private GameObject gideArrow;       //GideArrowを格納する

    /// <summary>
    /// @breif 初期化
    /// </summary>
    protected override void MarkerInitialize()
    {
        base.MarkerInitialize();

        playerObject = GameObject.Find("Player");
        markerbase = playerObject.GetComponent<MarkerBase>();

        startPosition = playerObject.transform.position;
        goalPosition = lineMarkerList[markerbase.CurrentMarker].gameObject.transform.position;
        yondPosition = lineMarkerList[markerbase.CurrentMarker + 1].gameObject.transform.position;

        this.transform.position = playerObject.transform.position + playerObject.transform.forward * 3.0f + Vector3.up * 0.5f;
        //gideArrow.transform.position = playerObject.transform.position + playerObject.transform.forward * 3.0f + Vector3.up * 0.5f;
    }

    /// <summary>
    /// @brief 更新処理
    /// </summary>
    private void GideArrowUpdate()
    {
        startPosition = playerObject.transform.position;
        goalPosition = lineMarkerList[markerbase.CurrentMarker].gameObject.transform.position;

        this.transform.position = playerObject.transform.position + playerObject.transform.forward * 3.0f + Vector3.up * 0.5f;
        //gideArrow.transform.position = playerObject.transform.position + playerObject.transform.forward * 3.0f + Vector3.up * 0.5f;

        if (lineMarkerList[markerbase.CurrentMarker].gameObject.tag != "goal")
        {
            yondPosition = lineMarkerList[markerbase.CurrentMarker + 1].gameObject.transform.position;
        }
    }
    
    /// <summary>
    /// @brief 角度計算
    /// </summary>
    private void GideArrowDraw()
    {
        Vector2 vec2 = new Vector2(goalPosition.x - startPosition.x,
                    goalPosition.z - startPosition.z);

        float r = Mathf.Atan2(vec2.y, vec2.x);
        float angle = Mathf.Floor(r * 360 / (2 * Mathf.PI));

        this.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);
        //gideArrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);

    }


    /// <summary>
    /// @brief 更新処理まとめ
    /// </summary>
    public override void OnUpdate()
    {
        GideArrowUpdate();
        GideArrowDraw();

        if (Singleton<GameInstance>.Instance.IsGoal)
        {
            Delete(gameObject);
        }
    }

}
