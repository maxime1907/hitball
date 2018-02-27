using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {


    public GameObject MainCanvas, ExitCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            MainCanvas.SetActive(false);
            ExitCanvas.SetActive(true);
        }

    }

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (LevelName == "Exit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(LevelName);
        }
        if (LevelName == "Menu")
            Destroy(this.gameObject);
    }

    public void ExitGame()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Yes")
        {
            float fadeTime = GameObject.Find("Fader").GetComponent<Fading>().BeginFade(1);
            StartCoroutine(LoadLevel("Exit", fadeTime));
        }
        else
        {
            MainCanvas.SetActive(true);
            ExitCanvas.SetActive(false);
        }
    }
}
