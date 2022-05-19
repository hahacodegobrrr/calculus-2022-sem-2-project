using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    Slider slider;

    private void Start() {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update() {
        if (gameObject.name.Equals("starting mass slider")) {
            Rocket.rocket.setStartingMass(slider.value * 2799999 + 1);
        } else if (gameObject.name.Equals("fuel ejection rate slider")) {
            Rocket.rocket.setFuelBurnRate(slider.value * 19999 + 1);
        } else if (gameObject.name.Equals("fuel ejection speed slider")) {
            Rocket.rocket.setFuelEjectionSpeed(slider.value * 4999 + 1);
        } else if (gameObject.name.Equals("engine burn time")) {
            Rocket.rocket.setBurnTime(slider.value * 599 + 1);
        }
    }

}
