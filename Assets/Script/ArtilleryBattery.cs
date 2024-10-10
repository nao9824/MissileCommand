using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtilleryBattery : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] TowerExplosion towerExplosion;
    [SerializeField] private CameraShake mainCamera;

    [SerializeField]int hp;
    public bool isShot;
    float coolTimer;

    Color color;
    Color startColor;
    Color endColor;

    // Start is called before the first frame update
    void Start()
    {

        hp = 3;
        isShot = true;
        coolTimer = 0;

        color = gameObject.GetComponent<SpriteRenderer>().color;

        startColor=color;
        endColor = Color.red;



    }

    // Update is called once per frame
    void Update()
    {
        coolTimer-=Time.deltaTime;

        if (hp <= 0)
        {
            Instantiate(towerExplosion,transform.position, Quaternion.identity);
            mainCamera.ShakeStart(1.0f, -0.8f, 0.8f);
            Destroy(gameObject);
        }

       
        if (coolTimer <= 0) {
            isShot = true;

        }
        else
        {
            isShot=false;
            //F•Ï‚¦‚é‚æ
            color = Color.Lerp(endColor, startColor, coolTimer / 3.0f);

        }

        if (!isShot)
        {
            
        }

    }

    public void Damage()
    {
        hp--;
    }

    public void Shot(Vector3 mousePos,GameObject cursor)
    {
        GameObject c = cursor;

        Bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetVector(transform.position, mousePos,c);
        coolTimer = 3.0f;

        
    }

}
