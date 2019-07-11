using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NewBehaviourScript : MonoBehaviour
{
    //ベジェ曲線用
    public struct Position
    {
        public float x;
        public float y;
    }
    private BezierCurve.Position[] p1;
    private BezierCurve.Position[] p2;

    [SerializeField, Range(0, 180.0f)] private float va;
    [SerializeField] private string debuglog1;
    [SerializeField] private string debuglog2;
    [SerializeField] private string debuglog3;
    [SerializeField] private string debuglog4;
    // Use this for initialization
    void Start()
    {
        p1 = new BezierCurve.Position[]
        {
        //0～45°
        new BezierCurve.Position {x = 0.0f, y = 0.0f},
        new BezierCurve.Position {x = 45.0f, y = 0.0f},
        new BezierCurve.Position {x = 30.0f, y = 75.0f},
        new BezierCurve.Position {x = 45.0f, y = 100.0f},
        };

        p2 = new BezierCurve.Position[]
        {
        //46～180°
        new BezierCurve.Position {x = 46.0f, y = 120.0f},
        new BezierCurve.Position {x = 75.0f, y = 100.0f},
        new BezierCurve.Position {x = 90.0f, y = 30.0f},
        new BezierCurve.Position {x = 180.0f, y = 30.0f}
        };
    }

    // Update is called once per frame
    void Update()
    {

        float b = va / 45.0f;
        float a = 1.0f - b;

        // (0%)0 ~ 45(100%)
        debuglog1 = "x1=" + (Mathf.Pow(a, 3) * p1[0].x + 3 * Mathf.Pow(a, 2) * b * p1[1].x + 3 * a * Mathf.Pow(b, 2) * p1[2].x + Mathf.Pow(b, 3) * p1[3].x).ToString();
        debuglog2 = "y1=" + (Mathf.Pow(a, 3) * p1[0].y + 3 * Mathf.Pow(a, 2) * b * p1[1].y + 3 * a * Mathf.Pow(b, 2) * p1[2].y + Mathf.Pow(b, 3) * p1[3].y).ToString();

        float bb = va / 180.0f;
        float aa = 1.0f - bb;

        // (100%)45 ~ 180(30%)
        debuglog3 = "x2=" + (Mathf.Pow(aa, 3) * p2[0].x + 3 * Mathf.Pow(aa, 2) * bb * p2[1].x + 3 * aa * Mathf.Pow(bb, 2) * p2[2].x + Mathf.Pow(bb, 3) * p2[3].x).ToString();
        debuglog4 = "y2=" + (Mathf.Pow(aa, 3) * p2[0].y + 3 * Mathf.Pow(aa, 2) * bb * p2[1].y + 3 * aa * Mathf.Pow(bb, 2) * p2[2].y + Mathf.Pow(bb, 3) * p2[3].y).ToString();
    }
}
