using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] private float gyroParam;	// ジャイロの傾き具合

    void Update()
	{
		if (InputKey()) { return; }
		GyroUpdata();
	}

	private bool InputKey()
	{
		if(Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.Rotate(0.0f, 0.2f, 0.0f);
			return true;
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.Rotate(0.0f, -0.2f, 0.0f);
			return true;
		}
		return false;
	}
	private void GyroUpdata()
	{
		gyroParam = Input.acceleration.x;
		if (gyroParam < -0.2f || 0.2f < gyroParam)
		{
			this.transform.Rotate(0.0f, gyroParam, 0.0f);
		}
	}
}