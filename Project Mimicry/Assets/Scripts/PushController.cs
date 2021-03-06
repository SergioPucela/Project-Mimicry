using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    Animator anim;
    Vector3 offset = new Vector3(0f, 1f, 0f);
    RaycastHit hit;

    bool cubeMoving = false;

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
            pushObject(cubeToPush);
        }

        //Fixes some animation issues
        if(anim.GetBool("isPushing") && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            anim.SetBool("isWalking", false);
        }
        else if(anim.GetBool("isPushing"))
        {
            anim.SetBool("isWalking", true);
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

                playerControl.animatorSpeed = 0f;
                playerControl.moveSpeed = 0f;
                playerControl.enabled = false;

                cubeToPush = hit.collider.gameObject;
                cubeToPushPos = cubeToPush.transform.position;

                directionToPush = -hit.normal;

                anim.SetBool("isPushing", true);
            }
        }
        else
        {
            //Debug.DrawRay(transform.position + offset, vector.normalized * interactRange, Color.red);
        }
    }

    private void pushObject(GameObject cubeToPush)
    {
        if(Vector3.Distance(cubeToPush.transform.position, directionToPush * pushDistance + cubeToPushPos) < 0.02f)
        {
            cubeToPush.transform.position = directionToPush * pushDistance + cubeToPushPos;
        }
        else
        {
            cubeToPush.transform.position = Vector3.Lerp(cubeToPush.transform.position, directionToPush * pushDistance + cubeToPushPos, lerpSpeed * Time.deltaTime);
        }
    }

    public void startPushObjectAnim()
    {
        cubeMoving = true;
    }

    public void stopPushObjectAnim()
    {
        cubeMoving = false;

        playerControl.enabled = true;
        playerControl.animatorSpeed = 0f;
        playerControl.moveSpeed = 0f;

        anim.SetBool("isPushing", false);
    }
}
