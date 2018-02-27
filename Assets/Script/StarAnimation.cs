using UnityEngine;
using System.Collections;

public class StarAnimation : MonoBehaviour {

    private float changeSize = 0.01f;
    private Vector3 initSize;

    // Use this for initialization
    void Start () {
        initSize = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = new Vector3(this.transform.localScale.x + changeSize, this.transform.localScale.y + changeSize, this.transform.localScale.z);
        if (this.transform.localScale.x > initSize.x + 0.02f)
            changeSize = -0.001f;
        else if (this.transform.localScale.x < initSize.x)
            changeSize = 0.001f;
        transform.Rotate(0, 0, 200 * Time.deltaTime);
    }
}
