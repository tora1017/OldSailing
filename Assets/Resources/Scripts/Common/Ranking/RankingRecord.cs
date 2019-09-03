/**********************************************************************************************/
/*@file       RankingRecord.cs
*********************************************************************************************
* @brief      ランキングデータの出力と読込を行う
*********************************************************************************************
* @author     Yuta Nagashima
*********************************************************************************************
* Copyright © 2018 Yuta Nagashima All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class RankingRecord : BaseObject
{

    //@brief ファイルに出力するための構造体
    public struct RankingData 
    {
        public int rankData;    //@brief 順位
        public float timeData;  //@brief タイム

        /// <summary>
        /// @brief 受け取った順位とタイムを変数に格納する
        /// </summary>
        /// <param name="rank">順位</param>
        /// <param name="time">タイム</param>
        public RankingData(int rank, float time) 
        {
            rankData = rank;
            timeData = time;
        }
    }

    [SerializeField]
    private List<Text> objectRankText;
    [SerializeField]
    private List<Text> objectTimeText;

    private const string FILE_DIRECTORY = "Assets/Resources/Data/Ranking";   //@brief 読み取るファイルのフォルダ名
    private const string FILE_NAME = "RankingData";                          //@brief 読み取るファイル名
    private const string FILE_EXTENTION = "csv";                             //@brief ファイルの拡張子
    private const string CHARA_CODE = "UTF-8";                               //@brief ファイルの文字コード
    private const int OUTPUT_RECODE_NUM = 4;                                 //@brief ファイルに出力する記録数

    private Sort rankingSort;                                                //@brief ソートを行うための変数
    private List<RankingData> rankingData;                                   //@brief ランキングのリスト
    private string inputFileName;                                            //@brief 読み込むファイルの階層と名前


    #region ソートを行うクラス
    public class Sort
    {

        /// <summary>
        /// @brief リストを受け取り挿入ソートを行う
        /// </summary>
        /// <param name="list">要素を持つリスト</param>
        public void InsertSort(List<float> list) 
        {
            
            for(int i = 1; i < list.Count; i++) 
            {

                float temp = list[i];

                if(list[i - 1] > temp) 
                {

                    int j = i;

                    do 
                    {
                        list[j] = list[j - 1];
                        j--;
                    } while(j > 0 && list[j - 1] > temp);

                    list[j] = temp;

                }

            }

        }

    }
    #endregion

    private void Start()
    {
        rankingSort = new Sort();
        rankingData = new List<RankingData>();
        inputFileName = FILE_DIRECTORY  + "/" + FILE_NAME + "." + FILE_EXTENTION;

        //テスト
        //RankingCreate(testRecodeList);
        //ここまで

        //データを書き込んでおく
        InRecord();
        TextWrite();

    }

    /// <summary>
    /// @brief 受けとった記録と読み込んだ記録を合わせランキングデータを作成
    /// </summary>
    /// <param name="recode">タイムを格納したリスト</param>
    public void RankingCreate(List<float> recode) 
    {

        //記録を読み込む
        InRecord();

        List<float> tempRecodeList = new List<float>();
        //受け取った記録と読み込んだ記録を合わせる
        for(int i = 0; i < recode.Count; i++) 
        {
            tempRecodeList.Add(recode[i]);
        }
        for(int i = 0; i < rankingData.Count; i++) 
        {
            tempRecodeList.Add(rankingData[i].timeData);
        }

        //ソートを行う
        rankingSort.InsertSort(tempRecodeList);

        //順位付けを行う
        rankingData.Clear();
        int rank = 1;
        for(int i = 0; i < 4; i++)
        {
            //書き込み用データに追加
            rankingData.Add(new RankingData(rank, tempRecodeList[i]));

            //順位ずらしは3位まで
            if(i > tempRecodeList.Count - 1) 
            {
                break;
            }

            //タイムが異なる場合順位をずらす
            if(tempRecodeList[i] != tempRecodeList[i + 1])
            {
                rank++;
            }

        }

        //記録を書き込む
        OutRecord();

    }

    /// <summary>
    /// @brief タイムのリストを受け取りcsvに出力する
    /// </summary>
    /// <param name="recode">タイムが格納されたリスト</param>
    /// <param name="isSort">trueでソートを実行。falseでは実行しない</param>
    private void OutRecord() 
    {
        StreamWriter sw = new StreamWriter(@inputFileName, false, Encoding.GetEncoding(CHARA_CODE));

        //見出し部分の書き込み
        sw.WriteLine("Rank,Time");
        
        for(int i = 0; i < OUTPUT_RECODE_NUM; i++) 
        {
            string rankStr = rankingData[i].rankData.ToString();
            string timeStr = rankingData[i].timeData.ToString();
            string wrStr = rankStr + "," + timeStr;

            sw.WriteLine(wrStr);
        }

        sw.Close();

    }

    /// <summary>
    /// @brief csvを読み取りタイムを格納する
    /// </summary>
    private void InRecord()
    {

        //ファイルが存在しなかった場合の処理
        if(!System.IO.File.Exists(inputFileName)) 
        {
            Debug.Log("File Not Found");
            Debug.Log("Create New File");
            InitRecode();
            return;
        }

        StreamReader sr = new StreamReader(@inputFileName, Encoding.GetEncoding(CHARA_CODE));
        string tempLine;

        //見出し部分は除外
        tempLine = sr.ReadLine();
        while((tempLine = sr.ReadLine()) != null)
        {
            RankingData tempData = new RankingData();
            string[] tempStr = tempLine.Split(',');         //順位とタイムで分割
            tempData.rankData = int.Parse(tempStr[0]);
            tempData.timeData = float.Parse(tempStr[1]);
            
            rankingData.Add(tempData);
            
        }

        sr.Close();

    }

    /// <summary>
    /// @brief 新規csvファイルを作成するための仮データを作成する
    /// </summary>
    private void InitRecode()
    {

        //初期データの作成
        for(int i = 0; i < OUTPUT_RECODE_NUM; i++)
        {
            rankingData.Add(new RankingData((i+1), 300 + (60 * i)));
        }

        OutRecord();

    }

    /// <summary>
    /// //@brief ランキングのデータをテキストに代入する
    /// </summary>
    private void TextWrite()
    {

        string[] rank = { "st","nd","rd","th" };

        for(int i = 0; i < OUTPUT_RECODE_NUM; i++) 
        {
            objectRankText[i].text = rankingData[i].rankData.ToString() + rank[rankingData[i].rankData - 1];

            float time = rankingData[i].timeData;
            string[] unit = new string[] 
            {
                Mathf.Floor((Mathf.Floor(time)) / 60).ToString(),       //分
                (Mathf.Floor(time) % 60).ToString("00"),                //秒
                ((time - Mathf.Floor(time)) * 1000).ToString("000"),    //ミリ秒
            };

            objectTimeText[i].text = unit[0] + ":" + unit[1] + "." + unit[2];
            Debug.Log(unit[0] + "分 : " + unit[1] + "秒" + unit[2] + "ミリ秒");
        }

    }

}
