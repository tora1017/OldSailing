/**********************************************************************************************/
/*! @file     ResultButton.cs
*********************************************************************************************
* @brief      リザルトのボタンを制御するクラス
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class ResultButton : BaseObject {

    [SerializeField]
    private Button[] button; // @brief ボタンを登録する配列


    private void Start ()
    {
        // 子のボタンコンポーネントを取得
        button = GetComponentsInChildren<Button>();

        if(Singleton<SaveDataInstance>.Instance.TutorialStatus != eTutorial.eTutorial_End)
        {
            // 配列内のボタン外部機能を停止する
            for (int i = 0; i < button.Length; i++)
                button[i].interactable = false;

            // チュートリアル画面に戻す
            button[1].interactable = true;
        }
    }
}
