﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<LaserCube> winConditions = new List<LaserCube>();
    [SerializeField] PlayerController playerControl;

    // Start is called before the first frame update
    void Awake()
    {
        if(playerControl != null) StartCoroutine("disablePlayerControl");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkWinCons())
        {
            print("YOU WIN!");
        }
    }

    private bool checkWinCons()
    {
        foreach(LaserCube cube in winConditions)
        {
            if (!cube.isReflecting) return false;
        }
        return true;
    }

    private IEnumerator disablePlayerControl()
    {
        playerControl.enabled = false;

        yield return new WaitForSeconds(0.25f);

        playerControl.enabled = true;
    }
}
