using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupInGame : PopupBase
{

    [SerializeField]
    private GameObject contens; // 表示するイベントを格納

    public void Start()
    {
        Open();
    }

    /// <summary>
    /// @brief ポップアップが開き終わったらイベントを表示
    /// </summary>
    public void OpenEnd()
    {
        contens.SetActive(true);
    }

    /// <summary>
    /// @brief ポップアップが閉じたらイベントを非表示
    /// </summary>
    public void CloseEnd()
    {
        contens.SetActive(false);
    }

    /// <summary>
    /// @brief ポップアップ中のイベントを管理
    /// </summary>
    /// <param name="id"></param>
    private void PopupAction(EButtonId id)
    {
        Close();
    }

    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        ButtonSet = EButtonSet.Set1;
        PopupButton.OnClickCallback = PopupAction;

        base.Open(null, null, OpenEnd);
    }
    /// <summary>
    /// @brief ポップアップを閉じる
    /// </summary>
    public void Close()
    {
        base.Close(CloseEnd, null, null);
    }

}