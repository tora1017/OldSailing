/***********************************************************************/
/*! @file   ShipController.cs
*************************************************************************
*   @brief  キーボードで船を操作するコントローラ(デバッグ用)
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using UnityEngine;

public class ShipController : BaseObject
{
    [SerializeField]
    private float moveSpeed; // @brief プレイヤーの進むスピード

    public float Speed
	{
		set { moveSpeed = value; }
		get { return moveSpeed; }
	}

   
    public void Start()
    {
        Singleton<ShipStates>.Instance.ShipState = eShipState.START;
        Singleton<GameInstance>.Instance.IsShipMove = false;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();


        if(Singleton<ShipStates>.Instance.ShipState == eShipState.STOP)
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Singleton<GameInstance>.Instance.IsShipMove)
        {
            Singleton<ShipStates>.Instance.ShipState = eShipState.START;
            // 移動
            if (Input.GetKey("right"))
            {
                transform.Rotate(0, 0.1f, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(0, -0.1f, 0);
            }
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;

           
        }
    }
}