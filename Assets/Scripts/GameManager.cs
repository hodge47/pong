using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerOneScore = 0;
    public int playerTwoScore = 0;

    private Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        // Get the ball
        ball = GameManager.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
