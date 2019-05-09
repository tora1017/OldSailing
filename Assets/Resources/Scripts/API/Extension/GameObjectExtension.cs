/**********************************************************************************************/
/*@file       GameObjectExtension.cs
*********************************************************************************************
* @brief      GameObjectの拡張メソッド
*********************************************************************************************
* @author     Yuta Takatsu & Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu & Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    /// <summary>
    /// @brief エラー処理を含めたFind関数
    /// </summary>
    /// <returns>Hierarchu上のゲームオブジェクト</returns>
    public static GameObject Find(string obj)
	{
		GameObject gameObject = GameObject.Find(obj);
        if(gameObject == null)
		{
			Debug.LogError("<color=red>" + obj + "が見つかりません。名前が間違っているか、Hierarchyに存在しません。</color>");
			return null;
		}

		return gameObject;
	}

    /// <summary> 
    /// @brief 親や子オブジェクトも含めた範囲から指定のコンポーネントを取得する 
    /// </summary> 
    public static T GetComponentInParentAndChildren<T>(this GameObject gameObject) where T : UnityEngine.Component
    {


        if (gameObject.GetComponentInParent<T>() != null)
        {
            return gameObject.GetComponentInParent<T>();
        }
        if (gameObject.GetComponentInChildren<T>() != null)
        {
            return gameObject.GetComponentInChildren<T>();
        }



        return gameObject.GetComponent<T>();
    }

    /// <summary> 
    /// @brief 親や子オブジェクトも含めた範囲から指定のコンポーネントを全て取得する 
    /// </summary> 
    public static List<T> GetComponentsInParentAndChildren<T>(this GameObject gameObject) where T : UnityEngine.Component
    {
        List<T> _list = new List<T>(gameObject.GetComponents<T>());



        _list.AddRange(new List<T>(gameObject.GetComponentsInChildren<T>()));
        _list.AddRange(new List<T>(gameObject.GetComponentsInParent<T>()));



        return _list;
    }

    /// <summary> 
    ///  @brief GameObjectをすべて取得する 
    /// </summary> 
    public static List<GameObject> GetAll(this GameObject obj)
    {
        List<GameObject> allChildren = new List<GameObject>();
        GetChildren(obj, ref allChildren);
        return allChildren;
    }

    /// <summary>
    /// @brief 親オブジェクトのみを取得する
    /// </summary>>
    public static  List<GameObject> GetParent(this GameObject obj)
    {
        List<GameObject> allParent = new List<GameObject>();
        GetParent(obj, ref allParent);
        return allParent;
    }

       
    /// <summary>
    /// @brief 親要素を取得してリストに追加
    /// </summary>>
    public static void GetParent(GameObject obj, ref List<GameObject> allParent)
    {
        Transform parent = obj.GetComponentInParent<Transform>();
        
        foreach(Transform ob in parent)
        {
            // 親要素がいなければ終了
            if (ob.gameObject == null)
            {
                return;
            }

            if (ob.gameObject.name != "GameObject")
            {
                allParent.Add(ob.gameObject);
            }
            GetParent(ob.gameObject, ref allParent);
        }
    }

    /// <summary>
    /// @brief 子要素を取得してリストに追加
    public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        // 子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
            GetChildren(ob.gameObject, ref allChildren);
        }
    }

	/// <summary>
	/// 指定した名前の子オブジェクトを全て取得する。
	/// </summary>
	/// <param name="obj">Object.</param>
	public static void GetChildren(GameObject obj, string objName)
	{
		List<GameObject> gameObjects = new List<GameObject>();
		Transform children = obj.GetComponentInChildren<Transform>();

		if (children.childCount == 0) return;

		foreach (Transform childObj in children)
		{
			if(childObj.name == objName)
			{
				gameObjects.Add(obj.gameObject);
				GetChildren(childObj.gameObject, ref gameObjects);
			}
		}
	}
    
    /// <summary>
    /// @brief 指定した名前のオブジェクトを取得する
    /// </summary>
    /// <param name="obj">Object.</param>
    /// <param name="allGameObject">All game object.</param>
	/// <param name="str">デフォルト引数：指定しない場合「GameObject」固定</param>
    public static void GetGameObject(GameObject obj, ref List<GameObject> allGameObject, string str)
    {
        Transform gameObject = obj.GetComponentInParent<Transform>();
        
        foreach (Transform ob in gameObject)
        {
            
            // 親要素がいなければ終了
            if (ob.gameObject == null)
            {
                return;
            }

            if (ob.gameObject.name == str)
            {
                allGameObject.Add(ob.gameObject);
            }
            GetGameObject(ob.gameObject, ref allGameObject, str);
        }
    }
    
	/// <summary>
    /// @brief GameObjectという名前のみリストに格納する
    /// </summary>>
	public static List<GameObject> GetGameObject(GameObject obj, string str = "GameObject")
	{
		List<GameObject> allGameObject = new List<GameObject>();
        GetGameObject(obj, ref allGameObject, str);
        return allGameObject;
    }

    /// <summary>
    /// @brief 子要素の総数を返す関数
    /// </summary>
	/// <value>子要素の総数</value>
	public static int ObjectNum
    {
		get { return ObjectNum; }
	}
}