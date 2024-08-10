using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float laneDistance = 2f;
    [SerializeField] int targetLane = 1;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float laneChangeSpeed = 5f;

    [Header("UI")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI cointext;

    private int coins;
    Vector2 startPos, endPos;
    bool isSwipeDetected = false;

    private void Update()
    {
        cointext.text = "Coins - " + coins;

        HandleMouseInput();

        Vector3 targetPos = new Vector3((targetLane * laneDistance - laneDistance) * 2, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * laneChangeSpeed);
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isSwipeDetected = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            Vector2 swipeDelta = endPos - startPos;

            if (swipeDelta.magnitude > 50) // Minimum swipe distance
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
                {
                    if (swipeDelta.x > 0 && targetLane < 2)
                    {
                        targetLane++; // Swipe right
                    }
                    else if (swipeDelta.x < 0 && targetLane > 0)
                    {
                        targetLane--; // Swipe left
                    }
                }
                isSwipeDetected = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coins += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }
    }
}