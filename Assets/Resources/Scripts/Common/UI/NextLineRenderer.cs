/***********************************************************************/
/*! @file   NextLineRenderer.cs
*************************************************************************
*   @brief  次のブイまでの線を描画する
*************************************************************************
*   @author Tsuyoshi Takaguchi
*************************************************************************
*   Copyright © 2018 Tsuyoshi Takaguchi All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLineRenderer : MarkerBase {

    private Vector3 startPosition;     // @brief 線の開始位置
    private Vector3 goalPosition;      // @brief 線の目標位置
    private Vector3 yondPosition;      // @brief 目標のその先の位置
    private GameObject playerObject;   // @brief プレイヤーを格納する
    private MarkerBase markerBase;     // @brief MarkerBaseを格納する
    private LineRenderer lineRenderer; // @brief LineRendererを格納する
    [SerializeField]
    private Material[] material;       // @brief Materialを格納する
    [SerializeField]
    private int rendererLine;          // @brief 描画するLineを指定

    /// <summary>
    /// @brief 初期化
    /// </summary>
    protected override void MarkerInitialize()
    {
        base.MarkerInitialize();



        playerObject = GameObject.Find("Player");
        markerBase = playerObject.GetComponent<MarkerBase>();

        lineRenderer = GetComponent<LineRenderer>();

        startPosition = playerObject.transform.position;
        goalPosition = hitMarkerList[markerBase.CurrentMarker].gameObject.transform.position;
        yondPosition = hitMarkerList[markerBase.CurrentMarker + 1].gameObject.transform.position;
    }
    /// <summary>
    ///  @brief 更新処理
    /// </summary>
    private void StraightLineUpdate()
    {
        startPosition = playerObject.transform.position;
        goalPosition = hitMarkerList[markerBase.CurrentMarker].gameObject.transform.position;

        if(hitMarkerList[markerBase.CurrentMarker].gameObject.tag != "goal")
        {
            yondPosition = hitMarkerList[markerBase.CurrentMarker + 1].gameObject.transform.position;
        }
    }

    /// <summary>
    /// @brief 線の描画
    /// </summary>
    private void StraightLineRenderer(int _rendererLine)
    {
        lineRenderer.SetVertexCount(2);


        if (_rendererLine == 0 && Singleton<GameInstance>.Instance.IsShipMove)
        {
            lineRenderer.material = material[0];
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, goalPosition);

        }
        else if (_rendererLine == 1 && Singleton<GameInstance>.Instance.IsShipMove)
        {
            lineRenderer.material = material[1];
            lineRenderer.SetPosition(0, goalPosition);
            lineRenderer.SetPosition(1, yondPosition);
        }
    }

    public override void OnUpdate()
    {
        StraightLineUpdate();
        StraightLineRenderer(rendererLine);
        
        if(Singleton<GameInstance>.Instance.IsGoal)
        {
            Delete(gameObject);
        }
    }
}
