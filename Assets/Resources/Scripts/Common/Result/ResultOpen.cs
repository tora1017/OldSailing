/**********************************************************************************************/
/*@file       ResultOpen.cs
*********************************************************************************************
* @brief      リザルト用ポップアップを開くクラス
*********************************************************************************************
* @author     Yuta Takatsu
*********************************************************************************************
* Copyright © 2018 Yuta Takatsu All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultOpen : BaseObject 
{

    [SerializeField]
    private GameObject resultPopup; // @brief Resultのインスタンス化

    private bool isCallOnse;
    private PopupResult result;

    private void Start()
    {
        isCallOnse = false;
        result = resultPopup.GetComponent<PopupResult>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!isCallOnse && BaseObjectSingleton<GameInstance>.Instance.IsPopup == true)
        {
            if (Singleton<GameInstance>.Instance.IsGoal == true)
            {


                if (SceneManager.GetActiveScene().name == "InTutorial")
                {
                    result.Open();
                }
                else if(SceneManager.GetActiveScene().name == "InGame")
                {
                    Singleton<ShipStates>.Instance.CameraMode = eCameraMode.GOAL;
                    if(Singleton<ShipStates>.Instance.CameraMode == eCameraMode.GOAL)
                    StartCoroutine(InGameResult());
                }
                isCallOnse = true;

            }
        }

        if (BaseObjectSingleton<GameInstance>.Instance.IsPopup == false)
            result.Close();

    }

    public IEnumerator InGameResult()
    {

        yield return new WaitForSeconds(5.0f);
        result.Open();
        Singleton<GameInstance>.Instance.IsShipMove = true;

    }
}
