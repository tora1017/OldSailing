/**********************************************************************************************/
/*@file       AspectCamera.cs
*********************************************************************************************
* @brief      画面サイズを端末に応じて変更させるクラス
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AspectCamera : BaseObject {

    //比率を 16 / 9 で初期化
    [SerializeField]
    private float aspectX = 16.0f;
    [SerializeField]
    private float aspectY = 9.0f;

    private GameObject cameraObj;
    private Camera camera;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        SetUpAspect();
    }

    private void Start()
    {
        // デリゲートの登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// @brief シーンが読み込まれる時に呼ばれるデリゲート
    /// </summary>
    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        SetUpAspect();
    }
    /// <summary>
    /// @brief アスペクト比の初期化
    /// </summary>
    private void SetUpAspect()
    {
        // カメラを指定
        cameraObj = GameObject.Find("Main Camera");
        camera = cameraObj.GetComponent<Camera>();

        Rect rect = setAspect(aspectX, aspectY);
        camera.rect = rect;

    }
    
    /// <summary>
    /// @brief サイズを取得し調整したサイズを返す
    /// </summary>
    /// <param name="width"  横幅 
    ///        name="height" 縦幅>
    /// </param>
    private Rect setAspect(float width,float height)
    {
        // 目標の比率
        float targetAspect = width / height;
        // 現在の比率
        float curAspect = (float)Screen.width / (float)Screen.height;
        // 縦幅の差分
        float heightAspect = curAspect / targetAspect;
        // 値の初期化
        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        // 縦長の場合
        if (1.0f > heightAspect) {
            rect.x = 0;
            rect.y = (1.0f - heightAspect) / 2.0f;
            rect.width = 1.0f;
            rect.height = heightAspect;
        }
        // 横長の場合
        else
        {
            float widthAspect = 1.0f / heightAspect;
            rect.x = (1.0f - widthAspect) / 2.0f;
            rect.y = 0.0f;
            rect.width = heightAspect;
            rect.height = 1.0f;
        }
        return rect;
    }
}
