/***********************************************************************/
/*! @file   ShipSwipe.cs
*************************************************************************
*   @brief  スワイプでで船を操作するコントローラ
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;
using System.Collections;

public class ShipSwipe : BaseObject
{

    private bool isFlick;
    private bool isClick;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Singleton<SaveDataInstance>.Instance.ISSwipe)
        {
            //スタート時のフリック操作が利かない
            if (Singleton<GameInstance>.Instance.IsShipMove)
            {

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                    isFlick = true;
                    touchStartPos = new Vector3(Input.mousePosition.x,
                                Input.mousePosition.y,
                                Input.mousePosition.z);

                    Invoke("FlickOff", 0.2f);
                }

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    touchEndPos = new Vector3(Input.mousePosition.x,
                                Input.mousePosition.y,
                                Input.mousePosition.z);


                    float directionX = (touchEndPos.x - touchStartPos.x) * 0.003f;
                    if (directionX >= 1.0f) directionX = 1.0f;
                    if (directionX <= -1.0f) directionX = -1.0f;


                    transform.Rotate(0, directionX, 0);



                    if (touchStartPos != touchEndPos)
                    {
                        ClickOff();
                    }
                }


                if (Input.GetKeyUp(KeyCode.Mouse0))
                {

                }
            }
        }
    }
    public void FlickOff()
    {
        isFlick = false;
    }

    public bool IsFlick()
    {
        return isFlick;
    }


    public void ClickOn()
    {
        isClick = true;
        Invoke("ClickOff", 0.2f);
    }

    public bool IsClick()
    {
        return isClick;
    }

    public void ClickOff()
    {
        isClick = false;
    }
}