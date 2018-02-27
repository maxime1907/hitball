using UnityEngine;
using System.Collections;

public class EdgeCollision : MonoBehaviour {

    // Use this for initialization
    public float colDepth = 4f;
    public float zPosition = 0f;
    public float height, width;

    private Vector2 screenSize;
    private Vector3 cameraPos;

    // Use this for initialization
    void Start()
    {
        //Generate world space point information for position and scale calculations
        Camera cam = Camera.main;
        height = cam.orthographicSize;
        width = cam.orthographicSize * cam.aspect;

        screenSize.x = width;
        screenSize.y = height;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
