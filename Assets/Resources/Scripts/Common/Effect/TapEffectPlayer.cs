/***********************************************************************/
/*! @file   TapEffectPlayer.cs
*************************************************************************
*   @brief  タップ時のエフェクトを再生する
*************************************************************************
*   @author Tsuyoshi Takaguchi
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffectPlayer : BaseObject {

    [SerializeField]
    private Camera camera; // @brief エフェクトを表示するカメラ

    /// <summary>
    /// @brief タップエフェクトの再生
    /// </summary>
    private void PlayTapEffect()
    {
        if (Input.GetMouseButtonDown(0) && !Singleton<GameInstance>.Instance.IsShipMove)
        {
            var pos = camera.ScreenToWorldPoint(Input.mousePosition + camera.transform.forward * 5);
            // エフェクトの再生
            BaseObjectSingleton<EffectManager>.Instance.PlayEffect("Tap", pos, Quaternion.Euler(90 - camera.transform.localScale.x, 0, 0));
        }
    }
   
    public void Update()
    {

        PlayTapEffect();
    }
}