using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

    public GameObject Player;

    private float diff;
    private Vector3 save;

    // Use this for initialization
    void Start () {
        diff = this.transform.position.y - Player.transform.position.y;
        save = Player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (save != Player.transform.position)
            this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y + diff, this.transform.position.z);
    }
}
