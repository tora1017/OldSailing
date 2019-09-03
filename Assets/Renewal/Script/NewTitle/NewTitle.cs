/***********************************************************************/
/*! @file   NewTitle.cs
*************************************************************************
*   @brief  タイトルのアニメーションを制御するスクリプト
*************************************************************************
*   @author Souta Nakayama
*************************************************************************
*   Copyright © 2019 Souta Nakayama All Rights Reserved.
************************************************************************/
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class NewTitle : MonoBehaviour {

    private GameObject textObject; //点滅させたい文字
    private float nextTime;
    [SerializeField] public float interval = 1.0f; //点滅周期

    // Use this for initialization
    void Start()
    {
        textObject = GameObject.Find("画面をタッチしてください");
        nextTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        // テキストの点滅
        //一定時間ごとに点滅
        if (Time.time > nextTime)
        {
            float alpha = textObject.GetComponent<CanvasRenderer>().GetAlpha();
            if (alpha == 1.0f)
                textObject.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            else
                textObject.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            nextTime += interval;
        }

        // 画面遷移
        if (Input.GetMouseButtonUp(0))
        {
            SceneManager.LoadScene("NewMain");  // NewMainシーンに遷移
        }

    }
}
