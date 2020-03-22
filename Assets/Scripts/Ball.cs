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
    private float moveIncrement = 100f;

    private RectTransform rectTransform;
    private float ballSize;
    [SerializeField]
    private Canvas pongCourt;

    private float currentDirectionX = 1;
    private float currentDirectionY = 0;

    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court canvas
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        // Get the rect transform
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        // Get the ball size
        ballSize = rectTransform.sizeDelta.x;
        // Choose the start direction of the ball
        ChooseStartDirection();
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
    }

    private void KeepBallInBounds()
    {
        Vector2 _currentPosition = rectTransform.anchoredPosition;
        // Horizontal Axis
        if (_currentPosition.x - (ballSize * 3) <= -(pongCourt.pixelRect.width / 2) || _currentPosition.x + (ballSize * 3) >= (pongCourt.pixelRect.width / 2))
        {
            currentDirectionX *= -1;
        }
        // Vertical Axis
        if (_currentPosition.y + (ballSize * 2) > (pongCourt.pixelRect.height / 2) || _currentPosition.y - (ballSize * 2) < -(pongCourt.pixelRect.height / 2))
        {
            currentDirectionY *= -1;
        }
    }

    private void MoveBall()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x + (moveIncrement * currentDirectionX), rectTransform.anchoredPosition.y + (moveIncrement * currentDirectionY)), Time.deltaTime * speed);
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
        }
    }
}
