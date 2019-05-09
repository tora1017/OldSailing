/**************************************************************************************/
/*! @file   PopupBase.cs
***************************************************************************************
@brief      PopupWindowの基底クラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class PopupBase : BaseObject
{

    [SerializeField]
    RectTransform popupWindowBase; // @brief 生成するPopupを格納
    RectTransform popupWindow;     // @brief 生成

    [SerializeField]
    RectTransform popupRoot; // @brief どこに生成するか

    PopupButton popupButton; // @brief Popup内のボタン

    private EButtonSet buttonSet = EButtonSet.Set1;  // ボタンの種類 初期はボタン一つのver
    private System.Action<EButtonId> buttonCallback; // どのボタンを呼ぶかを判断するコールバック

    private Image blackFade; // @brief ポップアップの背景

    /// <summary>
    /// @brief ポップアップ内のボタンのアクセサー
    /// </summary>
    public PopupButton PopupButton
    {
        get { return popupButton; }
    }

    /// <summary>
    /// @brief ポップアップの背景のアクセサー
    /// </summary>
    public Image BlackFade
    {
        get { return blackFade; }
    }

    /// <summary>
    /// @brief Idによってボタンの種類を判断するアクセサー
    /// </summary>
    public System.Action<EButtonId> ButtonCallback
    {
        set
        {
            if (popupButton != null)
                popupButton.OnClickCallback = value;

            buttonCallback = value;
        }
    }

    class PopupAction
    {
        public System.Action begin; // @brief ポップアップを呼び出すとき
        public System.Action run;   // @brief ポップアップを呼び出し中
        public System.Action end;   // @brief ポップアップを閉じるとき
    }

    PopupAction openAction = new PopupAction();
    PopupAction closeAction = new PopupAction();

    /// <summary>
    /// @brief ボタンをセットするアクセサー
    /// </summary>
    public EButtonSet ButtonSet
    {
        private get { return buttonSet; }
        set
        {
            if (popupButton != null)
                popupButton.transform.SetActive(false);

            if (value != EButtonSet.SetNone)
            {
                var root = popupWindow.transform.FindInChildren("Popup", false);
                var buttonGroup = root.transform.FindInChildren("Button", false);
                string path = (value == EButtonSet.Set1) ? "ButtonSet1" : "ButtonSet2";
                var button = buttonGroup.transform.FindInChildren(path, false);

                if (popupButton == null || popupButton.name != button.name)
                {
                    popupButton = button.GetComponent<PopupButton>();

                    popupButton.OnClickCallback = buttonCallback;
                }
            }
            if (popupButton != null)
                popupButton.transform.SetActive(true);

            buttonSet = value;
        }
    }

    float time = float.NaN;
    protected override void OnAwake()
    {
        base.OnAwake();

        if (popupWindow == null)
        {
            popupWindow = New(popupWindowBase) as RectTransform;
        }
        popupWindow.SetParent(popupRoot, false);
        popupWindow.transform.localScale = new Vector3(1, 0);

        if (blackFade == null)
        {
            blackFade = popupWindow.transform.FindInChildren("BackFade", false).GetComponent<Image>();
            blackFade.transform.SetActive(false);
        }
        popupWindow.SetActive(false);

        ButtonSet = EButtonSet.SetNone;
        RemoveObjectToList(this);
    }

    /// <summary>
    /// @brief ポップアップの状態を表すアクセサー
    /// </summary>
    public EPopupState PopupState
    {
        get;
        private set;
    }

    /// <summary>
    /// @brief ポップアップを開く関数
    /// </summary>
    /// <param name="openBeginAction"></param>
    /// <param name="openning"></param>
    /// <param name="openEnd"></param>
    /// <param name="time"></param>
    public virtual void Open(System.Action openBeginAction, System.Action openning = null, System.Action openEnd = null, float time = 0.25f)
    {

        popupWindow.SetActive(true);
        popupWindow.transform.localScale = new Vector3(1, 0);

        openAction.begin = openBeginAction;
        openAction.run = openning;
        openAction.end = openEnd;
        this.time = time;

        OnOpen();
    }

    /// <summary>
    /// @brief ポップアップを閉じる関数
    /// </summary>
    /// <param name="closeBeginAction"></param>
    /// <param name="closening"></param>
    /// <param name="closeEnd"></param>
    /// <param name="time"></param>
    public virtual void Close(System.Action closeBeginAction, System.Action closening = null, System.Action closeEnd = null, float time = 0.25f)
    {
        closeAction.begin = closeBeginAction;
        closeAction.run = closening;
        closeAction.end = closeEnd;
        this.time = time;

        popupWindow.transform.localScale = new Vector3(1,0);
        OnCloseAnimation();
    }

    /// <summary>
    /// @brief ポップアップを開くアニメーション用関数
    /// </summary>
    void OnOpen()
    {
        
        var tweener = popupWindow.DOScale(new Vector3(1,1), time).SetEase(Ease.InOutQuart);
        tweener
        .OnStart(() =>
        {
            blackFade.transform.SetActive(true);
            if (openAction.begin != null)
            {
                openAction.begin.Invoke();
                openAction.begin = null;
            }
            PopupState = EPopupState.OpenBegin;
        })
        .OnUpdate(() =>
        {
            if (openAction.run != null)
            {
                openAction.run.Invoke();
                openAction.run = null;
            }
            PopupState = EPopupState.Openning;
        })
         .OnComplete(() =>
         {

             if (openAction.end != null)
             {
                 openAction.end.Invoke();
                 openAction.end = null;
             }
             PopupState = EPopupState.OpenEnd;
             popupButton.transform.SetActive(true);
         });

    }

    /// <summary>
    /// @brief ポップアップを閉じるアニメーション関数
    /// </summary>
    void OnCloseAnimation()
    {
        var tweener = popupWindow.DOScale(new Vector3(1, 0), time).SetEase(Ease.InOutQuart);
        tweener.OnStart(() =>
        {
            if (closeAction.begin != null)
            {
                closeAction.begin.Invoke();
                closeAction.begin = null;
            }
            popupButton.transform.SetActive(false);
            PopupState = EPopupState.CloseBegin;
        })
         .OnUpdate(() =>
         {
             if (closeAction.run != null)
             {
                 closeAction.run.Invoke();
                 closeAction.run = null;
             }
             PopupState = EPopupState.Closing;
         })
         .OnComplete(() =>
         {

             if (closeAction.end != null)
             {
                 closeAction.end.Invoke();
                 closeAction.end = null;
             }
             blackFade.transform.SetActive(false);
             PopupState = EPopupState.CloseEnd;
         });
    }


    /// <summary>
    /// @brief ポップアップボタンのテキストを指定する関数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="text"></param>
    public void SetButtonText(EButtonId id, string text)
    {
        switch (id)
        {
            case EButtonId.OK:
                if (popupButton.OkText != null)
                {
                    popupButton.OkText.text = text;
                }
                break;

            case EButtonId.Cancel:
                if (popupButton.CancelText != null)
                {
                    popupButton.CancelText.text = text;
                }
                break;
        }
    }
}
