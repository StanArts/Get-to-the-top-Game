using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject player1, player2;

    public int P1Life;
    public int P2Life;
    int maxHealthValue = 3;

    public GameObject[] healthStars1;
    public GameObject[] healthStars2;

    float timeBeforeRespawn = 2f;
    bool isRespawning1;
    bool isRespawning2;

    public GameObject thePauseScreen;
    public string mainMenu;

    void Start()
    {
        P1Life = maxHealthValue;
        P2Life = maxHealthValue;
    }

    void Update()
    {
        if (P1Life <= 0 && !isRespawning1)
        {
            Respawn1();

            isRespawning1 = true;
            //player2WinScreen.SetActive(true);
        }

        if (P2Life <= 0 && !isRespawning2)
        {
            Respawn2();

            isRespawning2 = true;
            //player1WinScreen.SetActive(true);
        }

        PauseGame();
    }

    public void UpdateHearts1()
    {
        for (int i = 0; i < healthStars1.Length; i++)
        {
            if (P1Life > i)
            {
                healthStars1[i].SetActive(true);
            }
            else
            {
                healthStars1[i].SetActive(false);
            }
        }
    }

    public void UpdateHearts2()
    {
        for (int i = 0; i < healthStars2.Length; i++)
        {
            if (P2Life > i)
            {
                healthStars2[i].SetActive(true);
            }
            else
            {
                healthStars2[i].SetActive(false);
            }
        }
    }

    public void Respawn1()
    {
        StartCoroutine(RespawnP1Co());
    }

    public void Respawn2()
    {
        StartCoroutine(RespawnP2Co());
    }

    IEnumerator RespawnP1Co()
    {
        player1.SetActive(false);

        yield return new WaitForSeconds(timeBeforeRespawn);

        P1Life = maxHealthValue;

        isRespawning1 = false;
        UpdateHearts1();

        player1.transform.position = FindObjectOfType<Player_Controller>().respawnPosition;

        player1.SetActive(true);
    }

    IEnumerator RespawnP2Co()
    {
        player2.SetActive(false);

        yield return new WaitForSeconds(timeBeforeRespawn);

        P2Life = maxHealthValue;

        isRespawning2 = false;

        player2.transform.position = FindObjectOfType<Player_Controller>().respawnPosition;

        player2.SetActive(true);
    }

    public void HurtP1()
    {
        if (!FindObjectOfType<Player_Controller>().invincible)
        {
            P1Life -= 1;

            FindObjectOfType<Player_Controller>().KnockBack();

            UpdateHearts1();
        }
    }

    public void HurtP2()
    {
        if (!FindObjectOfType<Player_Controller>().invincible)
        {
            P2Life -= 1;

            FindObjectOfType<Player_Controller>().KnockBack();

            UpdateHearts2();
        }
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            thePauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}
