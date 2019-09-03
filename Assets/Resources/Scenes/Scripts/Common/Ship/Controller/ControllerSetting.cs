/**************************************************************************************/
/*! @file   ControllerSetting.cs
***************************************************************************************
@brief      設定画面のコントローラー関係の設定を行うクラス
***************************************************************************************
@author     yuta takatsu
***************************************************************************************
* Copyright © 2018 yuta takatsu All Rights Reserved.
***************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerSetting : BaseObject
{


	private Toggle toggle;

	private void Start()
	{
		
		toggle = this.GetComponent<Toggle>();

		Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile();
		if (this.gameObject.name == "Gyro")
		{
			toggle.isOn = Singleton<SaveDataInstance>.Instance.IsGyro;
		}
		else
		{
			toggle.isOn = Singleton<SaveDataInstance>.Instance.ISSwipe;
		}
	}

    /// <summary>
    /// @brief ジャイロのフラグを切り替える
    /// </summary>
    public void GyroChanged()
    {
        Singleton<SaveDataInstance>.Instance.IsGyro = !Singleton<SaveDataInstance>.Instance.IsGyro;
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);

    }

    /// <summary>
    /// @brief スワイプのフラグを切り替える
    /// </summary>
    public void SwipeChanged()
    {
        Singleton<SaveDataInstance>.Instance.ISSwipe = !Singleton<SaveDataInstance>.Instance.ISSwipe;
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }
}
