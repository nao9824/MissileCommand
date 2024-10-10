using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Meteorite meteorite;
    [SerializeField] private List<Transform> meteoritePoint = new List<Transform>();
    [SerializeField] private List<ArtilleryBattery> artilleryBattery = new List<ArtilleryBattery>();
    [SerializeField] private SpriteRenderer groundSpriteRenderer;
    //[SerializeField] private SpriteRenderer imageSpriteRenderer;
    [SerializeField] private CameraShake mainCamera;
    [SerializeField] private Cursor cursor;
    [SerializeField] private Explosion explosion;
    [SerializeField] Camera viewCamera;

    /// <summary>
    /// meteo
    /// </summary>
    float timer;
    [SerializeField]float shotTimer;
    bool isShot;
    [SerializeField]int pointChoice;

    /// <summary>
    /// hp
    /// </summary>
    [SerializeField]int hp;
    public Image greenImage;
    public int maxHP;

    bool isAliveAB;

    //Score
    public int score;
    [SerializeField]private TMP_Text scoreText;

    //フェードアウト
    [SerializeField] Image fadePanel;             
    float fadeDuration = 0.8f;

    //リザルト
    [SerializeField] Canvas resultPanel;
    public int resultScore;
    [SerializeField] private TMP_Text resultScoreText;

    // Start is called before the first frame update
    void Start()
    {
        shotTimer = 0.0f;
        timer = 0.0f;
        isShot = false;
        pointChoice = 0;

        hp = 10;
        maxHP = hp;
        isAliveAB = true;

        score = 0;
        scoreText.text = "score:0";

        resultScore = 0;
        resultScoreText.text = "score:0";
    }

    // Update is called once per frame
    void Update()
    {

        
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane; // カメラからの距離を設定
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            
            ///弾撃つよ
            ArtilleryBattery canArtilleryBattery = Shootable();
            if (canArtilleryBattery != null)
            {
                //カーソル出すよ
                Cursor c = Instantiate(cursor, worldPosition, Quaternion.identity);
                canArtilleryBattery.Shot(worldPosition,c.gameObject);
                
            }
           
            
        }

        if (!isShot &&
            shotTimer <= 0)
        {
            //タワーが生き残ってたら
            for(int i = 0; i < 3; i++)
            {
                if (artilleryBattery[i] != null)
                {
                    isAliveAB = true;
                    
                    break;
                }
                else
                {
                    isAliveAB = false;
                }
            }

            if (isAliveAB) {
                shotTimer = 1.2f;
            }
            else
            {
                shotTimer = 0.5f;
                meteorite.speedMax = 6.0f;
                meteorite.speedMin = 4.0f;
            }
            
            pointChoice = Random.Range(0, 3);
            timer = 0.0f;
            
        }

        //TimerがshotTimer分経ったらtrue
        if (shotTimer <= timer)
        {
            isShot = true;
        }

        if (isShot) {

            Meteorite meteo = Instantiate(meteorite, meteoritePoint[pointChoice].transform.position, Quaternion.identity);
            meteo.SetUp(groundSpriteRenderer,this);
            

            isShot = false;
            shotTimer = 0.0f;
        }

        //スコア
        scoreText.text = "score:" + score.ToString("d8");
        resultScoreText.text = "score:" + resultScore.ToString("d8");

        //hpが0になったら爆発達出動
        if (hp <= 0)
        {
            //0.2秒に一発
            if (timer >= 0.2f)
            {
                GameOver();
            }
            //フェードアウト開始
            fadePanel.color=Color.Lerp(fadePanel.color, new Color(1, 1, 1f, 1),fadeDuration * Time.deltaTime);

            if (fadePanel.color == new Color(1, 1, 1f, 1))
            {
                resultPanel.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0)) {
                    SceneManager.LoadScene("SampleScene");
                }
            }
        }

    }

    public void Damage()
    {
        hp--;
        greenImage.fillAmount = (float)hp / maxHP;
        mainCamera.ShakeStart(0.5f,-0.5f,0.5f);
    }

    public void MeteoExp()
    {
        mainCamera.ShakeStart(0.2f, -0.3f, 0.3f);
        score += 100;
        resultScore += 100;
    }

    public void GameOver()
    {
       timer = 0.0f;
        Vector3 rand = new Vector3(Random.Range(viewCamera.transform.position.x - ((viewCamera.orthographicSize * viewCamera.aspect)), viewCamera.transform.position.x + ((viewCamera.orthographicSize * viewCamera.aspect))),
            Random.Range(viewCamera.transform.position.y - (viewCamera.orthographicSize), viewCamera.transform.position.y + (viewCamera.orthographicSize)), 0);


        Instantiate(explosion, rand, Quaternion.identity);

    }

    ArtilleryBattery Shootable()
    {

        foreach (ArtilleryBattery battery in artilleryBattery)
        {
            if (battery.isShot)
            {
                
                return battery;

            }

        }
        return null;
    }

   
}
