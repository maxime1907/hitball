using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    private EdgeCollision Edge;
    private Player Player;

    private float xpos, height, width;
    private float speed = 0.03f;
    private float saveScore = 0;

    private bool shallMove = true;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();

        Edge = GameObject.Find("Player").GetComponent<EdgeCollision>();

        if (this.transform.position.x > 0)
            xpos = -1 * speed;
        else
            xpos = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.start == true)
        {

            if ((saveScore + 2) < Player.score && speed <= 0.09f)
            {
                if (this.transform.name == "Yellow" || this.transform.name == "Yellow(Clone)")
                {
                    saveScore = Player.score;
                    Player.speedEnnemies = Player.speedEnnemies + 0.003f;
                }
            }

            if (shallMove == true)
            {
                speed = Player.speedEnnemies;
                width = Edge.width - (this.transform.localScale.x);

                if (this.transform.position.x + (this.transform.localScale.x) > width)
                    xpos = -1 * speed;
                else if (this.transform.position.x - (this.transform.localScale.x) < (width * -1))
                    xpos = speed;

                this.transform.position = new Vector3(this.transform.position.x + xpos, this.transform.position.y, this.transform.position.z);
            }

            if (this.transform.position.y < (Camera.main.transform.position.y - Edge.height - (this.transform.localScale.y * 2)))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
