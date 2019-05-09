/***************************************************
 
 制作者 :沢井　伶奈
 制作日 :2017.12.1
 内容   :ボタンが飛ばすところとシーンが同じか判断
　　     シーンが同じだったらボタンを消去する
         シーンが切り替わる直後ボタンをfalseからtureに。また、flgを変える
      
         SeneReload()→シーンをロード
         CurrentScene→現在のシーン
         flg→ボタンのfalseとtureの管理
****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : BaseObject
{

    private bool flg;
    void Start()
    {

        SceneReload();

    }


    void Update()
    {

    }

    public void SceneReload()
    {
        //現在のシーン名を取得
        string CurrentScene = SceneManager.GetActiveScene().name;

        //現在のシーンが同じだったら
        if (CurrentScene == "ModeSelect")//モードセレクト
        {
            // GameObject.Find("ボタンの名前").SetActive(false);
            //ボタンを非表示にする
            //GameObject.Find("home").SetActive(false);
            //sdflg = false;
        }

        else if (CurrentScene == "Title")//タイトル
        {
            GameObject.Find("title").SetActive(false);
            flg = false;
        }
        else if (CurrentScene == "Battlerecord")//戦績
        {
            GameObject.Find("battlerecord").SetActive(false);
            flg = false;
        }
        else if (CurrentScene == "Credit")//クレジット
        {
            GameObject.Find("credit").SetActive(false);
            flg = false;
        }
        else if (CurrentScene == "Configuration")//設定
        {
            GameObject.Find("configuration").SetActive(false);
            flg = false;
        }
        else if (CurrentScene == "View")//ビュー
        {
            GameObject.Find("view").SetActive(false);
            flg = false;
        }
        OnEnd();
    }

    public override void OnEnd()
    {

        base.OnEnd();
        //flgがfalseだったら実行
        if (flg == false)
        {
            GameObject.Find("title").SetActive(true);

            //flgを最後trueにしてまたシーンを取得する
            flg = true;
        }
    }

}
