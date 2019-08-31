/**********************************************************************************************/
/*@file       BaseObject.cs
*********************************************************************************************
* @brief      すべてのオブジェクトを管理するための基底クラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControl : MonoBehaviour
{
	[SerializeField] private GameObject wind = null;

    void Update()
    {
        shori();
    }

    void shori()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0.0f, -10.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.Rotate(0.0f, 10.0f, 0.0f);
            }
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				wind.GetComponent<Wind>().SetWindAngle(wind.GetComponent<Wind>().GetWindAngle() + 10);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				wind.GetComponent<Wind>().SetWindAngle(wind.GetComponent<Wind>().GetWindAngle() - 10);
			}
		}
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.transform.Rotate(0.0f, -1.0f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.transform.Rotate(0.0f, 1.0f, 0.0f);
            }
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				wind.GetComponent<Wind>().SetWindAngle(wind.GetComponent<Wind>().GetWindAngle() + 1);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				wind.GetComponent<Wind>().SetWindAngle(wind.GetComponent<Wind>().GetWindAngle() - 1);
			}
		}
	}
}
