using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraPosition;
    float shakeNumX;
    float shakeNumY;
    float shakeTimer;

    float max;
    float min;
    float maxTime;

    void Start()
    {
        cameraPosition = transform.position;
        shakeNumX = 0;
        shakeNumY = 0;


        max = 0;
        min = 0;
    }

    void Update()
    {

        shakeTimer -= Time.deltaTime;

        if (shakeTimer>=0)
        {
            float t = shakeTimer / maxTime;

            shakeNumX = Random.Range(min, max) * t;
            shakeNumY = Random.Range(min, max) * t;


            transform.position = cameraPosition + new Vector3(shakeNumX, shakeNumY, 0);

        }

        if (shakeTimer <= 0)
        {

            transform.position = cameraPosition;

        }

    }

    public void ShakeStart(float shakeTimer,float max,float min)
    {
        this.shakeTimer = shakeTimer;
        maxTime = shakeTimer;
        this.max = max;
        this.min = min;
    }

}
