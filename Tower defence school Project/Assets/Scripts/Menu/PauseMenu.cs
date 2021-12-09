using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject.Find("Player").GetComponent<CharacterAttack>().enabled = true;
        GameObject.Find("Player").GetComponent<Dash>().enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject.Find("Player").GetComponent<CharacterAttack>().enabled = false;
        GameObject.Find("Player").GetComponent<Dash>().enabled = false;
    }

    public void MainMenu()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;       
    }
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
