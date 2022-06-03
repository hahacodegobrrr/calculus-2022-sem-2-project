using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    Slider slider;

    private void Start() {
        slider = gameObject.GetComponent<Slider>();
        if (gameObject.name.Equals("rocket mass slider")) {
            slider.value = 0.113f / 2f;
        } else if (gameObject.name.Equals("fuel mass slider")) {
            slider.value = 0.0122f / 0.2f;
        } else if (gameObject.name.Equals("burn time slider")) {
            slider.value = 1.6f / 10f;
        } else if (gameObject.name.Equals("impulse slider")) {
            slider.value = 10 / 200f;
        }
    }

    private void Update() {
        if (gameObject.name.Equals("rocket mass slider")) {
            Rocket.rocket.rocketMass = (slider.value == 0? 0.00001f : slider.value) * 2;
        } else if (gameObject.name.Equals("fuel mass slider")) {
            Rocket.rocket.fuelMass = slider.value * 0.2f;
        } else if (gameObject.name.Equals("burn time slider")) {
            Rocket.rocket.burnTime = slider.value * 10;
        } else if (gameObject.name.Equals("impulse slider")) {
            Rocket.rocket.impulse = slider.value * 200;
        }
    }

}
