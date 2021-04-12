using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    private bool quitting;

    [SerializeField] GameObject pausePanel;
    [SerializeField] Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !quitting)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;
        Cursor.visible = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        quitting = true;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        anim.SetTrigger("endScene");
        StartCoroutine("quit");
    }

    private IEnumerator quit()
    {
        yield return new WaitForSeconds(2f);
        print("Quitting game...");
        Application.Quit();
    }
}
