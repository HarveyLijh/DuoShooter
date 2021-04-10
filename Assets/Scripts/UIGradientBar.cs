using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGradientBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fillImage;

	public void SetMaxBarVal(float value)
	{
		slider.maxValue = value;
		slider.value = value;

		fillImage.color = gradient.Evaluate(1f);
	}

	public void SetValue(float value)
	{
		slider.value = value;

		fillImage.color = gradient.Evaluate(slider.normalizedValue);
	}

}
