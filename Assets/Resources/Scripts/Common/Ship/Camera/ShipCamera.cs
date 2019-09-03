/***********************************************************************/
/*! @file   ShipCamera.cs
*************************************************************************
*   @brief  船用のカメラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipCamera : BaseObject
{
   
    [SerializeField]
    private GameObject ship; // @brief 追跡する船
    [SerializeField]
    private Camera shipCamera;      // @brief 対象のカメラ
    
    private int layerMaskShip;  // @brief 船のレイヤー
    private float distance;     // @brief 船とカメラの距離
    private float cameraHeight; // @brief カメラの高さ
    private float followSpeed = 20; // @brief カメラのディレイスピード
    private Vector3 cameraOffset = Vector3.zero; // @brief カメラの位置の初期化


    // int型のtemplate
    Selectable<int> selectedValue = new Selectable<int>();


    void Start()
    {
        Singleton<ShipStates>.Instance.CameraMode = eCameraMode.TPS;

        layerMaskShip = 1 << LayerMask.NameToLayer("Ship"); // レイヤー情報を取得

        ChangeCameraAngle(Singleton<ShipStates>.Instance.CameraMode);
        //平面(X,Z)での距離を取得
        distance = Vector3.Distance(
            new Vector3(ship.transform.position.x, 0, ship.transform.position.z),
            new Vector3(shipCamera.transform.position.x, 0, shipCamera.transform.position.z));

        //カメラの高さの差分を取得
        cameraHeight = shipCamera.transform.position.y - ship.transform.position.y;

        // 値が変更されたときに呼び出されるコールバック関数を登録
        selectedValue.changed += selectedValue => ChangeCameraAngle(Singleton<ShipStates>.Instance.CameraMode);
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();


        // 値を変更　値が変わればコールバックが呼ばれる
        selectedValue.SetValueIfNotEqual((int)Singleton<ShipStates>.Instance.CameraMode);

        //カメラの角度を調整
        var newRotation = Quaternion.LookRotation(ship.transform.position - shipCamera.transform.position).eulerAngles;

        // ゴール用カメラ ディレイ無し
        if (Singleton<ShipStates>.Instance.CameraMode == eCameraMode.GOAL)
        {
            Vector3 goalPosition = transform.position;
            goalPosition.x = ship.transform.position.x + cameraOffset.x;
            goalPosition.y = ship.transform.position.y + cameraOffset.y + 3.5f;
            goalPosition.z = ship.transform.position.z + cameraOffset.z + 10f;
            transform.position = goalPosition;

        }

        //カメラの位置を高さだけ、ターゲットに合わせて作成
        var current = new Vector3(
            shipCamera.transform.position.x,
            ship.transform.position.y,
            shipCamera.transform.position.z
        );

        //チェック用の位置情報を作成(バックした時にカメラが引けるようにdistance分位置を後ろにずらす)
        var checkCurrent = current + Vector3.Normalize(current - ship.transform.position) * distance;

        //カメラが到達すべきポイントを計算（もともとのターゲットとの差分から計算します）
        var v = Vector3.MoveTowards(
            ship.transform.position,
            checkCurrent,
            distance);

        //カメラ位置移動(位置計算後にカメラの高さを修正）
        shipCamera.transform.position = Vector3.Lerp(
            current,
            v,
            Time.deltaTime * followSpeed
        ) + new Vector3(0, cameraHeight, 0);

        if (Singleton<ShipStates>.Instance.CameraMode == eCameraMode.TPS)
        {
            newRotation.x = 20;
        }
        newRotation.z = 0;
        shipCamera.transform.rotation = Quaternion.Slerp(shipCamera.transform.rotation, Quaternion.Euler(newRotation), 1);

    
    }
    /// <summary>
    ///  @brief 視点の変更時に呼ぶメソッド
    ///</summary>
    /// <param name="cameraMode">変更したい視点</param>
    public void ChangeCameraAngle(eCameraMode cameraMode)
    {

        if (Singleton<ShipStates>.Instance.CameraMode == eCameraMode.TPS)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosY(7);
            shipCamera.transform.SetPosZ(69);
        }
        if (Singleton<ShipStates>.Instance.CameraMode == eCameraMode.GOAL)
        {
            Camera.main.cullingMask |= layerMaskShip; // 表示
            shipCamera.transform.SetPosX(-3);
            shipCamera.transform.SetPosY(2);
            shipCamera.transform.SetPosZ(-5);
            shipCamera.transform.LookAt(ship.transform);

            // ゴール用カメラだけに使う固定用position
            cameraOffset = transform.position - ship.transform.position;
        }
    }
}