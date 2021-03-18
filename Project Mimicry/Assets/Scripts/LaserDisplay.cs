using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDisplay : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float viewRange;
    public bool isReflecting;

    private LaserCube parentCube;

    // Start is called before the first frame update
    void Awake()
    {
        parentCube = GetComponentInParent<LaserCube>();
    }

    // Update is called once per frame
    void Update()
    {
        checkReflect();
    }

    private void checkReflect()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, viewRange, layerMask) && isReflecting)
        {
            if (!hit.transform.gameObject.CompareTag("DisplayLaser"))
            {
                isReflecting = false;
                parentCube.isReflecting = false;
            }
            else
            {
                LaserCube hitParentCube = hit.transform.gameObject.GetComponent<LaserDisplay>().parentCube;

                if (!hitParentCube.isReflecting && !hitParentCube.isCubeIgnited)
                {
                    isReflecting = false;
                    parentCube.isReflecting = false;
                }
            }
        }
    }
}
