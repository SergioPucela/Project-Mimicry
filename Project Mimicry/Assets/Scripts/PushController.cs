using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    Animator anim;
    Vector3 offset = new Vector3(0f, 1f, 0f);
    RaycastHit hit;

    //float timer = 0f;
    //[SerializeField] float timeToPush = 0.5f;

    bool cubeMoving = false;
    //bool isPushing = false;

    PlayerController playerControl;

    GameObject cubeToPush;
    Vector3 cubeToPushPos;
    Vector3 directionToPush;

    [SerializeField] float interactRange;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float pushDistance;
    [SerializeField] float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerControl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cubeMoving)
        {
            checkPushing();
        }
        else
        {
            //checkTimer();
            pushObject(cubeToPush);
        }


    }

    private void checkPushing()
    {
        //Vector3 vector = (transform.position + offset + transform.forward) - (transform.position + offset);
        if (Physics.Raycast(transform.position + offset, transform.forward, out hit, interactRange, layerMask))
        {
            //Debug.DrawRay(transform.position + offset, vector.normalized * interactRange, Color.yellow);
            if (Vector3.Dot(transform.forward, -hit.normal) > 0.98f)
            {
                //Debug.DrawRay(transform.position + offset, vector.normalized * interactRange, Color.green);
                //isPushing = true;

                playerControl.animatorSpeed = 0f;
                playerControl.moveSpeed = 0f;
                playerControl.enabled = false;

                cubeToPush = hit.collider.gameObject;
                cubeToPushPos = cubeToPush.transform.position;

                directionToPush = -hit.normal;

                anim.SetTrigger("isPushing");

                cubeMoving = true;
            }
        }
        else
        {
            //Debug.DrawRay(transform.position + offset, vector.normalized * interactRange, Color.red);
        }
    }

    private void pushObject(GameObject cubeToPush)
    {
        //cubeToPush.transform.Translate(directionToPush * pushDistance * Time.deltaTime, Space.World); 
        if(Vector3.Distance(cubeToPush.transform.position, directionToPush * pushDistance + cubeToPushPos) < 0.02f)
        {
            cubeToPush.transform.position = directionToPush * pushDistance + cubeToPushPos;
            cubeMoving = false;
            playerControl.enabled = true;
            //anim.SetBool("isPushing", false);
        }
        else
        {
            cubeToPush.transform.position = Vector3.Lerp(cubeToPush.transform.position, directionToPush * pushDistance + cubeToPushPos, lerpSpeed * Time.deltaTime);
        }
    }

    /*
    private void checkTimer()
    {
        timer += Time.deltaTime;
        if (timer > timeToPush)
        {
            timer = 0f;
            isPushing = false;
            playerControl.enabled = true;
            anim.SetBool("isPushing", false);
        }
    }
    */
}
