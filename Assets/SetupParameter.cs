using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupParameter : MonoBehaviour
{
    GameObject prelaunchParams;
    GameObject flightStats;

    // Start is called before the first frame update
    void Start()
    {
        prelaunchParams = gameObject.transform.GetChild(0).gameObject;
        flightStats = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
        }

        if (Input.GetKeyDown(KeyCode.R)) {

        }
    }
}
