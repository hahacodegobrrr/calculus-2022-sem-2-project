using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static Rocket rocket;
    public LayerMask groundMask;

    //starting parameters
    public float rocketMass; //in kg
    public float fuelMass; //in kg
    public float burnTime; //in seconds
    public float impulse; //in newton-seconds

    //data collection
    public Vector3 velocity;
    public float totalRocketMass;
    public float altitude;
    public float thrust;
    public float fuelEjectionSpeed;
    public bool engineBurning;

    GameObject smokeEffects;


    float fuelLeft;
    float timeLeftOnBurn;

    void Start() {
        //default values at sim start
        rocketMass = 1;
        fuelMass = 0.15f;
        burnTime = 8;
        impulse = 150;

        rocket = this;
        smokeEffects = gameObject.transform.GetChild(0).gameObject;
        ResetSim();
    }

    private void ResetSim() {
        smokeEffects.SetActive(false);
        engineBurning = false;
        velocity = new Vector3(0, 0, 0);
        gameObject.transform.position = new Vector3(0, 5.3f, 0);
        timeLeftOnBurn = burnTime;
        fuelLeft = fuelMass;
    }

    void Update()
    {
        altitude = gameObject.transform.position.y;

        //reset
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetSim();
            return;
        }

        //launch rocket
        if (Input.GetKeyDown(KeyCode.Space)) {
            engineBurning = true;
            smokeEffects.SetActive(true);
        }

        //when the engine time has been depleted, engine shuts off
        if (timeLeftOnBurn <= 0) {
            engineBurning = false;
            smokeEffects.SetActive(false);
        }

        if (engineBurning) {
            //do something
            timeLeftOnBurn -= Time.deltaTime;
            fuelLeft -= (fuelMass / burnTime) * Time.deltaTime;
        }

        //physics calculations
        totalRocketMass = rocketMass + fuelLeft;
        velocity += (CalculateFNet() / totalRocketMass) * Time.deltaTime;
        if (Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0) {
            velocity.y = 0;
        }
        gameObject.transform.position += velocity * Time.deltaTime;
    }

    Vector3 CalculateFNet() {        
        Vector3 fG = new Vector3(0, -9.8f * totalRocketMass, 0);
        Vector3 fThrust = new Vector3(0, engineBurning ? impulse / burnTime : 0, 0);
        thrust = fThrust.magnitude;
        Vector3 fDrag = new Vector3();
        Vector3 fNormal = new Vector3(0,
            (Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0)? 9.8f * totalRocketMass : 0, 
            0);

        return fG + fThrust + fDrag + fNormal;
    }
}