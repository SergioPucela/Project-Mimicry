﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Awake()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        anim.SetTrigger("endScene");
        StartCoroutine("startFirstScene");
    }

    public void QuitGame()
    {
        anim.SetTrigger("endScene");
        print("EXIT GAME");
        StartCoroutine("exitGame");
    }

    private IEnumerator startFirstScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("FlockingScene");
    }

    private IEnumerator exitGame()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
