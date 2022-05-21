using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataWriter : MonoBehaviour
{
    bool recordingData;
    float timeSinceLastCollection;
    float timeSinceLaunch;
    StreamWriter writer;
    DataPoint dataBuffer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timeSinceLaunch += Time.deltaTime;
        timeSinceLastCollection += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !recordingData) {
            timeSinceLaunch = 0;
            string fileName = "Assets\\data.csv";

            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }
            writer = File.CreateText(fileName);
            writer.WriteLine(System.DateTime.Now.Date.Day + "/"
                + System.DateTime.Now.Month + "/"
                + System.DateTime.Now.Year + " "
                + ((System.DateTime.Now.Hour / 10 == 0)? "0" : "") + System.DateTime.Now.Hour + ":"
                + ((System.DateTime.Now.Minute / 10 == 0) ? "0" : "") + System.DateTime.Now.Minute + ":"
                + ((System.DateTime.Now.Second / 10 == 0) ? "0" : "") + System.DateTime.Now.Second);
            writer.WriteLine("Time(s),Altitude(m),Airspeed(m/s),Acceleration(m/ss),Total rocket mass(kg),Fuel ejection speed(m/s),Thrust(N),Engine Burning");
            recordingData = true;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            writer.Close();
            recordingData = false;
        }

        if (recordingData && timeSinceLastCollection >= 0.1f) {
            if (dataBuffer == null) {
                dataBuffer = new DataPoint(timeSinceLaunch, Rocket.rocket.altitude, Rocket.rocket.velocity.magnitude, Rocket.rocket.fuelEjectionSpeed, Rocket.rocket.totalRocketMass, 0, Rocket.rocket.thrust, Rocket.rocket.engineBurning);
            } else {
                dataBuffer = new DataPoint(timeSinceLaunch, Rocket.rocket.altitude, Rocket.rocket.velocity.magnitude, (Rocket.rocket.velocity.magnitude - dataBuffer.airSpeed) / timeSinceLastCollection, Rocket.rocket.totalRocketMass, Rocket.rocket.fuelEjectionSpeed, Rocket.rocket.thrust, Rocket.rocket.engineBurning);
            }
            writer.WriteLine(dataBuffer.ToString());
            timeSinceLastCollection = 0;
        }
    }
}

public class DataPoint {
    public float timeSinceLaunch; //calculated
    public float altitude; //fetched
    public float airSpeed; //fetched
    public float acceleration; //calculated
    public float totalRocketMass; //fetched
    public float fuelEjectionSpeed; //fetched
    public float thrust; //fetched
    public bool engineBurning; //fetched

    public DataPoint(float timeSinceLaunch, float altitude, float airSpeed, float acceleration, float totalRocketMass, float fuelEjectionSpeed, float thrust, bool engineBurning) {
        this.timeSinceLaunch = timeSinceLaunch;
        this.altitude = altitude;
        this.airSpeed = airSpeed;
        this.acceleration = acceleration;
        this.totalRocketMass = totalRocketMass;
        this.fuelEjectionSpeed = fuelEjectionSpeed;
        this.thrust = thrust;
        this.engineBurning = engineBurning;
    }

    public override string ToString() {
        return "" 
            + timeSinceLaunch + ","
            + altitude + ","
            + airSpeed + ","
            + acceleration + ","
            + totalRocketMass + ","
            + fuelEjectionSpeed + ","
            + thrust + ","
            + engineBurning;
    }
}