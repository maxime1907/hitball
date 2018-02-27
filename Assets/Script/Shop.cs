using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

    public Text totalStars, costSkin;
    public GameObject shoppy;

    private string imageName, ButtonId;

    private int actualSkin, totalOfStars, cost, x;

    private int boughtSkin = -1;

    // Use this for initialization
    void Start () {
        if (totalStars)
            totalStars.text = PlayerPrefs.GetInt("Stars").ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (totalStars != null)
            totalStars.text = PlayerPrefs.GetInt("Stars").ToString();
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

    public void ChangeSkin()
    {
        actualSkin = shoppy.GetComponent<SliderMenu>().choosenSkin;
        totalOfStars = PlayerPrefs.GetInt("Stars");
        ButtonId = EventSystem.current.currentSelectedGameObject.name;

        ButtonId = ButtonId.Substring(ButtonId.Length - 2);

        x = int.Parse(ButtonId);

        if (PlayerPrefs.GetInt(ButtonId) == 0 || PlayerPrefs.GetInt(ButtonId) == -1)
        {
            boughtSkin = -1;
            if (x == actualSkin)
                cost = int.Parse(costSkin.text);
            else
                cost = -1;
        }
        else
        {
            boughtSkin = 1;
            cost = -1;
        }

        if (x == actualSkin && totalOfStars >= cost)
        {
            x++;
            if (x < 10)
                imageName = "0" + x.ToString();
            else
                imageName = x.ToString();

            if (boughtSkin == -1)
            {
                PlayerPrefs.SetInt("Stars", totalOfStars - cost);
                PlayerPrefs.SetInt(ButtonId, 1);
                PlayerPrefs.SetString("Skin", imageName);
            }
            else
            {
                PlayerPrefs.SetString("Skin", imageName);
            }
        }
    }

}
