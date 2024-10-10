using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exp();
        if (transform.localScale.x >= 3.0f)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Exp()
    {
        Vector3 scale;
        scale = transform.localScale;
        scale.x += 2.0f * Time.deltaTime;
        scale.y += 2.0f * Time.deltaTime;
        transform.localScale = scale;
    }
}
