using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindUI : MonoBehaviour
{
	[SerializeField] private GameObject originObject = null;   // 原点となるオブジェクト（このオブジェクトを原点に回転）
	[SerializeField] private GameObject windObject = null;		// 風オブジェクト（UIは常にこのオブジェクトと反対の方向を向く）
	[SerializeField] private float angle = 0.0f;   // 風向き
	[SerializeField] private float range = 0.0f;   // 原点オブジェクトとの距離（必ずプレイヤーが見える位置に置く）

	LineRenderer line = null;	// debug

	void Start()
	{
		// 風向きに応じてUIの位置を決める
		float x = range * Mathf.Cos(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.x;
		float z = range * Mathf.Sin(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.z;
		this.transform.position = new Vector3(x, 0.0f, z);
		this.transform.LookAt(windObject.transform);


		//コンポーネントを取得する
		this.line = GetComponent<LineRenderer>();
		//線の幅を決める
		this.line.startWidth = 0.1f;
		this.line.endWidth = 0.1f;
		//頂点の数を決める
		this.line.positionCount = 2;

	}

	void Update()
	{
		// 風向きに応じてUIの位置を決める
		angle = originObject.GetComponent<LiftingForceCalculation>().GetAngle(originObject.transform.position, windObject.transform.position);
		float x = range * Mathf.Sin(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.x;
		float z = range * Mathf.Cos(3.14f * 2.0f / 360.0f * angle) + originObject.transform.position.z;
		this.transform.position = new Vector3(x, 0.0f, z);
		this.transform.LookAt(windObject.transform);


		line.SetPosition(0, originObject.transform.position);
		line.SetPosition(1, windObject.transform.position);
	}
}
