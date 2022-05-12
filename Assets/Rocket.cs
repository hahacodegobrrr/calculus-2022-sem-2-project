using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float rocketMass;
    public float gravitationalAcceleration;
    public float fuelEjectionSpeed; //speed of 
    public float fuelEjectionRate; //mass over time

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //launch rocket
        if (Input.GetKeyDown(KeyCode.Space)) {

        }
    }
}
