/**************************************************************************************/
/*! @file   ManagerSceneAutoLoader.cs
***************************************************************************************
@brief      Manager系のオブジェクトを置いておくシーンを実行時に必ずロードさせる
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneAutoLoader
{

    // ゲーム開始時(シーン読み込み前)に実行される 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadManagerScene()
    {
        string managerSceneName = "ManagerScene";

        // ManagerSceneが有効でない時(まだ読み込んでいない時)だけ追加ロードするように 
        if (!SceneManager.GetSceneByName(managerSceneName).IsValid())
        {
            SceneManager.LoadScene(managerSceneName, LoadSceneMode.Additive);
        }
    }
}