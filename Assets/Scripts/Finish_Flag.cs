using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Flag : MonoBehaviour
{
    public GameObject player1WinScreen;
    public GameObject player2WinScreen;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_1")
        {
            player1WinScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        
        if (collision.tag == "Player_2")
        {
            player2WinScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
