using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    GeneratorManager generatorManager;

    // Start is called before the first frame update
    void Start()
    {
        generatorManager = GetComponentInParent<GeneratorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectObstacle"))
        {
            generatorManager.laserCube = other.GetComponent<LaserCube>();

            generatorManager.laserCube.isCubeIgnited = true;

            generatorManager.setFans(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ObjectObstacle"))
        {
            generatorManager.setFans(false);

            generatorManager.laserCube.isCubeIgnited = false;
        }
    }
}
