/**********************************************************************************************/
/*@file   GyroSensitivity.cs
*********************************************************************************************
* @brief      感度のスライダーを実装するクラス
*********************************************************************************************
* @author     yuta takatsu
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroSensitivity : BaseObject {

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile();

        slider.value = Singleton<SaveDataInstance>.Instance.Sensitivty * 10;
    }

    /// <summary>
    /// @brief 感度が変化した際に呼ぶコールバック
    /// </summary>
    public void GyroSensitivityChange()
    {
        Singleton<SaveDataInstance>.Instance.Sensitivty = slider.value * 0.1f;
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }
}
