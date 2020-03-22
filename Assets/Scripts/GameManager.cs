using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerUI;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    private Ball ball;
    private RectTransform ballRectTransform;
    private float ballSize = 0f;

    private Text playerOneScoreText;
    private Text playerTwoScoreText;
    private Text volleyCountText;

    private Canvas pongCourt;

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        // Instantiate the player UI
        GameObject _playerUI = Instantiate(playerUI, pongCourt.gameObject.transform);
        playerOneScoreText = _playerUI.transform.GetChild(0).GetComponent<Text>();
        playerTwoScoreText = _playerUI.transform.GetChild(1).GetComponent<Text>();
        volleyCountText = _playerUI.transform.GetChild(2).GetComponent<Text>();
        playerOneScoreText.text = "0";
        playerTwoScoreText.text = "0";
        volleyCountText.text = "Volley: 0";
        // Get the ball
        ball = GameManager.FindObjectOfType<Ball>();
        ballRectTransform = ball.GetComponent<RectTransform>();
        ballSize = ballRectTransform.sizeDelta.x;
    }

    void FixedUpdate()
    {
        CheckForScore();
        UpdatePlayerUI();
    }

    private void CheckForScore()
    {
        // Player 1 scores
        Vector2 _currentPosition = ballRectTransform.anchoredPosition;
        if(_currentPosition.x + (ballSize / 2) >= (pongCourt.pixelRect.width / 2))
        {
            playerOneScore += 1;
            ball.ResetBall();
        }
        // Player 2 scores
        if (_currentPosition.x - (ballSize / 2) <= -(pongCourt.pixelRect.width / 2))
        {
            playerTwoScore += 1;
            ball.ResetBall();
        }
    }

    private void UpdatePlayerUI()
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
        volleyCountText.text = $"Volley: {ball.volleyCount}";
    }
}
