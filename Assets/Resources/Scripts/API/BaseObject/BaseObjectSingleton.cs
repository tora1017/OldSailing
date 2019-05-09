/**************************************************************************************/
/*! @file   BaseObjectSingleton.cs
***************************************************************************************
@brief      シングルトン作成用基底クラス
***************************************************************************************
@note       BaseObject用
****************************************************************************************
@author     Ryo Sugiyama
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class BaseObjectSingleton<T> : BaseObject where T : BaseObjectSingleton<T> {
    
    private static T instance;

    /// <summary>
    /// @brief 変数アクセサー
    /// </summary>
    public static T Instance
    {
        get { return instance; }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        if (CheckInstance() == true)
        {
            RemoveObjectToList(this);
            AppendManagerObjectToList(this);
        }
    }


   /// <summary>
   /// @brief インスタンスがあるかどうか確認
   /// </summary>
   /// <returns> true or false </returns>
    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (instance == this)
        {
            return true;
        }

        Object.Destroy(this);
        return false;
    }
}

