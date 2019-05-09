/**********************************************************************************************/
/*! @file     CreateSaveData.cs
*********************************************************************************************
* @brief      オブジェクトのデータをバイナリファイルにロード・セーブをする
*********************************************************************************************
* @note       あとから拡張しやすいように改良します
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CreateSaveData
{
    #region チュートリアルの状態を管理する関数群
    /**********************************************************************************************/
    /// <summary>
    /// @brief 指定されたチュートリアルを実行するか判断する関数
    /// </summary>
    /// <param name="path"></param>
    /// <returns>true 実行可 : false 実行不可</returns>
    public static bool DoTutorial(string path, eTutorial state)
    {
        Singleton<SaveDataInstance>.Instance = (SaveDataInstance)LoadFromBinaryFile(path);
        if (Singleton<SaveDataInstance>.Instance.TutorialStatus == state)
        {
            Debug.Log(Singleton<SaveDataInstance>.Instance.TutorialStatus);
            return true;
        }
        return false;
    }

    /// <summary>
    /// @brief チュートリアルの情報を更新する
    /// </summary>
    /// <param name="state"></param>
    public static void NextTutorialState(eTutorial state)
    {
        Singleton<SaveDataInstance>.Instance.TutorialStatus = state;
    }
    #endregion

    #region バイナリファイルへのアクセサー
    /**********************************************************************************************/
    /// <summary>
    /// @brief オブジェクトの内容をファイルから読み込み復元する
    /// </summary>
    /// <param name="path">読み込むファイル名</param>
    /// <returns>復元されたオブジェクト</returns>
    public static object LoadFromBinaryFile(string path)
    {
        // 指定したファイルがあるかどうか調べる
        if (!System.IO.File.Exists(path))
        {
            // なければ生成
           CreateBineryFile(path);
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryFormatter bf = new BinaryFormatter();

        //読み込んで逆シリアル化する
        object obj = bf.Deserialize(fs);
        fs.Close();

        return obj;
    }

    public static object LoadFromBinaryFile()
    {

        string path;

        /// 全プラットフォーム対応
        /// ただしAndroidのみ 4.4以上動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.temporaryCachePath + ".xml";
        }
        else
        {
            path = Application.persistentDataPath + ".xml";
        }


        // 指定したファイルがあるかどうか調べる
        if (!System.IO.File.Exists(path))
        {
            // なければ生成
            CreateBineryFile(path);
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        BinaryFormatter bf = new BinaryFormatter();

        //読み込んで逆シリアル化する
        object obj = bf.Deserialize(fs);
        fs.Close();

        return obj;
    }

    /// <summary>
    /// @brief オブジェクトの内容をファイルに保存する
    /// </summary>
    /// <param name="obj">保存するオブジェクト</param>
    /// <param name="path">保存先のファイル名</param>
    public static void SaveToBinaryFile(object obj, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        BinaryFormatter bf = new BinaryFormatter();

        //シリアル化して書き込む
        bf.Serialize(fs, obj);
        fs.Close();
    }

    public static void SaveToBinaryFile(object obj)
    {

        string path;

        /// 全プラットフォーム対応
        /// ただしAndroidのみ 4.4以上動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.temporaryCachePath + ".xml";
        }
        else
        {
            path = Application.persistentDataPath + ".xml";
        }

        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        BinaryFormatter bf = new BinaryFormatter();

        //シリアル化して書き込む
        bf.Serialize(fs, obj);
        fs.Close();
    }


    /// <summary>
    /// @brief ファイルの生成
    /// </summary>
    /// <param name="path">Path.</param>
    public static void CreateBineryFile(string path)
    {
        //　ファイルの生成
        FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        NextTutorialState(eTutorial.eTutorial_End);
        Singleton<SaveDataInstance>.Instance.MaxBGMVolume = 0.7f;
        Singleton<SaveDataInstance>.Instance.MaxSEVolume = 1.0f;
        Singleton<SaveDataInstance>.Instance.IsGyro = true;
        Singleton<SaveDataInstance>.Instance.ISSwipe = true;
        Singleton<SaveDataInstance>.Instance.Sensitivty = 0.5f;
        fs.Close();

        SaveToBinaryFile(Singleton<SaveDataInstance>.Instance, path);
    }
    #endregion
}

