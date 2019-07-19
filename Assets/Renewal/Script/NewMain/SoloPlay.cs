using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoloPlay : MonoBehaviour {
    
    [SerializeField]private GameObject SoloPlay00;
    [SerializeField]private GameObject MainCamera;
    [SerializeField]private GameObject Menu01;
	
	// Update is called once per frame
	void Touch () {
        if (Input.GetMouseButtonUp(0))  // ソロプレイボタンが押されるとメインカメラに映す
        {
            if (MainCamera.transform.position.x >= SoloPlay00.transform.position.x)
            {
                SoloPlay00.transform.position = new Vector3(transform.position.x - 1, 0, 0);
                Menu01.transform.position = new Vector3(transform.position.x - 1, 0, 0);
            }
        }

        /*void OnBecameInvisible()
        {
            // 表示されなくなった時の処理
            Destroy("Menu01");
        }*/

    }
}
