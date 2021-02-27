using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{

    Animator anim;
    [SerializeField] float clickRange;
    [SerializeField] GameObject prefabTest;
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 1));

        transform.position = newPos;

        if (Input.GetButtonDown("Click")){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, layerMask))
            {
                Instantiate(prefabTest, hit.point, Quaternion.identity);
                anim.SetBool("click", true);
            }
        }
    }

    public void SetAnimBoolToFalse()
    {
        anim.SetBool("click", false);
    }
}
