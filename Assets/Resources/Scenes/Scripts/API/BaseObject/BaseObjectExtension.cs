/**********************************************************************************************/
/*! @file   BaseObjectExtension.cs
*********************************************************************************************
* @brief      BaseObjectクラスの拡張メソッドを記述したクラス
*********************************************************************************************
* @note       拡張メソッドは非ジェネリック静的クラスで定義する必要があるため、別クラスになってます 
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using System.Collections;

public static class BaseObjectExtension
{
    /// <summary>
    /// @brief BaseObject型オブジェクトが存在するか調べる関数
    /// </summary>
    /// <param name="targetObj"></param>
    /// <returns> true or false </returns>
    public static bool IsPresence(this BaseObject targetObj)
    {
        return (targetObj != null) ? true : false;
    }
}
