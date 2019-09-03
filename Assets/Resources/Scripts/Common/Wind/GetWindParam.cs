/**********************************************************************************************/
/*@file       GetWindParam.cs
*********************************************************************************************
* @brief      風のベクトルを制御するクラス
*********************************************************************************************
* @author     Reina Sawai and Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Reina Sawai All Rights Reserved.
**********************************************************************************************/
using System;
using UnityEngine;
using System.Collections;

public class GetWindParam : BaseObject
{
	private float valueWind;    // @brief 風の方向
    
    private float windForce = 0; // @brief 風の強さ


	protected override void OnAwake()
	{
		base.OnAwake();
		valueWind = 0;
	}

	/// <summary>
	/// @brief 風の方向を計算するアクセサー
	/// </summary>
	public float WindForce
    {
        set { windForce = value; }
        get { return windForce; }

    }

    /// <summary>
    /// @brief 風向きを出すアクセサー
    /// </summary>
    public float ValueWind
    {
        get { return valueWind; }
        set
        {
            // 風向きを0~360の中に指定する
            if (value > 180)
            {
                valueWind = value - 180;
            }
            else if (value < -180)
            {
                valueWind = 180 - value;
            }
            else
            {
                valueWind = value;
            }

        }
    }
}
