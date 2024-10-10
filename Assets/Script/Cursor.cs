using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    
    Vector3 clickPoint;
    public bool isClick;
    
    // Start is called before the first frame update
    void Start()
    {

        isClick = false;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 rotation = transform.localEulerAngles;
        rotation.z += 300.0f * Time.deltaTime;
        transform.localEulerAngles = rotation;

    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    
}
