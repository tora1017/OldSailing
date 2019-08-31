using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSlider : MonoBehaviour
{
	[SerializeField] private Text windPowerText;
	[SerializeField] private GameObject windPowerSlider;
	[SerializeField] private Text windAngleText;
	[SerializeField] private GameObject windAngleSlider;
	[SerializeField] private GameObject wind;
	[SerializeField] private float windAngle;

	private void Start()
	{
		SetWindAngle();
		SetWindPower();
	}

	public void SetWindPower()
	{
		windPowerText.text = "風速：" + windPowerSlider.GetComponent<Slider>().value + " km";
		wind.GetComponent<Wind>().SetWindPower(windPowerSlider.GetComponent<Slider>().value);
	}
	public void SetWindAngle()
	{
		windAngleText.text = "風向き：" + windAngleSlider.GetComponent<Slider>().value + " °";
		wind.GetComponent<Wind>().SetWindAngle(windAngleSlider.GetComponent<Slider>().value);
	}
}
