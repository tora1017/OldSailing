using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 必ずアローUIにアタッチしてください。
/*
 * オブジェクトを原点として回転：https://qiita.com/FumioNonaka/items/c246aca8f1b1b03a66be
*/

public class Wind : MonoBehaviour
{
	[SerializeField] private GameObject originObject = null;   // 原点となるオブジェクト（このオブジェクトを原点に回転）
	[SerializeField] private float power = 0.0f;
	[SerializeField] private float angle = 0.0f;   // 風向き（0～360） 
	[SerializeField] private float range = 0.0f;   // オブジェクトとの距離（必ずマップの外になるように）

	void Start()
	{
		float x = range * Mathf.Cos(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.x;
		float z = range * Mathf.Sin(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.z;
		this.transform.position = new Vector3(x, 0.0f, z);
	}

    void Update()
	{
		float x = range * Mathf.Cos(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.x;
		float z = range * Mathf.Sin(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.z;
		this.transform.position = new Vector3(x, 0.0f, z);
	}

	public float GetWindPower()
	{
		return power;
	}
	public float GetWindAngle()
	{
		return angle;
	}
	public void SetWindPower(float arg)
	{
		power = arg;
	}
	public void SetWindAngle(float arg)
	{
		angle = arg;
	}
}
