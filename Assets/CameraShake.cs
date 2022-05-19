using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 baseLocation;

    private void Start() {
        baseLocation = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = baseLocation + new Vector3(1, 1, 1) * Random.Range(0, Rocket.rocket.velocity.magnitude / 1000 < 0.1f? Rocket.rocket.velocity.magnitude / 1000 : 0.1f);
    }
}
