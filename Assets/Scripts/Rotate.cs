using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float timerRotation;
    float speedRotation;

    private void Awake()
    {
        speedRotation = 180 / timerRotation;
    }
    // Update is called once per frame
    void Update()
    {
        timerRotation -= Time.deltaTime;
        if (timerRotation > 0)
            transform.Rotate(new Vector3(0, 0, speedRotation)* Time.deltaTime);
    }
}