/**********************************************************************************************/
/*@file       BaseObject.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    #region リストを操作する場合における、変数・変数アクセサー
    /****************************************************************************************/
    /// <summary>
    /// @brief BaseObject型双方向連結リスト
    /// </summary>
    private static LinkedList<BaseObject> baseObjectLinkedList = new LinkedList<BaseObject>();

    /// <summary>
    /// @brief BaseObject型のマネージャー用双方向連結リスト
    /// </summary>
    private static LinkedList<BaseObject> baseObjectManagerLinkedList = new LinkedList<BaseObject>();

    /// <summary>
    /// @brief 現在のシーンにあるオブジェクトを格納する双方向連結リスト
    /// </summary>
    private static LinkedList<BaseObject> currentSceneObjectList = new LinkedList<BaseObject>();

    /// <summary>
    /// @brief 消去しないオブジェクト用リスト
    /// </summary>
    private static LinkedList<BaseObject> dontDestroyObject = new LinkedList<BaseObject>();

    /// <summary>
    /// @brief  BaseObject型オブジェクトをリストに格納するアクセサー
    /// @set BaseObject型オブジェクトの挿入
    /// @get BaseObject型オブジェクトの取得
    /// </summary>
    public static LinkedList<BaseObject> BaseObjectList
    {
        get { return baseObjectLinkedList; }
        private set { baseObjectLinkedList = value; }
    }

    /// <summary>
    /// @brief  BaseObject型オブジェクトをリストに格納するアクセサー
    /// @set BaseObject型マネージャオブジェクトの挿入
    /// @get BaseObject型マネージャオブジェクトの取得
    /// </summary>
    public static LinkedList<BaseObject> BaseObjectManagerList
    {
        get { return baseObjectManagerLinkedList; }
        private set { baseObjectManagerLinkedList = value; }
    }

    /// <summary>
    /// @brief BaseObject非消去オブジェクトをリストに格納するアクセサー 
    /// </summary>
    public static LinkedList<BaseObject> CurrentSceneObjectList
    {
        get { return currentSceneObjectList; }
        private set { currentSceneObjectList = value; }
    }

    /// <summary>
    /// @brief 消去時実行関数が一度だけ呼ばれることを保証するための変数
    /// </summary>

    private BaseObject owner;

    /// <summary>
    /// @brief 自身を参照する関数
    /// @set BaseObject型オブジェクトの挿入
    /// @get BaseObject型オブジェクトの取得
    /// </summary>
    public BaseObject Owner
    {
        get { return owner; }
        private set { owner = value; }
    }

    #endregion

    #region 派生クラスでの実装が可能なもの
    /*****************************************************************************************/
    /// <summary>
    /// @brief インスタンスが生成されて場に出てくるときに実行される関数
    /// @note  すべてのオブジェクトに対してAwakeが実行される順番は未定義なので、
    ///        Awakeが完了していることを前提とした他オブジェクトのメソッド呼び出しなどはしないでください
    ///        また、Awake()は基本的にオーバーライドせず、AppendListConstructor()を使ってください
    /// </summary>
    public virtual void Awake()
    {
        Owner = this;
        AppendObjectToList(this);
        AppendSceneObjectToList(this);
    }

    /// <summary>
    /// @brief フレーム毎に、マネージャクラスの更新を実行する関数
    /// @note  この関数の呼び出し回数はプロセッサに依存します
    /// </summary>
    public virtual void OnFastUpdate() { return; }

	/// <summary>
	/// @brief フレーム毎に、アニメーションがレンダリングされる前に実行される関数
	/// @note  この関数の呼び出し回数はプロセッサに依存します
	/// </summary>
	public virtual void OnUpdate() { return; }

    /// <summary>
    /// @brief Update()が呼ばれた後に実行されるUpdate関数
    /// @note  オブジェクトのカメラ追従などに使用してください
    /// </summary>
    public virtual void OnLateUpdate() { return; }

    /// <summary>
    /// @brief 物理挙動の更新の直前に固定フレームレートで呼ばれる更新関数
    /// @note  Update()はフレーム毎の呼び出し回数がプロセッサに依存するので、
    ///        物理処理などはここに記述してください
    /// </summary>
    public virtual void OnFixedUpdate() { return; }

    /// <summary>
    /// @brief ポーズ中にのみ実行される更新関数
    /// @note  ポーズ中はこの更新関数以外は実行されないので、
    ///        必要ならばこの関数を実装してください。
    /// </summary>
    public virtual void OnPorseUpdate() { return; }

    /// <summary>
    /// @brief シーンの最後に呼ばれる関数
    /// </summary>
    public virtual void OnEnd() { return; }

    /// <summary>
    /// @brief 管理リストに登録されたときに一度だけ呼ばれるコンストラクタのようなもの
    /// </summary>
    protected virtual void OnAwake() { return; }

    /// <summary>
    /// @brief 管理リストから消去されるときに一度だけ呼ばれるデストラクタのようなもの
    /// </summary>
    protected virtual void OnRemoveList() { return; }

    /// <summary>
    /// Use Debug
    /// </summary>
    public virtual void OnStartTimer() 
    {
        //Timer.Start();
    }

    public virtual void OnEndTimer()
    {
      //  float hoge;
        //hoge = Timer.Stop();
        //if (hoge != 0)
            //Debug.Log(hoge + Owner.ToString());
        //Timer.Reset();
    }


    #endregion

    #region 静的関数群
    /*****************************************************************************************/
    /// <summary>
    /// @brief 指定したオブジェクトを検索する関数
    /// </summary>
    /// <param name="foundObject"></param>
    /// <returns>find or null</returns>
    static public BaseObject FindObjectToList(BaseObject foundObject)
    {
        var obj = baseObjectLinkedList.Find(foundObject);
        return (obj != null) ? obj.Value : null;
    }

    static BaseObject FindManagerObjectToList(BaseObject find)
    {
        var obj = baseObjectManagerLinkedList.Find(find);
        return (obj != null) ? obj.Value : null;
    }

    static BaseObject FindDontDestroyObject(BaseObject find)
    {
        var obj = dontDestroyObject.Find(find);
        return (obj != null) ? obj.Value : null;
    }

    /// <summary>
    /// @brief オブジェクトをリストに登録する関数
    /// </summary>
    /// <param name="value"></param>     
    static public void AppendObjectToList(BaseObject value)
    {
        if (FindObjectToList(value) != null) return;
        baseObjectLinkedList.AddLast(value);
        value.OnAwake();
    }

    static public void AppendManagerObjectToList(BaseObject value)
    {
        if (FindManagerObjectToList(value) != null) return;
        baseObjectManagerLinkedList.AddLast(value);
        value.OnAwake();
    }

    static public void AppendSceneObjectToList(BaseObject value)
    {
        currentSceneObjectList.AddLast(value);
        //Debug.Log(value.name);
    }

    static public void AppendDontDestroyObject(BaseObject value)
    {
        if (FindDontDestroyObject(value) != null) return;

        dontDestroyObject.AddLast(value);
        DontDestroyOnLoad(value.gameObject);
    }

    /// <summary>
    /// @brief オブジェクトをリストから消去する関数
    /// </summary>
    /// <param name="value"></param>
    static public void RemoveObjectToList(BaseObject value)
    {
        if (FindObjectToList(value) != null)
        {
            baseObjectLinkedList.Remove(value);
            value.OnRemoveList();
        }
    }

    /// <summary>
    /// @brief マネージャオブジェクトをリストから消去する関数
    /// </summary>
    /// <param name="value"></param>
    static public void RemoveManagerObjectToList(BaseObject value)
    {
        if (FindManagerObjectToList(value) != null)
        {
            baseObjectManagerLinkedList.Remove(value);
            value.OnRemoveList();
        }
    }

    static public void RemoveDontDestroyObject(BaseObject value)
    {
        if (FindDontDestroyObject(value) != null)
        {
            dontDestroyObject.Remove(value);
            Delete(value);
        }

    }

    public void RemoveSceneObjectAll()
    {
        foreach (var obj in currentSceneObjectList)
        {
            dontDestroyObject.Remove(obj);
        }
    }

    public void RemoveDontDestroyObjectAll()
    {
        foreach (var obj in dontDestroyObject)
        {
            dontDestroyObject.Remove(obj);
            Delete(obj);
        }
    }

    /// <summary>
    /// @brief オブジェクト生成用関数
    /// @note Instantiate()は使わずこちらを使用してください
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="createObj"></param>
    /// <returns></returns>
    static public T New<T>(T createObj) where T : UnityEngine.Object
    {
        if (createObj == null) return null;
        T returnObj = Instantiate(createObj) as T;
        return returnObj;
    }

    /// <summary>
    /// @brief オブジェクト消去用　消去に若干のディレイがある
    /// @note  Destroy()は使わずこちらを使ってください
    /// </summary>
    /// <param name="delete"></param>
    static public void Delete(BaseObject delete)
    {
        if (delete == null) return;

        if (FindObjectToList(delete) != null)
            RemoveObjectToList(delete);
        Destroy(delete.gameObject);
        return;
    }

    static public void Delete<T>(T delete) where T : UnityEngine.Object
    {
        if (delete == null) return;
        Destroy(delete);
        return;
    }

    /// <summary>
    /// @brief オブジェクト即時消去用　
    /// @note  消去に即時性が求められる場合はこちらを使ってください
    /// </summary>
    /// <param name="delete"></param>
    static public void DeteleImmediate(BaseObject delete)
    {
        if (delete == null) return;
        if (FindObjectToList(delete) != null)
            RemoveObjectToList(delete);
        DestroyImmediate(delete.gameObject);
        return;
    }

    static public void DeteleImmediate<T>(T delete) where T : UnityEngine.Object
    {
        if (delete == null) return;

        DestroyImmediate(delete);
    }
    #endregion
}
