using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Play : MonoBehaviour
{
    public GameObject Ambient;

    private GameObject Tmp;
    
    // Use this for initialization
    void Start()
    {
        Tmp = GameObject.FindGameObjectWithTag("Ambient");
        if (!Tmp)
        {
            Instantiate(Ambient, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

    }

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(LevelName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayNow()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("TheGame", fadeTime));
    }
    public void LoadShop()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("Shop", fadeTime));
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store");
    }
    public void LoadStats()
    {
        float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
        StartCoroutine(LoadLevel("Stats", fadeTime));
    }

    private IEnumerator WaitThoseSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

}
