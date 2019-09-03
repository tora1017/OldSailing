/**********************************************************************************************/
/*! @file   BaseObjectUpdater.cs
*********************************************************************************************
* @brief      すべてのオブジェクトのアップデートを管理
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;

public class BaseObjectUpdater : BaseObject
{

    #region MonoBehavaiourの実装
    /***************************************************************************************/

    /* @note ポーズ中は OnPorseUpdate() のみ走ります。*/

    /// <summary>
    /// @brief このソリューション唯一のUpdate関数
    /// @note ここ以外でUpdate関数は使わないでください
    /// </summary>
    void Update()
    {
        //Debug.log(Singleton<GameInstance>.instance.IsPorse);

        if (!BaseObjectSingleton<GameInstance>.Instance.IsPorse)
        {
            foreach (var obj in BaseObjectManagerList)
            {
                if (obj.IsPresence())
                    obj.OnFastUpdate();
            }

            foreach (var obj in BaseObjectList)
            {
                if (obj.IsPresence())
                {
                    obj.OnUpdate();
                }
            }
        }
        else
        {
            foreach (var obj in BaseObjectList)
            {
                if (obj.IsPresence())
                    obj.OnPorseUpdate();
            }
        }
    }

    /// <summary>
    /// @brief このソリューション唯一のLateUpdate関数
    /// @note ここ以外でLateUpdate関数は使わないでください
    /// </summary>
    void LateUpdate()
    {

        if (!BaseObjectSingleton<GameInstance>.Instance.IsPorse)
        {
            foreach (var obj in BaseObjectList)
            {
                if (obj.IsPresence())
                    obj.OnLateUpdate();
            }

        }

    }
    /// <summary>
    /// @brief このソリューション唯一のFixedUpdate関数
    /// @note ここ以外でFixedUpdate関数は使わないでください
    /// </summary>
    void FixedUpdate()
    {
        if (!BaseObjectSingleton<GameInstance>.Instance.IsPorse)
        {
            foreach (var obj in BaseObjectList)
            {
                if (obj.IsPresence())
                    obj.OnFixedUpdate();
            }
        }
    }
    #endregion
}
