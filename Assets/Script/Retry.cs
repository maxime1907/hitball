using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Retry : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;

    private Player Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player").GetComponent<Player>();
        Player.start = false;
        scoreText.text = Player.score.ToString();
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        Player.GetComponent<Renderer>().enabled = false;
        Destroy(Player.gameObject);
    }

	// Update is called once per frame
	void Update () {
    }

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(LevelName);
    }

    public void RetryGame()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("TheGame", fadeTime));
    }
}
