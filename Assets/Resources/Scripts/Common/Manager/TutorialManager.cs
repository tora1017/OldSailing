/**********************************************************************************************/
/*! @file     TutorialManager.cs
*********************************************************************************************
* @brief      チュートリアルの初期状態を設定してます
*********************************************************************************************
* @author     Ryo Sugiyama　& Yuta Takatsu
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseObjectSingleton<TutorialManager>
{

    protected string fileName;
    

    protected override void OnAwake()
    {
        base.OnAwake();

        /// 全プラットフォーム対応
        /// ただしAndroidのみ 4.4以上動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            fileName = Application.temporaryCachePath + ".xml";
        }
        else
        {
            fileName = Application.persistentDataPath + ".xml";
        }

        // チュートリアルの情報を取得
        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile(fileName);

        // 初回起動はチュートリアルモードに突入させる
        if(Singleton<SaveDataInstance>.Instance.TutorialStatus == eTutorial.eTutorial_Null)
        {
            //　チュートリアルの状態をモードセレクトのチュートリアルにして保存する
            //CreateSaveData.NextTutorialState(eTutorial.eTutorial_ModeSelect);
            CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance, fileName);
        }


        /* リリース用　チュートリアルにバグがあるためENDにしてリリースしています。　*/
        CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance, fileName);

    }

    /// <summary>
    /// @brief チュートリアルステートを切り替えるメソッド
    /// </summary>
    public void NextTutorialState(eTutorial TutorialId)
    {

        // チュートリアルが切り替わることを通知
        BaseObjectSingleton<GameInstance>.Instance.IsTutorialState = true;

        // チュートリアル中は、最後に行ったチュートリアルのシーンまで飛びます。
        switch (TutorialId)
        {
            case eTutorial.eTutorial_Null:  // 初回起動

                //　チュートリアルの状態をModeSelectチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);

                break;
                /*
            case eTutorial.eTutorial_ModeSelect: //　モードセレクト画面チュートリアル

                //　チュートリアルの状態をstraightチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Straight);

                break;

            case eTutorial.eTutorial_Straight: //　straight

                //　チュートリアルの状態をcurveチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_Curve);

                break;

            case eTutorial.eTutorial_Curve: //　curve

                //　チュートリアルの状態をEndTextチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_EndText);

                break;

            case eTutorial.eTutorial_EndText: //　最後のテキスト

                //　チュートリアルの状態をEndチュートリアルにして保存する
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);

                break;
*/
            case eTutorial.eTutorial_End: //　チュートリアルがおわり

                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);

                break;

            default:
                CreateSaveData.NextTutorialState(eTutorial.eTutorial_End);
                break;

        }
        // 状態を保存する
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance, fileName);
     
    }
}