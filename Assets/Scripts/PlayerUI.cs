using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{

    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    public Text volleyCountText;
    public Text countdownTimerText;
    public Text playerOneNameText;
    public Text playerTwoNameText;
    public GameObject winMenu;
    public Text winnerText;

    private void Start()
    {
        winMenu.SetActive(false);
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void RestartMatch()
    {
        int _thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_thisScene);
    }
}
