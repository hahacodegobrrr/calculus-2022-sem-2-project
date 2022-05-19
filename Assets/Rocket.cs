using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static Rocket rocket;
    public LayerMask groundMask;

    float startingRocketMass; //in kilograms

    float fuelEjectionSpeed; //meters per second

    float fuelEjectionRate; //mass per second

    float engineBurnTime; //total burn time of engine


    public Vector3 velocity;
    float currentRocketMass;
    float fuelConsumed;
    float timeLeftOnBurn;
    bool engineBurning;

    void Start() {
        //default values at sim start
        startingRocketMass = 1;
        fuelEjectionSpeed = 1;
        fuelEjectionRate = 1;
        engineBurnTime = 0;
        rocket = this;
        currentRocketMass = startingRocketMass;
        timeLeftOnBurn = engineBurnTime;
        velocity = new Vector3(0, 0, 0);
    }

    private void ResetSim() {
        engineBurning = false;
        velocity = new Vector3(0, 0, 0);
        gameObject.transform.position = new Vector3(0, 5.3f, 0);
        timeLeftOnBurn = engineBurnTime;
        currentRocketMass = startingRocketMass;
    }

    void Update()
    {
        //reset
        if (Input.GetKeyDown(KeyCode.R)) {
            ResetSim();
            return;
        }

        //launch rocket
        if (Input.GetKeyDown(KeyCode.Space)) {
            engineBurning = true;
        }

        //when the engine time has been depleted, engine shuts off
        if (timeLeftOnBurn <= 0) {
            engineBurning = false;
        }

        if (engineBurning) {
            //do something
            timeLeftOnBurn -= Time.deltaTime;
            fuelConsumed += fuelEjectionRate * Time.deltaTime;
            currentRocketMass -= fuelEjectionRate * Time.deltaTime;
        }

        //physics calculations
        velocity += (CalculateFNet() / currentRocketMass) * Time.deltaTime;
        if (Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0) {
            velocity.y = 0;
        }
        gameObject.transform.position += velocity * Time.deltaTime;
    }

    Vector3 CalculateFNet() {        
        Vector3 fG = new Vector3(0, -9.8f * currentRocketMass, 0);
        Vector3 fThrust = new Vector3(0, engineBurning ? fuelEjectionRate * fuelEjectionSpeed : 0, 0);
        Vector3 fDrag = new Vector3();
        Vector3 fNormal = new Vector3(0,
            (Physics.CheckSphere(gameObject.transform.position - new Vector3(0, 5.3f, 0), 1, groundMask) && velocity.y < 0)? 9.8f * currentRocketMass : 0, 
            0);

        return fG + fThrust + fDrag + fNormal;
    }

    public void setStartingMass(float m) {
        startingRocketMass = m;
    }
    public void setFuelBurnRate(float m) {
        fuelEjectionRate = m;
    }
    public void setFuelEjectionSpeed(float m) {
        fuelEjectionSpeed = m;
    }
    public void setBurnTime(float m) {
        engineBurnTime = m;
    }
}