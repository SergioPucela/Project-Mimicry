using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamTrigger : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] CinemachineVirtualCamera vcamPrior;
    [SerializeField] List<CinemachineVirtualCamera> vcamsNonPrior = new List<CinemachineVirtualCamera>();

    private bool isTriggerActive;

    private void Update()
    {
        if (isTriggerActive)
        {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                isTriggerActive = false;
                player.calculateInputFromCamera();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggerActive = true;

        if (other.CompareTag("Player"))
        {
            vcamPrior.Priority = 10;

            foreach(CinemachineVirtualCamera vcam in vcamsNonPrior)
            {
                vcam.Priority = 5;
            }
        }
    }
}
