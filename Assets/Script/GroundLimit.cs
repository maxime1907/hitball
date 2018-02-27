using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GroundLimit : MonoBehaviour {

    public GameObject Player;

    private float diff;
    private float save = 0f;
    private float saveMain = 0f;

    // Use this for initialization
    void Start () {
        diff = this.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (saveMain < Player.transform.position.y && this.name == "Main Camera")
        {
            this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y, this.transform.position.z);
            saveMain = Player.transform.position.y;
        }

        if (save < Player.transform.position.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y + diff, this.transform.position.z);
            save = Player.transform.position.y ;
        }
    }

    IEnumerator LoadLevel(string LevelName, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(LevelName);
    }
}
