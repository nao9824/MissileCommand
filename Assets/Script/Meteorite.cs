using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Explosion explosion;
    [SerializeField] ParticleSystem particleSystems;

    private float min;
    private float max;

    Vector3 target;
    Vector3 direction;

    public float speed;
    public float speedMax;
    public float speedMin;



    // Start is called before the first frame update
    void Start()
    {

        speed = 0.0f;
        target = new Vector2(Random.Range(min, max), min);
        direction = target - transform.position;
        direction.Normalize();

        speedMax = 4.0f;
        speedMin = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;

        speed = Random.Range(speedMin, speedMax);

        transform.position += direction * speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))
        {
            //先に判定と描画切ってパーティクルだけ見えるように（生成は止める）
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            particleSystems.Stop();
            //一秒後Destroy
            Destroy(gameObject,1.0f);

            gameManager.Damage();
        }

        if (collision.CompareTag("ArtilleryBattery"))
        {
            //先に判定と描画切ってパーティクルだけ見えるように（生成は止める）
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            particleSystems.Stop();
            //一秒後Destroy
            Destroy(gameObject,1.0f);
            collision.GetComponent<ArtilleryBattery>().Damage();
        }

        if (collision.CompareTag("Explosion"))
        {
            
            //先に判定と描画切ってパーティクルだけ見えるように（生成は止める）
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            particleSystems.Stop();

            Instantiate(explosion, transform.position, Quaternion.identity);
            gameManager.MeteoExp();
            //一秒後Destroy
            Destroy(gameObject,1.0f);
        }
    }

    public void SetUp(SpriteRenderer spriteRenderer,GameManager gameManager_)
    {

        min = spriteRenderer.bounds.min.x;
        max = spriteRenderer.bounds.max.x;

        gameManager = gameManager_;
    }

}
