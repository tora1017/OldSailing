using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoloPlay : MonoBehaviour {
    
    [SerializeField]private GameObject SoloPlay00;
    [SerializeField]private GameObject MainCamera;
    [SerializeField]private GameObject Menu01;
    private bool SoloMode = false;

    [SerializeField, Range(0, 10)]
    float time = 1;

    [SerializeField]
    Vector3 endPosition;

    private float startTime;
    private Vector3 startPosition;

    void OnEnable()
    {
        if (time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))  // ソロプレイボタンが押されるとUIを切り替える
        {
            Debug.Log("ok");
            if (Menu01.transform.position.x >= -500)
            {
                Menu01.transform.position += new Vector3(-500f, 0f, 0f);
                Debug.Log("ok");
            }
            if (!SoloMode) {
                if (Menu01.transform.position.x >= -500)
                {
                    Menu01.transform.position += new Vector3(-500f, 0f, 0f);
                    Debug.Log("ok");
                }

                //// ソロプレイモードのUI移動の処理
                //if (SoloPlay00.transform.position.x < )
                //{
                //    var diff = Time.timeSinceLevelLoad - startTime;
                //    if (diff > time)
                //    {
                //        transform.position = endPosition;
                //        enabled = false;
                //    }

                //    var rate = diff / time;
                //    transform.position = Vector3.Lerp(startPosition, endPosition, rate);
                //}
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            startPosition = transform.position;
        }

        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
#endif
        Gizmos.DrawSphere(endPosition, 0.1f);
        Gizmos.DrawSphere(startPosition, 0.1f);

        Gizmos.DrawLine(startPosition, endPosition);
    }
}
