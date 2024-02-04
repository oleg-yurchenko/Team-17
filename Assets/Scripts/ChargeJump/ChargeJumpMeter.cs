using UnityEngine;
using UnityEngine.UI;

public class ChargeJumpMeter : MonoBehaviour
{
	[SerializeField] private Slider slider;
	
	private void Start() {
		slider = GetComponent<Slider>();
	}
	
	public void SetCharge(float charge) 
	{
		slider.value = charge;
	}
	
	public void SetMinCharge(float minCharge) 
	{
		slider.minValue = minCharge;
	}
	
	public void SetMaxCharge(float maxCharge) 
	{
		slider.maxValue = maxCharge;
	}
}