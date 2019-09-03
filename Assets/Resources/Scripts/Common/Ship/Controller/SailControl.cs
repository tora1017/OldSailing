/***********************************************************************/
/*! @file   TitleShipAnimation.cs
*************************************************************************
*   @brief  風と進行方向に対して最適な帆の角度を算出して移動させるクラス
*************************************************************************
*   @author Ryo Sugiyama
*************************************************************************
*   Copyright © 2017 Ryo Sugiyama All Rights Reserved.
************************************************************************/
using UnityEngine;

public class SailControl : BaseObject {    
    
	private Transform sail;            // @brief 船のセールオブジェクトを格納する変数
	private GameObject human;
	private GameObject moveCircle;
	private ShipController shipController;
	private GetWindParam windVec;
    

	private float sailRotate;

	[SerializeField]
	private float minSpeed;
    
    [SerializeField]
	private float maxSpeed;

	private float constantValue;

	private const float ableMoveDegree = 15f;

	private float curMaxSpeed;

    private void Start()
    {
    
		sail = this.transform.Find("Sail");
        

		//human = GameObjectExtension.Find("Human");
		//moveCircle = GameObjectExtension.Find("Circle");
		//shipController = gameObject.GetComponent<ShipController>();
		windVec = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();
        

		minSpeed = 10;
		maxSpeed = 50;

		//constantValue = (maxSpeed - minSpeed) / 180;

		//CircleChangeRotate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
		//constantValue = (maxSpeed - minSpeed) / 180;
		SailRotate(windVec.ValueWind, this.transform.localEulerAngles.y);
		//CircleMove();

    }

	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
        /*
		if (shipController.Speed < curMaxSpeed)
        {
            shipController.Speed += 3 * Time.deltaTime;

            // TODO
			// shipController.Speed += getWindParam.WindForce * Time.deltaTime; が本当は使いたい
            // 実装されたら変えてくれ
        }

        if (shipController.Speed > curMaxSpeed)
        {
            shipController.Speed -= 3 * Time.deltaTime;
        }

        if (!Singleton<GameInstance>.Instance.IsShipMove)
        {
            shipController.Speed = 20;
        }
        */
	}


	/// <summary>
	/// @brief 風と進行方向に対して最適な帆の角度を算出して移動させる
	/// </summary>
	/// <param name="windVector"> 風のベクトルの方向 </param>
	/// <param name="playerRotate"> プレイヤーが進行しているベクトルの方向 </param>
	private void SailRotate(float windVector, float playerRotate)
    {
  
		playerRotate -= 180;

		if(playerRotate >= windVector + ableMoveDegree)
		{
			sailRotate = 10 + ((playerRotate - ableMoveDegree) * (80 / (180 - ableMoveDegree)));
			//curMaxSpeed = Mathf.Abs(10 + ((playerRotate - ableMoveDegree) * constantValue));
		}

		if (playerRotate <= windVector - ableMoveDegree)
        {
			sailRotate = -10 + ((playerRotate + ableMoveDegree) * (80 / (180 - ableMoveDegree)));
			//curMaxSpeed = Mathf.Abs(10 + ((playerRotate - ableMoveDegree) * constantValue));
        }
     
		sail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);


    }

    public void CircleMove()
	{
		moveCircle.transform.position = this.transform.position;
	}

    public void CircleChangeRotate()
	{
		moveCircle.transform.eulerAngles = new Vector3(90, windVec.ValueWind * -1, 0);
	}

    /* セールの角度を算出する計算式について */

    // 風向きを0°とする
	// 船が風向きに対して進める角度は、ableMoveDgree ~ 180° : -ableMoveDgree° ~ -180° である。
    // 自艇の角度が45°の時、セールの角度は10°である
	// 自艇の角度が180°の時、セールの角度は90°である
    // この時、自艇の角度1°大きくなるたびにセールの角度は0.5925大きくなる
    //
    // よって、自艇の角度から、セールの角度を求める式は、
    // 自艇の角度をx、セールの角度をyとする
    //
	// 自艇の角度xが、x < ableMoveDgree ではない時　　(右)
	// y =  10 + ((x - ableMoveDgree) * 0.5925)
	// 自艇の角度xが、x < -ableMoveDgree ではない時　(左)
	// y = -10 + ((x + ableMoveDgree) * 0.5925)
    // となる


}
