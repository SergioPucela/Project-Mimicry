﻿using System.Collections;
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

    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask obstaclesLayerMask;
    [SerializeField] float interactRange;
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
            pushObject();
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
        if (Physics.Raycast(transform.position + offset, transform.forward, out hit, interactRange, layerMask)) //El objeto que quiero empujar es PushableObject?
        {
            if (Vector3.Dot(transform.forward, -hit.normal) > 0.98f) //Estoy mirando al PushableObject?
            {
                cubeToPush = hit.collider.gameObject;
                cubeToPushPos = cubeToPush.transform.position;

                directionToPush = -hit.normal;

                if (Physics.OverlapSphere(directionToPush * pushDistance + cubeToPushPos, 0.5f, obstaclesLayerMask).Length == 0) //No hay nada detrás del PushableObject que me impida empujarlo?
                {
                    playerControl.animatorSpeed = 0f;
                    playerControl.moveSpeed = 0f;
                    playerControl.enabled = false;

                    anim.SetBool("isPushing", true);
                }
            }
        }
    }

    private void pushObject()
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
