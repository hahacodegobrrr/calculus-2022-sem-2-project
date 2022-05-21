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
        for (int i = 0; i < flightStats.transform.childCount; i++) {
            flightStats.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < prelaunchParams.transform.childCount; i++) {
            prelaunchParams.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            for (int i = 0; i < prelaunchParams.transform.childCount; i++) {
                prelaunchParams.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < flightStats.transform.childCount; i++) {
                flightStats.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            for (int i = 0; i < flightStats.transform.childCount; i++) {
                flightStats.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < prelaunchParams.transform.childCount; i++) {
                prelaunchParams.transform.GetChild(i).gameObject.SetActive(true);
            }

        }
    }
}
