using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamTrigger : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] CinemachineVirtualCamera vcamPrior;
    [SerializeField] CinemachineVirtualCamera vcam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vcamPrior.enabled = true;
            vcam.enabled = false;
            StartCoroutine("calculateInputValues");
        }
    }

    private IEnumerator calculateInputValues()
    {
        yield return new WaitForSeconds(0.75f);
        player.calculateInputFromCamera();
    }
}
