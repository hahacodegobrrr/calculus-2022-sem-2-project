using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static Rocket rocket;
    public LayerMask groundMask;

    //starting parameters
    public double rocketMass; //in kg
    public double fuelMass; //in kg
    public double burnTime; //in seconds
    public double impulse; //in newton-seconds

    //data collection
    public Vector3 velocity;
    public double totalRocketMass;
    public double altitude;
    public double thrust;
    public double drag; //in newtons
    public bool engineBurning;

    GameObject smokeEffects;
    GameObject fireEffects;

    double fuelLeft;
    double timeLeftOnBurn;

    void Start() {
        //default values at sim start
        rocketMass = 1;
        fuelMass = 0.15f;
        burnTime = 8;
        impulse = 150;

        rocket = this;
        smokeEffects = gameObject.transform.GetChild(0).gameObject;
        fireEffects = gameObject.transform.GetChild(1).gameObject;
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

    void FixedUpdate()
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
            fireEffects.SetActive(true);
        }

        //when the engine time has been depleted, engine shuts off
        if (timeLeftOnBurn <= 0) {
            engineBurning = false;
            smokeEffects.SetActive(false);
            fireEffects.SetActive(false);
        }

        if (engineBurning) {
            //do something
            timeLeftOnBurn -= Time.deltaTime;
            fuelLeft -= (fuelMass / burnTime) * Time.deltaTime;
        }

        //physics calculations
        totalRocketMass = rocketMass + fuelLeft;
        velocity += (CalculateFNet() / (float)totalRocketMass) * Time.deltaTime;
        if (Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0) {
            velocity.y = 0;
        }
        drag = (0.75f * 0.00156f * 1.225f / 2) * velocity.magnitude * velocity.magnitude;
        gameObject.transform.position += velocity * Time.deltaTime;
    }

    Vector3 CalculateFNet() {
        Vector3 fG = new Vector3(0, (float)(-9.8f * totalRocketMass), 0);
        Vector3 fThrust = new Vector3(0, (float)(engineBurning ? impulse / burnTime : 0), 0);
        thrust = fThrust.magnitude;
        Vector3 fDrag = new Vector3(0, (float)(velocity.y > 0? -drag : drag), 0);
        Vector3 fNormal = new Vector3(0,
            (float)((Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0)? 9.8f * totalRocketMass : 0), 
            0);

        return fG + fThrust + fDrag + fNormal;
    }
}