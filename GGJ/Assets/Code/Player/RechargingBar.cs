using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class RechargingBar : MonoBehaviour
{


    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 100;
        slider.value = slider.maxValue;
    }

    public void DecreaseValue(float amount)
    {
        slider.value -= amount;
        if(slider.value < 1)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
    }

    public void RegenValue(float amount)
    {
        slider.value = amount;
        if (slider.value > 10)
        {
            slider.fillRect.gameObject.SetActive(true);
        }
    }


}
