/**********************************************************************************************/
/*@file       SetSound.cs
*********************************************************************************************
* @brief      スライダーの値をサウンドミキサーにセットする
*********************************************************************************************
* @author     Shun Tsuchida
*********************************************************************************************
* Copyright © 2018 Shun Tsuchida All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSound : BaseObject
{
	[SerializeField] private AudioMixer audioMixer;	// サウンドミキサー

	/// <summary>
	/// @brief 音量をセット
	/// </summary>
	public void SetMaster(float volume)
	{
		if (volume == -30f) { audioMixer.SetFloat("MasterVol", -80); }
		else { audioMixer.SetFloat("MasterVol", volume); }
	}
}
