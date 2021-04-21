using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{

    private Animator anim;

    [SerializeField] float clickRange;
    [SerializeField] AudioSource tabSound;
    [SerializeField] LayerMask layerMask;

    private FleeTrigger fleeTrigger;

    [HideInInspector] public int NumClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fleeTrigger = tabSound.gameObject.GetComponent<FleeTrigger>();
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

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                NumClicks++;

                tabSound.gameObject.transform.position = new Vector3(hit.point.x, 2.25f, -7.35f);
                tabSound.Play();
                fleeTrigger.StartFlee();
                anim.SetBool("click", true);
            }
        }
    }

    public void SetAnimBoolToFalse()
    {
        anim.SetBool("click", false);
    }
}
