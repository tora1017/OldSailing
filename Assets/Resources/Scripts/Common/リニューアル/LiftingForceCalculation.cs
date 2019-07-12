using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 揚力計算
/*
 * 参考サイト
 * 揚力計算：https://ja.wikipedia.org/wiki/%E6%8F%9A%E5%8A%9B
 * 合力計算：https://kotobank.jp/word/%E5%90%88%E5%8A%9B-63346
 * 抗力係数：http://skomo.o.oo7.jp/f28/hp28_63.htm
 * 
 * 風力に応じて船の速度の原則を確認済み。
 */


//ベジェ曲線用
class Position
{
    public float x;
    public float y;

    public Position(float xx, float yy)
    {
        x = xx;
        y = yy;
    }
}

public class LiftingForceCalculation : MonoBehaviour
{
    [SerializeField] private float l;                               // 揚力（Lift）
    [SerializeField] private float p = 1;                           // 風の密度
    [SerializeField] private float v;                               // 相対速度（Vector）
    [SerializeField] private float s = 1;                           // 帆の面積（Surface）
    [SerializeField] private float cl = 1.12f;                      // 揚力係数（Coeffient of Lift）
    [SerializeField, Range(0, 1.0f)] private float windPower = 0;   // 風力
    [SerializeField] private float shipspeed;                       // 船の速度

    // 風の概念を足す
    [SerializeField] private float windAngle = 0.0f;    // 風の角度
    [SerializeField] private float shipAngle = 0.0f;           // 船の角度
    [SerializeField] private float va; // 船と風との相対速度
    [SerializeField] private float windPercent;   // 風倍率
    [SerializeField] private string debuglog;

    private Position[] pos;
    private Position[] pos2;

    public void Start()
    {
        //0～45°
        pos = new Position[4]
        {
            new Position(0.0f, 0.0f),
            new Position(40.0f, 0.0f),
            new Position(40.0f, 0.0f),
            new Position(45.0f, 100.0f),
        };
        //46～180°
        pos2 = new Position[4]
        {
            new Position(45.0f, 100.0f),
            new Position(80.0f, 60.0f),
            new Position(150.0f, 30.0f),
            new Position(180.0f, 30.0f),
        };
    }

    void Update()
    {

        WindForceCalculation();

        //揚力計算

        // 風力が船の推進力より高いとき　⇒　加速（ただし、風力を超えることはない）
        if (windPower > shipspeed)
        {
            v = windPower - shipspeed;                                          // 相対速度計算
            l = (p * (v * v) * s * cl) / 2;                                     // 揚力計算
            shipspeed = Mathf.Sqrt(Mathf.Pow(l, 2) + Mathf.Pow(shipspeed, 2));  //　合力加算
        }
        // 風力が船の推進力より低いとき　⇒　減速
        else if (windPower < shipspeed)
        {
            v = shipspeed - windPower;                                          // 相対速度計算
            l = (p * (v * v) * s * cl) / 2;                                     // 揚力計算
            shipspeed = Mathf.Sqrt(Mathf.Pow(shipspeed, 2) - Mathf.Pow(l, 2));  // 合力減算
        }
        this.transform.Translate(Vector3.forward * -shipspeed * Time.deltaTime * 30);   // 船の進行方向に対して加算
    }

    /// <summary>
    /// @brief 船の角度と風の角度に応じて風力を変化させる
    /// </summary>
    public void WindForceCalculation()
    {
        //角度計算...船の角度と風の角度に応じて風力を変化させる
        shipAngle = Mathf.RoundToInt(this.transform.localEulerAngles.y);
        va = Mathf.Abs(windAngle - shipAngle);
        if (va > 180.0f) { va = 360.0f - va; }    // 180度に換算

        // (0%)0 ~ 45(100%)
        if (va <= 45.0f)
        {
            float b = va / 45.0f;
            float a = 1.0f - b;
            windPercent =  (Mathf.Pow(a, 3) * pos[0].y + 3 * Mathf.Pow(a, 2) * b * pos[1].y + 3 * a * Mathf.Pow(b, 2) * pos[2].y + Mathf.Pow(b, 3) * pos[3].y);
            debuglog = "県内";
        }
        // (%)46 ~ 180(30%)
        else if(va > 45)
        {
            float b = (va - 45.0f) / 135.0f;
            float a = 1.0f - b;
            windPercent = (Mathf.Pow(a, 3) * pos2[0].y + 3 * Mathf.Pow(a, 2) * b * pos2[1].y + 3 * a * Mathf.Pow(b, 2) * pos2[2].y + Mathf.Pow(b, 3) * pos2[3].y);
            debuglog = "県外";
        }
    }

}
