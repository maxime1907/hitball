using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject prefab, prefabYellow, prefabPurple, prefabGreen;
    public AudioClip Jump, Coin, over;
    public Vector3 startPosition;
    public Text scoreText;

    public float speedEnnemies, jumpVelocity;

    public int Highscore, Stars, TotalTime, score;

    public bool start = false;
    public bool IsTouched = false;

    private SpriteRenderer Skin;
    private EdgeCollision Edge;
    private AudioSource source;
    private Vector3 offset;
    private Sprite tmpSkin;
    private Touch touch;

    private float width, height, playedTime, xpos;

    private string skinName = "player";

    private bool alive = true;

    // Use this for initialization
    void Start()
    {
        this.transform.position = startPosition;

        DontDestroyOnLoad(this);

        Edge = this.GetComponent<EdgeCollision>();
        source = GetComponent<AudioSource>();
        Skin = GetComponent<SpriteRenderer>();

        Highscore = PlayerPrefs.GetInt("Highscore");
        Stars = PlayerPrefs.GetInt("Stars");
        skinName = PlayerPrefs.GetString("Skin");

        tmpSkin = Resources.Load<Sprite>(skinName);
        if (tmpSkin != null)
            Skin.sprite = tmpSkin;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive == true)
        {
            if (start == false)
            {
                if (((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space)) && IsTouched == false)
                {
                    if (Random.Range(-10, 10) > 0)
                        xpos = 0.09f;
                    else
                        xpos = -0.09f;

                    source.PlayOneShot(Jump, 1);
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpVelocity, 0);
                    start = true;
                }
                else
                {
                    this.transform.position = startPosition;
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                }
            }

            if (start == true)
            {
                playedTime += Time.deltaTime;

                if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (xpos > 0)
                        xpos = -0.09f;
                    else
                        xpos = 0.09f;

                    source.PlayOneShot(Jump, 1);
                    this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpVelocity, 0);
                }

                width = Edge.width - (this.transform.localScale.x);

                height = (Edge.height * -1) + (this.transform.localScale.y * 0.5f);

                if (this.transform.position.y < height)
                    youDie();

                if (this.transform.position.x + (this.transform.localScale.x) > width)
                    xpos = -0.09f;
                else if (this.transform.position.x - (this.transform.localScale.x) < (width * -1))
                    xpos = 0.09f;

                if (xpos > 0f)
                    transform.Rotate(0, 0, -20);
                else
                    transform.Rotate(0, 0, 20);

                this.transform.position = new Vector3(this.transform.position.x + xpos, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    public void HomeMenu()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("Menu", fadeTime));
    }

    void youDie()
    {
        alive = false;
        source = GetComponent<AudioSource>();
        source.PlayOneShot(over, 1);
        Stars += score;
        PlayerPrefs.SetInt("Stars", Stars);
        if (score > Highscore)
        {
            Highscore = score;
            PlayerPrefs.SetInt("Highscore", Highscore);
        }
        playedTime += PlayerPrefs.GetInt("TotalTime");
        PlayerPrefs.SetInt("TotalTime", (int)playedTime);
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("Over", fadeTime));
    }

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(LevelName);
        if (LevelName == "Menu")
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Star")
        {
            source.PlayOneShot(Coin, 1);
            Destroy(other.gameObject);
            score++;
            scoreText.text = score.ToString();
        }
        else if (other.tag == "DetectorY")
        {
            Instantiate(prefabYellow, new Vector3(2.53f, other.transform.position.y + 19f, other.transform.position.z), Quaternion.identity);
            Instantiate(prefab, new Vector3(2.53f, other.transform.position.y + 23f, other.transform.position.z), Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "DetectorP")
        {
            Instantiate(prefabPurple, new Vector3(-2.2f, other.transform.position.y + 19f, other.transform.position.z), Quaternion.identity);
            Instantiate(prefab, new Vector3(-2.2f, other.transform.position.y + 23f, other.transform.position.z), Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "DetectorG")
        {
            Instantiate(prefabGreen, new Vector3(2.53f, other.transform.position.y + 19f, other.transform.position.z), Quaternion.identity);
            Instantiate(prefab, new Vector3(2.53f, other.transform.position.y + 23f, other.transform.position.z), Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Bar")
        {
            if (alive == true)
            {
                youDie();
            }
        }
    }
}
