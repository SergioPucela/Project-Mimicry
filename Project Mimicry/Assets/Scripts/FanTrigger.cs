using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrigger : MonoBehaviour
{
    FanManager fanManager;

    // Start is called before the first frame update
    void Start()
    {
        fanManager = GetComponentInParent<FanManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectObstacle"))
        {
            fanManager.objectToPush = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ObjectObstacle"))
        {
            if (fanManager.fanIsON)
            {
                fanManager.setFanCollider(true);
            }
            else
            {
                fanManager.objectToPush = null;
            }
        }
    }
}
