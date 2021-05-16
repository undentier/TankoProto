using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSysteme : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deadMenu;
    public GameObject victoryMenu;
    public bool gameIsPause;

    private bool lockVictory;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseMenu();
        }

        if (LevelManager.instance.playerList.Count < 1)
        {
            deadMenu.SetActive(true);
        }
        else
        {
            deadMenu.SetActive(false);
        }
        
        if (End.victory && !lockVictory)
        {
            lockVictory = true;
            StartCoroutine(VictoryCoolDown());
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseMenu()
    {
        if (!gameIsPause)
        {
            gameIsPause = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            gameIsPause = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameIsPause = false;
    }

    public void ActiveVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }

    IEnumerator VictoryCoolDown()
    {
        yield return new WaitForSeconds(3f);
        ActiveVictoryMenu();
    }
}
