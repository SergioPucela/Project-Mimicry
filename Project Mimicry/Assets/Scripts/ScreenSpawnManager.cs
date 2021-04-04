using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> screens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(GameObject GO in screens)
            {
                StartCoroutine(spawnScreen(GO));
            }
        }
    }

    private IEnumerator spawnScreen(GameObject screen)
    {
        yield return new WaitForSeconds(Random.Range(0.15f, 0.75f));
        screen.SetActive(true);
    }
}
