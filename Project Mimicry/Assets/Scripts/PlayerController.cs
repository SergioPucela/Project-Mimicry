using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] float smoothRotation = 0.1f;

    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    float animatorSpeed = 0f;
    float moveSpeed = 0f;

    Animator anim;

    Quaternion newRotation = Quaternion.identity;

    Vector3 forward, right;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0f, 90f, 0f)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("animSpeed", animatorSpeed);
            Move();
        }
        else if (moveSpeed > deceleration * Time.deltaTime)
        {
            moveSpeed -= deceleration * Time.deltaTime;
            anim.SetFloat("animSpeed", animatorSpeed);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (anim.GetBool("isWalking"))
        {
            moveSpeed = 0f;
            animatorSpeed = 0f;
            anim.SetBool("isWalking", false);
        }
        //print("Move Speed: " + moveSpeed + "// Dec * Time = " + deceleration * Time.deltaTime);
    }

    void Move()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0f, Input.GetAxis("VerticalKey"));

        Accelerate();

        Vector3 rightMovement = right * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        newRotation = Quaternion.LookRotation(heading);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, smoothRotation * Time.deltaTime);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    void Accelerate()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
            if (animatorSpeed <= 1f)
            {
                animatorSpeed = moveSpeed;
            }
        }
    }
}
