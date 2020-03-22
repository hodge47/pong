using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { ONE, TWO}

public class Paddle : MonoBehaviour
{
    public PlayerNumber playerNumber = PlayerNumber.ONE;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float moveIncrement = 1f;

    private Canvas pongCourt;
    private RectTransform rectTransform;
    private float paddleSizeY;

    // Start is called before the first frame update
    void Start()
    {
        // Get the pong court canvas
        pongCourt = GameObject.FindObjectOfType<Canvas>();
        //Debug.Log($"Court size: {pongCourt.pixelRect.width}x{pongCourt.pixelRect.height}");
        // Get the paddle rect
        rectTransform = this.gameObject.GetComponent<RectTransform>();
        // Get the size of the paddle
        paddleSizeY = rectTransform.sizeDelta.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        Vector2 _currentPosition = rectTransform.anchoredPosition;
        KeyCode _upKeyCode = (playerNumber == PlayerNumber.ONE) ? KeyCode.W : KeyCode.UpArrow;
        KeyCode _downKeyCode = (playerNumber == PlayerNumber.ONE) ? KeyCode.S : KeyCode.DownArrow;

        if (Input.GetKey(_upKeyCode))
        {
            if (_currentPosition.y + (paddleSizeY / 2) < (pongCourt.pixelRect.height / 2))
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + moveIncrement), Time.deltaTime * speed);
        }
        else if (Input.GetKey(_downKeyCode))
        {
            if (_currentPosition.y - (paddleSizeY / 2) > -(pongCourt.pixelRect.height / 2))
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - moveIncrement), Time.deltaTime * speed);

        }
    }
}
