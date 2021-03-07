using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanManager : MonoBehaviour
{
    public bool fanIsON;
    public GameObject objectToPush;

    [SerializeField] float pushForce;
    [SerializeField] float lerpSpeed;

    [SerializeField] GameObject fanCollider;
    private Vector3 triggerPos;

    // Start is called before the first frame update
    void Start()
    {
        triggerPos = GetComponentInChildren<FanTrigger>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fanIsON && objectToPush != null)
        {
            pushObject(objectToPush);
        }
        else if(!fanIsON && fanCollider.activeSelf == true)
        {
            fanCollider.SetActive(false);
        }
    }

    private void pushObject(GameObject cubePush)
    {
        if (Vector3.Distance(cubePush.transform.position, transform.forward * pushForce + triggerPos) < 0.02f)
        {
            cubePush.transform.position = transform.forward * pushForce + triggerPos;
            objectToPush = null;
        }
        else
        {
            cubePush.transform.position = Vector3.Lerp(cubePush.transform.position, transform.forward * pushForce + triggerPos, lerpSpeed * Time.deltaTime);
        }
    }

    public void setFanCollider(bool colBool)
    {
        fanCollider.SetActive(colBool);
    }
}
