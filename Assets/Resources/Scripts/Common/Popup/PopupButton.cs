/**************************************************************************************/
/*! @file   PopupButton.cs
***************************************************************************************
@brief      PopupWindowのボタンを制御するクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PopupButton : BaseObject
{

    /// @brief 外部でも使うためSerializeable
    [System.Serializable]
    class ButtonText
    {
        [SerializeField]
        private Button button; // @brief 指定するボタン

        /// <summary>
        /// @brief ボタンのアクセサー
        /// </summary>
        public Button Button
        {
            get { return button; }
        }

        [SerializeField]
        Text text; // @brief 指定するテキスト

        /// <summary>
        /// @brief テキストのアクセサー
        /// </summary>
        public Text Text
        {
            get { return text; }
        }

        [SerializeField]
        EButtonId id; // @brief ボタンのID

        /// <summary>
        /// @brief ボタンIDのアクセサー
        /// </summary>
        public EButtonId Id
        {
            get { return id; }
        }
    }

    [SerializeField]
    ButtonText ok; // @brief OKテキスト格納用
    [SerializeField]
    ButtonText cancel; // @brief Cancelテキスト格納用

    /// <summary>
    /// @brief ボタンがクリックされたときにイベントを返すアクセサー
    /// </summary>
    public System.Action<EButtonId> OnClickCallback
    {
        private get;
        set;
    }

    public Text OkText
    {
        get { return ok.Text; }
    }

    public Text CancelText
    {
        get { return cancel.Text; }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
    
        this.transform.SetActive(false);

        if (ok.Text != null)
        {
            ok.Text.text = "OK";
        }

        if (cancel.Text != null)
        {
            cancel.Text.text = "Cancel";
        }


        if (ok.Button != null)
        {
            ok.Button.onClick.RemoveAllListeners();
            ok.Button.onClick.AddListener(() =>
            {
                if (OnClickCallback != null)
                {
                    OnClickCallback.Invoke(ok.Id);
                }
            });
        }

        if (cancel.Button != null)
        {
            cancel.Button.onClick.RemoveAllListeners();
            cancel.Button.onClick.AddListener(() =>
            {
                if (OnClickCallback != null)
                {
                    OnClickCallback.Invoke(cancel.Id);
                }
            });
        }
    }
}
