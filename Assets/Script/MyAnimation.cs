using UnityEngine;
using System.Collections;

public class MyAnimation : MonoBehaviour {

	private float changeSize = 0.006f;
    private Vector3 initSize;

	// Use this for initialization
	void Start () {
		initSize = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {

        if (this.transform.localScale.x > initSize.x + 0.18f)
            changeSize = -0.006f;
        else if (this.transform.localScale.x < initSize.x)
            changeSize = 0.006f;

        this.transform.localScale = new Vector3(this.transform.localScale.x + changeSize, this.transform.localScale.y + changeSize, this.transform.localScale.z);
    }
}
