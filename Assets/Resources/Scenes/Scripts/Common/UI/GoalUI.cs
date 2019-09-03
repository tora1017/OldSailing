/**********************************************************************************************/
/*@file       CountDown.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using UnityEngine;
using UnityEngine.UI;

public class GoalUI : BaseObject
{
    
	[SerializeField] private Sprite maskUI;
    
    [SerializeField] private Sprite goalUI;
    
    private Image image;
    

    // Use this for initialization
    void Start()
    {
        image = this.GetComponent<Image>();
		image.sprite = maskUI;
        Singleton<GameInstance>.Instance.IsGoal = false;
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        base.OnUpdate();

       if (Singleton<GameInstance>.Instance.IsGoal)
        {
			image.sprite = goalUI;
        } 
    }
}
