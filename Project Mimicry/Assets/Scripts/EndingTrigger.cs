using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] Animator anim1;
    [SerializeField] Animator anim2;
    [SerializeField] FirstPersonMovement player;

    [SerializeField] GameManager GM;

    [SerializeField] float waitingTime;

    private void OnTriggerEnter(Collider other)
    {
        anim1.enabled = true;
        anim2.enabled = true;

        player.enabled = false;

        StartCoroutine(waitTime(waitingTime));
    }

    private IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);

        GM.startTransition = true;
    }
}
