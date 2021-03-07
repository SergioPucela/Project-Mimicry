using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCube : MonoBehaviour
{
    public bool isCubeIgnited;
    [SerializeField] LineRenderer LaserPrefab;
    [SerializeField] float laserRange;

    public List<GameObject> LaserOrigins = new List<GameObject>();

    //Debug
    bool isTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCubeIgnited && !isTrigger)
        {
            generateAllLasers();
            print("LASERS EVERYWHERE");

            //Debug
            isTrigger = true;
        }
    }

    private void generateAllLasers()
    {
        foreach(GameObject laserDisplay in LaserOrigins) //TO DO: MAKE THIS WORK WITH OBJECT POOL
        {
            LineRenderer laser = Instantiate(LaserPrefab);
            laser.SetPosition(0, laserDisplay.transform.position);
            laser.SetPosition(1, laserDisplay.transform.forward * laserRange + laserDisplay.transform.position);
        }
    }
}
