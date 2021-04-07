using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject characterDummy;
    [SerializeField] float blinkTime;

    private bool alreadyBlinked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyBlinked)
        {
            StartCoroutine("blink");
        }
    }

    private IEnumerator blink()
    {
        alreadyBlinked = true;

        yield return new WaitForSeconds(blinkTime);
        blackPanel.SetActive(true);
        characterDummy.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        blackPanel.SetActive(false);    
    }
}
