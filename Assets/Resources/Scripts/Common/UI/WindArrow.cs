/**********************************************************************************************/
/*! @file     WindArrow.cs
*********************************************************************************************
* @brief      風に応じて矢印の向きを変える（矢印は風の方向を指す）
*********************************************************************************************
* @note       受け取った風の向きに応じた矢印の向き変更を行います。また、色の変更も。
*             追い風（45 ~ 315)は緑, 向かい風(0 ~ 45 || 315 ~ 360)は赤。
*               
*             参考[回転] https://qiita.com/utah/items/005e34e6888b0b6c63c3
*             　　[色変更] https://qiita.com/lycoris102/items/98bc5a5659e4889dd43f
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindArrow : BaseObject
{
    [SerializeField] private GameObject player;     // @brief 矢印UIの基準となるオブジェクトを代入
    [SerializeField] private GetWindParam getWind;  // @brief GetWindParamがセットされたオブジェクトをアタッチ
    private Image image;        // UIの画像を代入

    void Start()
    {
        image = GetComponent<Image>();
    }

    public override void OnUpdate()
    {
        //// プレイヤーの角度に応じて矢印の向きを変更
        transform.eulerAngles = new Vector3(0f, 0f, player.transform.eulerAngles.y - getWind.ValueWind);

        // UIの角度に応じた矢印の色を変更
        if (45.0f <= transform.eulerAngles.z && transform.eulerAngles.z < 315.0f)
        {
            image.color = new Color(0, 255, 0, 255);    // 向かい風　－＞　緑
        }
        else
        {
            image.color = new Color(255, 0, 0, 255);    // 追い風　　－＞　赤
        }
    }
}
