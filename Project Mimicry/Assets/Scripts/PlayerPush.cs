using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] float rangeToPush;
    Animator anim;
    Vector3 offset = new Vector3(0f, 1f, 0f);
    RaycastHit hitInfo;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position + offset, transform.forward, out hitInfo, rangeToPush))
        {
            if (hitInfo.collider.CompareTag("PushableObject"))
            {
                Vector3 reflect = -Vector3.Reflect(transform.forward, hitInfo.normal);
                if(Vector3.Dot(transform.forward, reflect) > 0.98f)
                {
                    print("PUSH");
                    anim.SetBool("isPushing", true);
                }
            }
        }
        else
        {
            if (anim.GetBool("isPushing"))
            {
                anim.SetBool("isPushing", false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + offset, transform.position + offset + transform.forward * rangeToPush);
    }
}
