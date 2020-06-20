using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BolaMove : MonoBehaviour{
    public int force = 200;
    public int maxCombo = 2;
    int maxScore = 5;
    int scoreP1 = 0;
    int scoreP2 = 0;
    float combo = 0;
    bool paused = false;
    bool display = false;
    AudioSource mh1;
    AudioSource mh2;
    AudioSource mh3;
    AudioSource mh4;
    ParticleSystem particleObject;
    GameObject panelFinish;
    GameObject panelPause;
    Text scoreUIP1;
    Text scoreUIP2;
    Text nameUIP1;
    Text nameUIP2;
    Rigidbody2D rbody;
    Vector2 pos;

    void Awake(){
        maxScore = StaticValues.MAXSCORE;

        particleObject = GetComponent<ParticleSystem>();
        particleObject.Stop();

        mh1 = GameObject.Find("MH1").GetComponent<AudioSource>();
        mh2 = GameObject.Find("MH2").GetComponent<AudioSource>();
        mh3 = GameObject.Find("MH3").GetComponent<AudioSource>();
        mh4 = GameObject.Find("MH4").GetComponent<AudioSource>();

        scoreUIP1 = GameObject.Find("P1 Score").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("P2 Score").GetComponent<Text>();
        nameUIP1 = GameObject.Find("P1 Name").GetComponent<Text>();
        nameUIP2 = GameObject.Find("P2 Name").GetComponent<Text>();

        panelFinish = GameObject.Find("Game Over");
        panelPause = GameObject.Find("Pause Screen");

        rbody = GetComponent<Rigidbody2D>();

        panelFinish.SetActive(false);
        panelPause.SetActive(false);
        scoreUIP1.text = "";
        scoreUIP2.text = "";
        nameUIP1.text = StaticValues.P1NAME;
        nameUIP2.text = StaticValues.P2NAME;
        StartCoroutine(FlashName());

        PaddleMove P2 = GameObject.Find("PaddleKanan").GetComponent<PaddleMove>();
        P2.bot = StaticValues.P2ISBOT;

        BolaMulai("");
    }

    void Update(){
        if (!panelFinish.activeInHierarchy){    
            if (Input.GetKeyDown(KeyCode.Escape)){
                paused = !paused;
            }

            if (paused){
                Time.timeScale = 0;
                if (display) {
                    scoreUIP1.text = scoreP1 + "";
                    scoreUIP2.text = scoreP2 + "";
                    nameUIP1.text = StaticValues.P1NAME;
                    nameUIP2.text = StaticValues.P2NAME;
                    display = false;
                }
                panelPause.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R)){
                    GameObject.Find("Main Camera").GetComponent<ManageScenes>().StartGame();
                }
                if (Input.GetKeyDown(KeyCode.Q)){
                    GameObject.Find("Main Camera").GetComponent<ManageScenes>().MainMenu();
                }
            } else {
                Time.timeScale = 1;
                if (!display) {
                    scoreUIP1.text = "";
                    scoreUIP2.text = "";
                    nameUIP1.text = "";
                    nameUIP2.text = "";
                    display = true;
                }
                panelPause.SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "PaddleKiri" || collision.gameObject.name == "PaddleKanan"){
            mh1.Play();
            float angle = (transform.position.y - collision.transform.position.y) * 5f;
            Vector2 direction = new Vector2(rbody.velocity.x, angle).normalized;
            rbody.velocity = new Vector2(0, 0);
            rbody.AddForce(direction*force*(2+combo));
            if (combo < maxCombo){
                combo = combo + 0.25f;
            }
        }
        if (collision.gameObject.name == "BatasAtas" || collision.gameObject.name == "BatasBawah"){
            mh2.Play();
        }
        if (collision.gameObject.name == "BatasKiri"){
            StartCoroutine(BolaUlang("P1"));
        }
        if (collision.gameObject.name == "BatasKanan"){
            StartCoroutine(BolaUlang("P2"));
        }
        
        if (scoreP1 == maxScore || scoreP2 == maxScore){
            panelFinish.SetActive(true);
            Text labelPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
            if (scoreP1 > scoreP2){
                labelPemenang.text = StaticValues.P1NAME;
            } else {
                labelPemenang.text = StaticValues.P2NAME;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator FlashName(){
        display = true;
        yield return new WaitForSeconds(3f);
        display = false;
    }

    IEnumerator BolaUlang(string side){
        CircleCollider2D col = gameObject.GetComponent<CircleCollider2D>();
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        col.enabled = !col.enabled;
        sr.enabled = !sr.enabled;
        if (side == "P1"){
            mh3.Play();
            mh4.Play();
            scoreP2 = scoreP2 + 1;
        }
        if (side == "P2"){
            mh3.Play();
            mh4.Play();
            scoreP1 = scoreP1 + 1;
        }

        particleObject.Play();
        rbody.velocity = new Vector2(0,0);

        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";

        yield return new WaitForSeconds(0.5f);

        scoreUIP1.text = "";
        scoreUIP2.text = "";

        BolaReset();
        BolaMulai(side);
        col.enabled = !col.enabled;
        sr.enabled = !sr.enabled;
    }

    void BolaReset(){
        particleObject.Stop();
        transform.localPosition = new Vector2(0,0);
        rbody.velocity = new Vector2(0,0);
        combo = 0;
    }

    void BolaMulai(string state){
        Vector2 direction;
        if (state == "P1"){
            direction = new Vector2(-2, 0).normalized;
        } else if (state == "P2"){
            direction = new Vector2(2, 0).normalized;
        } else {
            int rand = 0;
            while (rand == 0){
                rand = Random.Range(-1, 1);
            }
            direction = new Vector2(rand*2, 0).normalized;
        }
        rbody.AddForce(direction*force);
    }
}
