using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    private CharacterController controller;

    private float x;
    private float z;

    private Vector3 move;

    [SerializeField] private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * (x/2) + transform.forward * z; //Horizontal speed will be half of vertical speed

        controller.Move(move * speed * Time.deltaTime);
    }
}
