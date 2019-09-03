/*********************************************************************************************/
/*@file       AITestScript.cs
*********************************************************************************************
* @brief      AIの挙動を制御するクラス
*********************************************************************************************
* @note       継承不可
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AITestScript : MarkerBase
{

    #region 変数宣言
        
    private Vector2 myPos;                  // @brief 自身の場所
    private Vector2 myZIGUZAGUPos;          // @brief ジグザグ開始時の自身の場所
    
    private List<Vector2> markerPos;        // @brief マーカーの場所
	private List<GameObject> AIMarkerList;  // @brief AIが通るマーカーのコンポーネント

	private GameObject me;                  // @brief 自分のコンポーネント
	private GameObject mySail;              // @brief 自分のセールコンポーネント
    private GameObject myHuman;             // @brief 自分のヒトコンポーネント
	private GetWindParam getWindParam;      // @brief 風のベクトル

    private readonly Vector3 rotateL = new Vector3(0f, -1.5f, 0f);        // @brief 左旋回用変数
    private readonly Vector3 rotateR = new Vector3(0f, 1.5f, 0f);         // @brief 右旋回用変数

    private float AISpeed;                  // @brief 現在のスピード
	private float AITopSpeed;               // @brief 出せる最高速度
	private float sailRotate;               // @brief セールの角度
       
	private float myRadius;                 // @brief 船が向いてる角度
	private float turnRadius;               // @brief 次のマーカーまでのラジアン
	private float turnDegree;               // @brief 次のマーカーまでの度数

    private float markerDistance;           // @brief ブイから次のブイまでの距離
	private float currentDistance;          // @brief 自身から次のブイまでの現在の距離
    
	private const float ableMoveDegree = 15f; // @brief 自身が進める角度
     
    /// <summary>
    /// @beief AIがどの状態で進んでいるか
    /// </summary>
	private enum eAIStatus
	{
        eZIGUZAGU,
        eTurn,
        eGoal,
        NULL

	}
    
    /// <summary>
    /// @beief 曲がる時の状態
    /// </summary>
    private enum eAIMoveStatus
    {
        eRight,
        eLeft,
        NULL
    }
    
    /// <summary>
    /// @beief ジグザグに進む時のシーケンス
    /// </summary>
    private enum eAISequence
    {
        eSetUp,
        eFirst,
        eSecond,
        eThird,
        eLast,
        NULL
    }

	private eAIStatus AIStatus;     // @beief 現在のAIの状態
    private eAISequence AISequence; // @brief 現在のジグザグのシーケンス

    #endregion
    
    #region 初期化
       
    /// <summary>
	/// @brief MarkerBaseの実装
	/// </summary>
    protected override void MarkerInitialize()
	{
		base.MarkerInitialize();

        // リストの初期化
		markerPos = new List<Vector2>();
		AIMarkerList = new List<GameObject>();
		AIMarkerList = GameObjectExtension.GetGameObject(markerObjName, DecideAIStrength());

        // コンポーネントの取得
		me = this.gameObject;
		mySail = me.transform.Find("Sail").gameObject;
		myHuman = me.transform.Find("Human").gameObject;
		getWindParam = GameObjectExtension.Find("UIWind").GetComponent<GetWindParam>();
        
        // 変数の初期化
		myRadius = transform.localEulerAngles.y;
		currentMarker = 0;
		currentHitMarker = 1;

		AIStatus = eAIStatus.NULL;
        AISequence = eAISequence.NULL;
       
		GetMarkerVec2();
        currentDistance = Distance(myPos, markerPos[currentHitMarker]);

		NextPointDeg();
		SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

    }
    
    #endregion

    #region 更新関数
    
    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
    public override void OnUpdate()
	{
        base.OnUpdate();

        // セールをまげつつ速度の計算
        SailRotate(getWindParam.ValueWind, me.transform.localEulerAngles.y);

    }

    /// <summary>
    /// @brief BaseObjectの実装
    /// </summary>
	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

		ShipMove();

		switch (AIStatus)
		{
			case eAIStatus.eZIGUZAGU:
                
				MoveTypeZIGUZAGU(NextPointDeg());
				break;

			case eAIStatus.eTurn:

				ShipRotate(NextPointDeg());
				break;

			case eAIStatus.eGoal:

                transform.Rotate(rotateR);        
				break;

			case eAIStatus.NULL:
                break;
		}
	}

    #endregion


    #region 関数の実装

    /// <summary>
    /// @brief AIの強さの決定
    /// </summary>
    /// <return> 通るオブジェクトの名前 </returns>
    private string DecideAIStrength()
    {
        string temp;

        switch ((int)Random.Range(0.0f, 3.0f))
        {
            case 0:
                temp = "AIFast";
                break;

            case 1:
                temp = "AINormal";
                break;

            case 2:
                temp = "AILate";
                break;

            default:
                temp = "GameObject";
				Debug.LogWarning("<color=red>StageがAIに対応してません</color>");
				break;
        }

        return temp;
    }


    /// <summary>
	/// @brief 使うコンポーネントの座標をVector2型にしてリスト化
	/// </summary>
    private void GetMarkerVec2()
	{
        myPos = new Vector2(transform.position.x, transform.position.z);
        
		for (int i = 0; i < AIMarkerList.Count; i++)
		{
			markerPos.Add(new Vector2(AIMarkerList[i].transform.position.x,
									  AIMarkerList[i].transform.position.z));
		}
    }

    #endregion

    /// <summary>
    /// @brief 次のブイの角度を求める
    /// </summary>
    /// <returns> 次のブイのラジアン値 </returns>
    private float NextPointDeg()
	{
		myRadius = transform.localEulerAngles.y;

        turnRadius = Mathf.Atan2(markerPos[currentHitMarker].y - transform.position.y,
								 markerPos[currentHitMarker].x - transform.position.x);

		turnDegree = turnRadius * Mathf.Rad2Deg + 87;

		return turnDegree;
	}
    
    /// <summary>
    /// @brief 次のブイがどっち回りかを求める
    /// </summary>
    /// <returns> 曲がる方向 </returns>
    /// <param name="turnDegree"> 次のブイの角度 </param>
    private eAIMoveStatus NextTurnDirection(float turnDegree)
    {
        // 左
        if (Mathf.DeltaAngle(myRadius, turnDegree) < 0)
        {
            return eAIMoveStatus.eLeft;           
        }
        // 左
        else  if (Mathf.DeltaAngle(myRadius, turnDegree) > 1)
        {
            return eAIMoveStatus.eRight;
        }

        return eAIMoveStatus.NULL;
        
    }

    #region Ship and Sail Moving

    /// <summary>
	/// @brief 船の直進処理
	/// </summary>
    private void ShipMove()
	{
		if (AISpeed < AITopSpeed)
		{
			AISpeed += 3 * Time.deltaTime;

            // TODO
            // AISPeed += getWindParam.WindForce が本当は使いたい
            // 実装されたら変えてくれ
        }
        if (AISpeed > AITopSpeed)
		{
			AISpeed -= 3 * Time.deltaTime;
		}

        if (!Singleton<GameInstance>.Instance.IsShipMove)
		{
			AISpeed = 20;
		}

        // 進む
        transform.position = transform.forward * -AISpeed * Time.deltaTime;

        // 速度が遅かったらジグザグに走らせる
		if (AITopSpeed < 30 && AIStatus != eAIStatus.eTurn)
		{
            AIStatus = eAIStatus.eZIGUZAGU;
		}
	}

    /// <summary>
    /// @brief 現在の船の角度と風のベクトルからセールの角度を求める
    /// </summary>
    /// <param name="windVector"> 風のベクトル </param>
    /// <param name="rotate"> 自身の角度 </param>
    private void SailRotate(float windVector, float rotate)
	{
		float temp = 10;

		rotate -= 180;

        if (rotate >= windVector + ableMoveDegree)
		{
			sailRotate = 10 + ((rotate - ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (60 - 10) / 180));
		}
		if (rotate <= windVector - ableMoveDegree)
		{
			sailRotate = 10 + ((rotate + ableMoveDegree) * 80 / (180 - ableMoveDegree));
			temp = Mathf.Abs(10 + ((rotate - ableMoveDegree) * (60 - 10) / 180));
		}

		mySail.transform.localEulerAngles = new Vector3(0, sailRotate, 0);

		AITopSpeed = temp;
	}

    /// <summary>
    /// @brief 船の回転
    /// </summary>
    private void ShipRotate(float turnDeg)
	{
        // 左
        if (NextTurnDirection(turnDeg) == eAIMoveStatus.eLeft)
		{
			transform.Rotate(rotateL);
		}

        // 右
        else if (NextTurnDirection(turnDeg) == eAIMoveStatus.eRight)
		{
			transform.Rotate(rotateR);
		}
        
	}

    /// <summary>
    /// @brief ジグザグに動く処理
    /// </summary>
    /// <param name="turnDegree"> 次のブイの角度 </param>
    private void MoveTypeZIGUZAGU(float turnDegree)
    {
        // このフレームでのブイ目での距離
        currentDistance = Distance(myPos, markerPos[currentHitMarker]);

        switch (AISequence)
        {
            case eAISequence.NULL:
                // ジグザグの準備
                AISequence = eAISequence.eSetUp;
                markerDistance = Distance(myPos, markerPos[currentHitMarker]);
                myZIGUZAGUPos = transform.position;
                return;
               
            case eAISequence.eSetUp:
                
                // 最高速度が上がるまで船に角度をつける
                if(AITopSpeed < 40)
                {
                    if (NextTurnDirection(turnDegree) == eAIMoveStatus.eLeft)
                    {
                        transform.Rotate(rotateL);
                    }
                    else
                    {
                        transform.Rotate(rotateR);
                    }
                }
                
                else
                {
                    AISequence = eAISequence.eFirst;
                }

                return;
                
            case eAISequence.eFirst:
                
                // マーカーまで半分走ったら次のシークエンス
                if (Distance(myPos, myZIGUZAGUPos) > markerDistance * 0.5)
                {
                    AISequence = eAISequence.eSecond;
                }

                break;
                
            case eAISequence.eSecond:
                
                // マーカーの方向に船の角度を戻して進む
                if(NextTurnDirection(turnDegree) == eAIMoveStatus.NULL)
                {
                    AISequence = eAISequence.eLast;
                }
                else
                {
                    ShipRotate(NextPointDeg());
                }

                break;
                
            case eAISequence.eThird:
                AISequence = eAISequence.eLast;
                break;
                
            case eAISequence.eLast:
                AISequence = eAISequence.NULL;
                AIStatus = eAIStatus.NULL;
                break;
        }
    }

    
    #endregion

    /// <summary>
    /// @brief 次のブイまでの距離を求める
    /// </summary>
    /// <returns> 距離 </returns>
    /// <param name="meP"> 自身の場所 </param>
    /// <param name="youP"> 次のブイの場所 </param>
    private float Distance(Vector2 meP, Vector2 youP)
    {
        return (meP.x - youP.x) * (meP.x - youP.x) + (meP.y - youP.y) * (meP.y - youP.y);
    }

    /// <summary>
    /// @brief 当たった時に実行される処理
    /// </summary>
    /// <param name="other"> アタッチされているオブジェクト以外 </param>
    private void OnTriggerEnter(Collider other)
    {
        // 当たったゲームオブジェクトが、目的のマーカーの場所と一致した場合
        if (other.gameObject == AIMarkerList[currentHitMarker].gameObject)
        {
            // スタートとゴールが同じ場所にあった時にゴール判定にならないようにする処理
            if (other.tag == "goal")
            {
                isGoal = true;
                currentHitMarker = 1;
			    AIStatus = eAIStatus.eGoal;
            }
            else
            {
                // 現在通ったマーカーの総数を計算
                currentHitMarker += 1;
			    AIStatus = eAIStatus.eTurn;
            }
        }

        // 当たったゲームオブジェクトが、目的のブイの場所と一致した場合
	    if (other.gameObject == AIMarkerList[currentMarker].gameObject && other.tag != "goal")
        {
            // 現在通ったブイの総数を計算
            currentMarker++;
        }
    }    
}