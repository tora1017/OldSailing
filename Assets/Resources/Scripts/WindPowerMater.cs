using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WindPowerMater : MonoBehaviour
{
	private Slider slider = null;  // 風を受けている量
	[SerializeField] private GameObject windPowerSlider = null; 
	private LiftingForceCalculation liftingForceCalculation = null;

    // Start is called before the first frame update
    void Start()
    {
		slider = windPowerSlider.GetComponent<Slider>();
		liftingForceCalculation = GetComponent<LiftingForceCalculation>();
    }

    // Update is called once per frame
    void Update()
    {
		slider.value = liftingForceCalculation.windPercent * 100;
    }
}
