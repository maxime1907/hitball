using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    public GameObject MyPauseMenu;
    public AudioClip PauseMusic;

    private Player Player;
    private AudioSource source;

    // Use this for initialization
    void Start() {
		Player = GameObject.Find("Player").GetComponent<Player>();
        MyPauseMenu.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GamePause()
    {
        if (Player.start == false) {
            Player.start = true;
            Player.IsTouched = false;
            MyPauseMenu.SetActive(false);
        }
        else {
            Player.IsTouched = true;
			Player.startPosition = Player.transform.position;
            Player.start = false;
            Player.IsTouched = true;
            source.PlayOneShot(PauseMusic, 1);
            MyPauseMenu.SetActive(true);
        }
    }
}
