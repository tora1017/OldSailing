using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] private float gyroParam;	// ジャイロの傾き具合

    void Start()
    {

    }

    void Update()
    {
		GyroUpdata();
		if (gyroParam < -0.2f || 0.2f < gyroParam)
		{
			this.transform.Rotate(0.0f, gyroParam, 0.0f);
		}
	}

	private void GyroUpdata()
	{
		gyroParam = Input.acceleration.x;
	}
}