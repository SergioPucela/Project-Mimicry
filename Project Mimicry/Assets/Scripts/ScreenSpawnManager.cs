using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens = new List<GameObject>();
    [SerializeField] List<GameObject> screensToDisable = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject GO in screens)
            {
                StartCoroutine(spawnScreen(GO));
            }

            if(screensToDisable.Count > 0)
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
        yield return new WaitForSeconds(Random.Range(0.15f, 0.75f));
        screen.SetActive(true);
    }
}
