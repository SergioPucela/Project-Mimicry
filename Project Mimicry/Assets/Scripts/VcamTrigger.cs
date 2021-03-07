using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamTrigger : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] CinemachineVirtualCamera vcamPrior;
    [SerializeField] List<CinemachineVirtualCamera> vcamsNonPrior = new List<CinemachineVirtualCamera>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            vcamPrior.enabled = true;

            foreach(CinemachineVirtualCamera vcam in vcamsNonPrior)
            {
                vcam.enabled = false;
            }

            StartCoroutine("calculateInputValues");
        }
    }

    private IEnumerator calculateInputValues()
    {
        yield return new WaitForSeconds(0.9f);
        player.calculateInputFromCamera();
    }
}
