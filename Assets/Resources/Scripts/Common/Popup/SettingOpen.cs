/**************************************************************************************/
/*! @file   SettingPopupOpen.cs
***************************************************************************************
@brief      リザルト画面を表示させるクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : PopupBase {

    [SerializeField]
    private GameObject contents; // @brief ポップアップ画面を格納

    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        base.Open(null, null, SettingOpen);
    }
    /// <summary>
    /// @brief ポップアップを閉じる
    /// </summary>
    public void Close()
    {
        base.Close(null, null, SettingClose);
    }

    /// <summary>
    /// @brief 設定画面のポップアップを表示させる
    /// </summary>
    private void SettingOpen()
    {
        contents.SetActive(true);
    }

    /// <summary>
    /// @brief 設定画面のポップアップを非表示にさせる
    /// </summary>
    private void SettingClose()
    {
        contents.SetActive(false);
    }
}
