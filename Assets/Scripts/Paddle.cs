using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumberEnum { ONE, TWO, COMPUTER }

public class Paddle : MonoBehaviour
{
    public PlayerNumberEnum playerNumber = PlayerNumberEnum.ONE;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float moveIncrement = 1f;

    private Canvas pongCourt;
    private RectTransform rectTransform;
    private float paddleSizeY;
    private float paddleSpeed = 0;
    private System.Random rand = new System.Random();

    Ball ball;
    RectTransform ballRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court canvas
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        // Get the paddle rect
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        // Get the size of the paddle
        paddleSizeY = rectTransform.sizeDelta.y;
        // Set the paddle speed if this is AI player
        if (playerNumber == PlayerNumberEnum.COMPUTER)
        {
            paddleSpeed = speed / 2;
        }
        else
        {
            paddleSpeed = speed;
        }
        // Get ball information
        ball = GameObject.FindObjectOfType<Ball>();
        ballRectTransform = ball.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        Vector2 _currentPosition = rectTransform.anchoredPosition;

        if (playerNumber == PlayerNumberEnum.COMPUTER)
        {

            // Allow the computer to move if the ball is on its side of the court
            if (ballRectTransform.anchoredPosition.x > 0)
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, ball.gameObject.GetComponent<RectTransform>().anchoredPosition.y), Time.deltaTime * paddleSpeed);
            }
            // Move back to 0 if ball is on the opposite side of the court
            else
            {
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, 0), Time.deltaTime * paddleSpeed);
            }
        }
        else
        {
            KeyCode _upKeyCode = (playerNumber == PlayerNumberEnum.ONE) ? KeyCode.W : KeyCode.UpArrow;
            KeyCode _downKeyCode = (playerNumber == PlayerNumberEnum.ONE) ? KeyCode.S : KeyCode.DownArrow;

            if (Input.GetKey(_upKeyCode))
            {
                if (_currentPosition.y + (paddleSizeY / 2) < (pongCourt.pixelRect.height / 2))
                    rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + moveIncrement), Time.deltaTime * paddleSpeed);
            }
            else if (Input.GetKey(_downKeyCode))
            {
                if (_currentPosition.y - (paddleSizeY / 2) > -(pongCourt.pixelRect.height / 2))
                    rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - moveIncrement), Time.deltaTime * paddleSpeed);

            }
        }
    }
}
