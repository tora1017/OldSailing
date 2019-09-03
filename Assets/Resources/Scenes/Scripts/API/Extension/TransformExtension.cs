/**********************************************************************************************/
/*@file   TransformExtension.cs
***********************************************************************************************
* @brief  Transform型に対しての拡張メソッドをまとめた静的クラス
***********************************************************************************************
* @author     yuta takatsu
***********************************************************************************************
* Copyright © 2017 yuta takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /*********************************************************************************************/
    /// <summary>
    /// @brief    対象のTransformの子供からT型のコンポーネントを取得しリストとして返す
    /// @return   子から取得したT型コンポーネントをすべて入れた配列
    /// </summary>
    public static T[] GetChildrenComponentTo<T>(Transform target)
    {
        int count = target.childCount;
        Transform transform = target;
        T[] children = new T[count];
        for (int i = 0; i < count; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<T>();
        }
        return children;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    アクティブ状態に関係なく子オブジェクトを取得する
    /// @return   すべての子オブジェクト
    /// </summary>
    public static GameObject[] GetChildren(this Transform target)
    {
        int count = target.childCount;
        Transform transform = target;
        GameObject[] children = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
        return children;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    指定の名前の子オブジェクトを取得する
    ///           第三引数をfalseにするとアクティブ､非アクティブ関係なく取得可能
    /// @return   成功 : 子オブジェクト / 失敗 : null
    /// </summary>
    public static GameObject FindInChildren(this Transform target, string serchName, bool isActiveOnly)
    {
        if (isActiveOnly)
        {
            GameObject child = target.Find(serchName).gameObject;
            return child;
        }
        else
        {
            GameObject[] list = target.GetChildren();
            foreach (var child in list)
            {
                if (child.name == serchName)
                {
                    return child;
                }
            }
        }
        return null;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    アクティブ状態を切り替える
    /// @return   none
    /// </summary>
    public static void SetActive(this Transform target, bool isActive)
    {
        target.gameObject.SetActive(isActive);
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    オブジェクトが存在するかの判定
    /// @return   存在する : true / 存在しない : false
    public static bool IsValid(this Transform target)
    {
        return (target != null) ? true : false;
    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    X座標をセットしやすくする 
    /// @return   none
    /// </summary>
    public static void SetPosX(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.x = val;
        me.transform.position = newPos;

    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    Y座標をセットしやすくする
    /// @return   none
    /// </summary>
    public static void SetPosY(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.y = val;
        me.transform.position = newPos;

    }

    /*********************************************************************************************/
    /// <summary>
    /// @brief    Z座標をセットしやすくする
    /// @return   none
    /// </summary>
    public static void SetPosZ(this Transform me, float val)
    {

        var newPos = me.transform.position;
        newPos.z = val;
        me.transform.position = newPos;

    }

}