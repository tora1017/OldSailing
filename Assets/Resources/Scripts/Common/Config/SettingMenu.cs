/**********************************************************************************************/
/*@file       SettingMenu.cs
*********************************************************************************************
* @brief      設定メニューの表示非表示
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SettingMenu : BaseObject
{
    [SerializeField] private GameObject settingButton;    // @brief メニューを出すボタン
    [SerializeField] private GameObject settingPopup; // brief settingのインスタンス化
    private PopupSetting setting; // @brief 設定用ポップアップ
    private bool activeMenuFlag;           // @brief メニューの表示非表示のフラグ


    /// <summary>
    /// @brief 変数の初期化
    /// </summary>
    private void Start()
    {
        BaseObjectSingleton<GameInstance>.Instance.IsPorse = false;
        activeMenuFlag = false;
        setting = settingPopup.GetComponent<PopupSetting>();

        // 初回のチュートリアル中は設定ボタンを表示しない
        if(Singleton<SaveDataInstance>.Instance.TutorialStatus == eTutorial.eTutorial_End)
            settingButton.SetActive(!activeMenuFlag);
        else
            settingButton.SetActive(activeMenuFlag);

        if(SceneManager.GetActiveScene().name == "Setting")
		{
			ActiveMenu();
		}
    }

    /// <summary>
    /// @brief booleanを入れ替える
    /// </summary>
    bool ChengeBool(bool arg)
    {
        return !arg;
    }

    /// <summary>
    /// @brief 設定メニューの表示非表示切り替え
    /// </summary>
    public void ActiveMenu()
    {
		if (Singleton<GameInstance>.Instance.IsShipMove || SceneManager.GetActiveScene().name == "Setting")
        {
            activeMenuFlag = ChengeBool(activeMenuFlag);
            settingButton.SetActive(!activeMenuFlag);
            settingPopup.SetActive(activeMenuFlag);
            
            // ポーズフラグの切り替え 設定画面を開いているときはポーズ中 閉じればプレイ中の状態
            BaseObjectSingleton<GameInstance>.Instance.IsPorse = !BaseObjectSingleton<GameInstance>.Instance.IsPorse;

            // ポップアップの開閉
            if (activeMenuFlag)
            {
				Singleton<SoundPlayer>.Instance.PauseBGM();
                setting.Open();
            }
            else
            {
                setting.Close();
                Singleton<SoundPlayer>.Instance.PlayBGM();
            }
        }   
    }
}
