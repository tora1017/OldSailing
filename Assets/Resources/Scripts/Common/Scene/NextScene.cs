/***********************************************************************/
/*! @file   NextScene.cs
*************************************************************************
*   @brief  次のシーンを登録するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneManagement;

public class NextScene : BaseObject
{

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    [SerializeField]
    private SCENES nextScene; // @brief 次のシーン格納用

    /// <summary>
    /// @brief 次のシーンを判断するアクセサー
    /// </summary>
    public SCENES AccessNextScene { set; get; }

    /// <summary>
    /// @brief タップされたときに呼ばれる関数
    /// </summary>
    public void OnTap()
    {

        //シーン破棄時に実行される関数をオブジェクト分だけ実行
        foreach (var obj in CurrentSceneObjectList)
        {
            base.OnEnd();
            obj.OnEnd();
        }
        BaseObjectSingleton<GameInstance>.Instance.IsPopup = false; // ポップアップを閉じる
        BaseObjectSingleton<GameInstance>.Instance.IsPorse = false; // ポーズ中であれば解除
        Debug.Log(Singleton<SaveDataInstance>.Instance.TutorialStatus);

        SceneManager.SceneMove(nextScene); // SceneManagerを呼び出す 引数は次のシーン
    }

    /// <summary>
    /// @brief 現在の状態を通知する
    /// </summary>
    public void DispatchTutorialState()
    {
        TutorialManager.Instance.NextTutorialState(Singleton<SaveDataInstance>.Instance.TutorialStatus);
    }
}