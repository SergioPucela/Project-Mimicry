using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerControl;
    [SerializeField] GameObject levelsShortcut;

    [SerializeField] List<LaserCube> winConditions = new List<LaserCube>();
    [SerializeField] List<LaserCube> loseConditions = new List<LaserCube>();

    [SerializeField] Animator transitionAnim;

    [Header("First Person Scenes ONLY")]
    [SerializeField] bool firstPersonScene;

    [HideInInspector] public bool startTransition;

    // Start is called before the first frame update
    void Awake()
    {
        if(playerControl != null) StartCoroutine("disablePlayerControl");
        if (!firstPersonScene) Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && levelsShortcut != null) //Display or hide the levels shortcuts
        {
            if (levelsShortcut.activeSelf)
            {
                levelsShortcut.SetActive(false);
                Cursor.visible = false;
            }
            else
            {
                levelsShortcut.SetActive(true);
                Cursor.visible = true;
            }
        }

        if (!firstPersonScene)
        {
            if (checkWinCons())
            {
                print("YOU WIN!");
                StartCoroutine("loadNextLevel");
            }
        }
        else if(startTransition)
        {
            instaLoadNextLevel();
            startTransition = false;
        }
    }

    private bool checkWinCons()
    {
        foreach(LaserCube cube in winConditions)
        {
            if (!cube.isReflecting) return false;
        }

        foreach (LaserCube cube in loseConditions)
        {
            if (cube.isReflecting) return false;
        }

        return true;
    }

    private IEnumerator disablePlayerControl()
    {
        playerControl.enabled = false;

        yield return new WaitForSeconds(1f);

        playerControl.enabled = true;
    }

    private void instaLoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            print("GAME ENDED");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator loadNextLevel()
    {
        transitionAnim.SetTrigger("endScene");
        yield return new WaitForSeconds(2f);
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            print("GAME ENDED");
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadLvl1()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level1_1");
    }
    public void LoadLvl2()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level1_2");
    }
    public void LoadLvl3()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level1_3");
    }
    public void LoadLvl4()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level2_1");
    }
    public void LoadLvl5()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level2_2");
    }
    public void LoadLvl6()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level2_3");
    }
    public void LoadLvl7()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level3_1");
    }
    public void LoadLvl8()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level3_2");
    }
    public void LoadLvl9()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        SceneManager.LoadScene("Level3_3");
    }
}
