/***********************************************************************/
/*! @file   FadeManager.cs
*************************************************************************
*   @brief  フェードの制御をするマネージャークラス
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FadeManager : BaseObjectSingleton<FadeManager>
{

    [SerializeField]
    private Image fade; // @brief 黒い画像

    private Color fadeColor = Color.black; // @brief フェードの色を指定

    private const float FADETIME = 1f; // @brief フェードにかける時間を定義


    public void Start()
    {
        fade.raycastTarget = false;
    }

    /// <summary>
    /// @brief フェードをさせるコルーチン呼び出し
    /// </summary>
    public void Load(int scene)
    {
        FadeManager.Instance.StartCoroutine(SceneLoad(scene));
    }

    /// <summary>
    /// @brief フェードを行うコルーチン
    /// </summary>
    IEnumerator SceneLoad(int scene)
    {
        fade.raycastTarget = true;
        fade.color = new Color(0, 0, 0, 0);

        // フェードイン
        while (fade.color.a < 1)
        {
            fade.color += new Color(0, 0, 0, 1f * (FADETIME * Time.deltaTime));
            yield return null;
        }
        fade.color = new Color(0, 0, 0, 1);

        yield return null;

        SceneManager.LoadScene(scene);

        // フェードアウト
        while (fade.color.a > 0)
        {
            fade.color -= new Color(0, 0, 0, 1f * (FADETIME * Time.deltaTime));
            yield return null;
        }

        fade.color = new Color(0, 0, 0, 0);
        fade.raycastTarget = false;
    }
}