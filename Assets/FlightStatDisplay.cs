using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightStatDisplay : MonoBehaviour
{
    Text text;

    private void Start() {
        text = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Equals("airspeed")) {
            text.text = "Air speed: " + Rocket.rocket.velocity.magnitude + " m/s";
        } else if (gameObject.name.Equals("mass")) {
            text.text = "Mass: " + Rocket.rocket.totalRocketMass + " kg";
        } else if (gameObject.name.Equals("thrust")) {
            text.text = "Thrust: " + Rocket.rocket.thrust + " N";
        }
    }
}
