using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private float maxSpeed = 2500f;
    [SerializeField]
    private float moveIncrement = 100f;
    [SerializeField]
    private int volleySpeedIncrement = 10;
    [SerializeField]
    private bool isMainMenu = false;

    private RectTransform rectTransform;
    private float ballSize;
    private Canvas pongCourt;

    private float currentDirectionX = 1;
    private float currentDirectionY = 0;
    private float ballSpeed;

    private System.Random rand = new System.Random();

    [HideInInspector]
    public int volleyCount = 0;
    [HideInInspector]
    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court canvas
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        // Get the rect transform
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        // Get the ball size
        ballSize = rectTransform.sizeDelta.x;
        // Get the original ball speed so we can reset after failed volley
        ballSpeed = speed;
        // Choose the start direction of the ball
        ChooseStartDirection();
        // Make the ball move if main menu
        if (isMainMenu)
            canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KeepBallInBounds();
        MoveBall();
    }

    private void ChooseStartDirection()
    {
        float _rand = rand.Next(0, 2);
        float _direction = (_rand == 0) ? 1 : -1;
        currentDirectionX = _direction;
        if (isMainMenu)
            currentDirectionY = -_direction;
    }

    private void KeepBallInBounds()
    {
        Vector2 _currentPosition = rectTransform.anchoredPosition;
        // Horizontal Axis
        if (_currentPosition.x - (ballSize / 2) <= -(pongCourt.pixelRect.width / 2) || _currentPosition.x + (ballSize / 2) >= (pongCourt.pixelRect.width / 2))
        {
            currentDirectionX *= -1;
        }
        // Vertical Axis
        if (_currentPosition.y + (ballSize / 2) > (pongCourt.pixelRect.height / 2) || _currentPosition.y - (ballSize / 2) < -(pongCourt.pixelRect.height / 2))
        {
            currentDirectionY *= -1;
        }
    }

    private void MoveBall()
    {
        if (canMove)
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x + (moveIncrement * currentDirectionX), rectTransform.anchoredPosition.y + (moveIncrement * currentDirectionY)), Time.deltaTime * ballSpeed);
    }

    private float BallDeflection(Vector2 _ballPos, Vector2 _paddlePos, float _paddleHeight)
    {
        // Return a Y direction based on where the ball hit the paddle
        return (_ballPos.y - _paddlePos.y) / _paddleHeight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            currentDirectionX *= -1;
            currentDirectionY = BallDeflection(this.transform.position, collision.gameObject.transform.position, collision.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            // Increment the volley count
            volleyCount++;
            // Increase the ball speed if the volley count is high enough
            if (volleyCount % volleySpeedIncrement == 0 && ballSpeed < maxSpeed)
            {
                ballSpeed += ballSpeed / 2;
            }
        }
    }

    public void ResetBall()
    {
        // Choose a new random start direction when the ball is reset
        ChooseStartDirection();
        // Reset the ball to the center of the screen
        rectTransform.anchoredPosition = new Vector2(0, 0);
        // Reset the balls Y direction
        currentDirectionY = 0f;
        // Reset the ball speed
        ballSpeed = speed;
        // Reset the volley count
        volleyCount = 0;
    }
}
