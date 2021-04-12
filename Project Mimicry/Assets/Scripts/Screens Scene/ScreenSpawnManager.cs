using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens = new List<GameObject>();
    [SerializeField] List<GameObject> screensToDisable = new List<GameObject>();

    [Header("Control the delay spawn")]
    [SerializeField] bool isDelayed;
    [SerializeField] float displayDelay;

    private bool alreadyTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject GO in screens)
            {
                StartCoroutine(spawnScreen(GO));
            }

            if(screensToDisable.Count > 0 && !isDelayed)
            {
                foreach (GameObject GO in screensToDisable)
                {
                    GO.SetActive(false);
                }
            }
        }
    }

    private IEnumerator spawnScreen(GameObject screen)
    {
        if(isDelayed)
        {
            yield return new WaitForSeconds(displayDelay);
            if (screensToDisable.Count > 0)
            {
                foreach (GameObject GO in screensToDisable)
                {
                    GO.SetActive(false);
                }
            }
            screen.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.15f, 0.75f));
            screen.SetActive(true);
        }
    }
}
