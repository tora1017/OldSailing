using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicalSwitch : MonoBehaviour
{
	[SerializeField] private Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField] private Quaternion startRoration = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
	[SerializeField] private GameObject windPowerSlider = null;
	[SerializeField] private float startWindPowerParam = 0.0f;
	[SerializeField] private GameObject windAngleSlider = null;
	[SerializeField] private float startWindAngleParam = 0.0f;

	private void Start()
	{
		startPosition = this.transform.position;
		startRoration = this.transform.rotation;
		startWindPowerParam = windPowerSlider.GetComponent<Slider>().value;
		startWindAngleParam = windAngleSlider.GetComponent<Slider>().value;

	}
	public void ResetPosition()
	{
		this.transform.position = startPosition;
		this.transform.rotation = startRoration;
		windPowerSlider.GetComponent<Slider>().value = startWindPowerParam;
		windAngleSlider.GetComponent<Slider>().value = startWindAngleParam;
	}
}
