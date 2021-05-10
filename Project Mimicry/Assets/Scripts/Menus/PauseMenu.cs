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

    [SerializeField] ArmController arm;

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

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (arm != null)
        {
            Cursor.lockState = CursorLockMode.None;
            arm.enabled = true;
        }

        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        isPaused = true;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (arm != null) arm.enabled = false;

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
