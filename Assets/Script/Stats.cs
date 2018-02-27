using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Stats : MonoBehaviour {

    public Text highscoreText;
    public Text timeText;
    public Text starsText;

    // Use this for initialization
    void Start()
    {
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
        int playedHours = (PlayerPrefs.GetInt("TotalTime") / 60) / 60;
        int playedMinutes = (PlayerPrefs.GetInt("TotalTime") / 60);
        timeText.text = playedHours.ToString() + ":" + playedMinutes.ToString();
        starsText.text = PlayerPrefs.GetInt("Stars").ToString();
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(LevelName);
    }

    public void GoHome()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("Menu", fadeTime));
    }
}
