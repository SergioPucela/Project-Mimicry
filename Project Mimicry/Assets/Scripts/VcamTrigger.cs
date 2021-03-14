using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamTrigger : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] CinemachineVirtualCamera vcamPrior;
    [SerializeField] List<CinemachineVirtualCamera> vcamsNonPrior = new List<CinemachineVirtualCamera>();

    private bool isButtonDown;

    private void Update()
    {
        if (isButtonDown)
        {
            if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                isButtonDown = false;
                player.calculateInputFromCamera();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isButtonDown = true;

        if (other.CompareTag("Player"))
        {
            vcamPrior.enabled = true;

            foreach(CinemachineVirtualCamera vcam in vcamsNonPrior)
            {
                vcam.enabled = false;
            }
        }
    }
}
