using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerControl;

    [SerializeField] List<LaserCube> winConditions = new List<LaserCube>();
    [SerializeField] List<LaserCube> loseConditions = new List<LaserCube>();

    // Start is called before the first frame update
    void Awake()
    {
        if(playerControl != null) StartCoroutine("disablePlayerControl");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            print("Reload level");
            reloadLevel();
        }
        if (checkWinCons())
        {
            print("YOU WIN!");
            StartCoroutine("loadNextLevel");
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

        yield return new WaitForSeconds(0.25f);

        playerControl.enabled = true;
    }

    private IEnumerator loadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
