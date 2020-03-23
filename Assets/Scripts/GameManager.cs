using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerUIPrefab;

    public int playTo = 5;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    private string winner = "None";

    private Ball ball;
    private RectTransform ballRectTransform;
    private float ballSize = 0f;

    private Text playerOneScoreText;
    private Text playerTwoScoreText;
    private Text volleyCountText;
    private Text countdownTimerText;
    private Text playerOneNameText;
    private Text playerTwoNameText;
    private float countdownTimeLeft = 3f;
    private bool countdownTimerActive = true;
    private GameObject playerUI;

    private Canvas pongCourt;

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        // Instantiate the player UI
        playerUI = Instantiate(playerUIPrefab, pongCourt.gameObject.transform);
        PlayerUI _uiElements = playerUI.GetComponent<PlayerUI>();
        playerOneScoreText = _uiElements.playerOneScoreText;
        playerTwoScoreText = _uiElements.playerTwoScoreText;
        volleyCountText = _uiElements.volleyCountText;
        countdownTimerText = _uiElements.countdownTimerText;
        playerOneNameText = _uiElements.playerOneNameText;
        playerTwoNameText = _uiElements.playerTwoNameText;
        playerOneScoreText.text = "0";
        playerTwoScoreText.text = "0";
        volleyCountText.text = "Volley: 0";
        playerOneNameText.text = "Player 1";
        var _paddles = GameObject.FindObjectsOfType<Paddle>();
        foreach (Paddle p in _paddles)
        {
            if (p.playerNumber == PlayerNumberEnum.TWO)
            {
                playerTwoNameText.text = "Player 2";
            }
            else if (p.playerNumber == PlayerNumberEnum.COMPUTER)
            {
                playerTwoNameText.text = "Computer";
            }
        }

        // Get the ball
        ball = GameManager.FindObjectOfType<Ball>();
        ballRectTransform = ball.GetComponent<RectTransform>();
        ballSize = ballRectTransform.sizeDelta.x;
    }

    void FixedUpdate()
    {
        if (countdownTimerActive)
        {
            CountdownTimer();
        }
        CheckForScore();
        UpdatePlayerUI();
    }

    private void CheckForScore()
    {
        if (playerOneScore < playTo && playerTwoScore < playTo)
        {
            // Player 1 scores
            Vector2 _currentPosition = ballRectTransform.anchoredPosition;
            if (_currentPosition.x + (ballSize / 2) >= (pongCourt.pixelRect.width / 2))
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
        else
        {
            winner = (playerOneScore >= playTo) ? "Player 1" : $"{playerTwoNameText.text}";
            playerUI.GetComponent<PlayerUI>().ShowWinMenu();
            playerUI.GetComponent<PlayerUI>().winnerText.text = $"{winner.ToUpper()} WINS!";
            ball.gameObject.SetActive(false);
        }

    }

    private void UpdatePlayerUI()
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
        volleyCountText.text = $"Volley: {ball.volleyCount}";
    }

    private void CountdownTimer()
    {
        countdownTimeLeft -= Time.deltaTime;
        countdownTimerText.text = $"{Mathf.RoundToInt(countdownTimeLeft)}";
        ball.canMove = false;
        ball.gameObject.SetActive(false);
        if (countdownTimeLeft < 1f)
        {
            countdownTimerActive = false;
            countdownTimeLeft = 3f;
            countdownTimerText.text = string.Empty;
            ball.gameObject.SetActive(true);
            ball.canMove = true;
        }
    }
}
