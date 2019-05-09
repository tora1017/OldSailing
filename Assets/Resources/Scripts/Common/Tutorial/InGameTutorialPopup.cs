/**************************************************************************************/
/*! @file   InTutorialPopup.cs
***************************************************************************************
@brief     Inチュートリアル時のポップアップに関する処理
***************************************************************************************
@author     Yuta Takatsu
***************************************************************************************
* Copyright © 2016 Yuta Takatsu All Rights Reserved.
***************************************************************************************/
using UnityEngine;

public class InGameTutorialPopup : PopupBase {

    private GameObject contens; // @brief ポップアップを格納

    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    /// <param name="content"></param>
    /// <param name="openedCallback"></param>
    public void Open(GameObject content, System.Action openedCallback)
    {
        
        contens = content;

        if (contens != null)

            contens.SetActive(false);
        base.Open(null, null, () =>
          {
              if (contens != null)
                  contens.SetActive(true);
              openedCallback.Invoke();
          });
    }

    /// <summary>
    /// @brief ポップアップを閉じる
    /// </summary>
    /// <param name="closeBegin"></param>
    public void Close(System.Action closeBegin)
    {
        base.Close(closeBegin);
    }
}