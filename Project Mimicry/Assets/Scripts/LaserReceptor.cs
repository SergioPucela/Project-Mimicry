using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceptor : MonoBehaviour
{
    [SerializeField] List<LaserCube> cubesToTrigger = new List<LaserCube>();
    [SerializeField] List<LaserCube> inhibitorCubes = new List<LaserCube>();

    private LaserCube parentCube;

    //This booleans will make the setCubes function be called just one time (= more efficient)
    private bool activated;
    private bool deactivated = true;

    // Start is called before the first frame update
    void Awake()
    {
        parentCube = GetComponent<LaserCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentCube.isReflecting)
        {
            if (checkInhibitors())
            {
                if (activated) setCubes(true);
                deactivated = true;
                activated = false;
            }
            else if (deactivated)
            {
                setCubes(false);
                deactivated = false;
                activated = true;
            }
        }
        else
        {
            if (deactivated) setCubes(false);
            deactivated = false;
            activated = true;
        }
    }

    private void setCubes(bool isIgnited)
    {
        foreach(LaserCube cube in cubesToTrigger)
        {
            cube.isCubeIgnited = isIgnited;
        }
    }

    private bool checkInhibitors()
    {
        if (inhibitorCubes.Count <= 0) return true;

        foreach(LaserCube cube in inhibitorCubes)
        {
            if (cube.isReflecting) return false;
        }
        return true;
    }
}
