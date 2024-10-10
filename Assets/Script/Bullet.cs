using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]GameManager gameManager;

    Vector3 destination;//�ړI�n
    Vector3 direction;
    Transform thisTransform;
    GameObject c;
    [SerializeField] Explosion explosion;
    float speed;

    // Start is called before the first frame update
    void Start()
    {

        thisTransform = transform;
        speed = 6.0f;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition;
        Vector3 addVector;
        //�s���������߂��
        addVector = new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0);
        addVector.Normalize();

        newPosition = thisTransform.position + addVector * speed * Time.deltaTime;
        transform.position = newPosition;

        //���ł������Ɍ�����ς���
        transform.rotation=Quaternion.FromToRotation(Vector3.up,addVector);

        //�����ړI�n�ɂ����甚�����ď����܂���
        if (Vector2.Distance(transform.position, c.transform.position) < 0.1f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(c.gameObject);
            Destroy(gameObject);
        }
        
    }

    public void GetVector(Vector3 from, Vector3 to,GameObject cursor)
    {
        direction = new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);
        c = cursor;
        destination = to;
    }

}
