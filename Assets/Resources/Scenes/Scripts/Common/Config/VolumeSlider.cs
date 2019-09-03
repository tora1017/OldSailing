/**********************************************************************************************/
/*@file   VolumeSlider.cs
*********************************************************************************************
* @brief      音量調整のスライダーを実装するクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : BaseObject {

    private Slider slider;

	void Start()
    {
        slider = GetComponent<Slider>();

        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)CreateSaveData.LoadFromBinaryFile();

        if(gameObject.name == "SESlider")
		{
			slider.value = Singleton<SaveDataInstance>.Instance.MaxSEVolume * 10;
		}
		else
		{
			slider.value = Singleton<SaveDataInstance>.Instance.MaxBGMVolume * 10;
		}
	}

    /// <summary>
    /// @brief BGM値が変化した際に呼ばれる関数
    /// </summary>
    public void BGMValueChanged()
    {
        Singleton<SaveDataInstance>.Instance.MaxBGMVolume = slider.value * 0.1f;
        Singleton<SoundPlayer>.Instance.PauseBGM();        
        Singleton<SoundPlayer>.Instance.PlayBGM();
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }
    /// <summary>
    /// @brief SE値が変化した際に呼ばれる関数
    /// </summary>
    public void SEValueChanged()
    {
        Singleton<SaveDataInstance>.Instance.MaxSEVolume = slider.value * 0.1f;
        Singleton<SoundPlayer>.Instance.PlaySE("PassedMarker");
        CreateSaveData.SaveToBinaryFile(Singleton<SaveDataInstance>.Instance);
    }


}
