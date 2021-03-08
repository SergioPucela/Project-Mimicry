using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCube : MonoBehaviour
{
    public bool isCubeIgnited;

    [SerializeField] float updateFrequency;
    [SerializeField] LineRenderer LaserPrefab;
    [SerializeField] float laserRange;
    [SerializeField] LayerMask layerMask;

    private float timer = 0f;

    public List<GameObject> LaserOrigins = new List<GameObject>();

    private Queue<GameObject> lasers = new Queue<GameObject>();

    private ObjectPooler objectPooler;
    private GameObject currentLaser;
    private LineRenderer mLineRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCubeIgnited)
        {
            if(timer >= updateFrequency)
            {
                timer = 0f;
                while(lasers.Count != 0)
                {
                    lasers.Dequeue().SetActive(false);
                }
                generateAllLasers();
            }
            timer += Time.fixedDeltaTime;
        }
        else if(lasers.Count > 0)
        {
            while (lasers.Count != 0)
            {
                lasers.Dequeue().SetActive(false);
            }
        }
    }

    private void generateAllLasers()
    {
        foreach(GameObject laserDisplay in LaserOrigins)
        {
            RaycastHit hit;
            if (Physics.Raycast(laserDisplay.transform.position, laserDisplay.transform.forward, out hit, laserRange, layerMask))
            {
                createNewLaser(laserDisplay.transform.position, hit.point, laserDisplay.transform.rotation);

                if (hit.transform.gameObject.CompareTag("DisplayLaser"))
                {
                    LaserDisplay reflectLaser = hit.transform.gameObject.GetComponent<LaserDisplay>();
                    reflectLaser.isReflecting = true;
                }
            }

            /*
            LineRenderer laser = Instantiate(LaserPrefab);
            laser.SetPosition(0, laserDisplay.transform.position);
            laser.SetPosition(1, laserDisplay.transform.forward * laserRange + laserDisplay.transform.position); //Esta línea me puede servir para debug
            */
        }
    }

    /*
    public void reflectLasers(GameObject laserToAvoid)
    {
        foreach (GameObject laserDisplay in LaserOrigins)
        {
            if(laserDisplay != laserToAvoid)
            {
                RaycastHit hit;
                if (Physics.Raycast(laserDisplay.transform.position, laserDisplay.transform.forward, out hit, laserRange, layerMask))
                {
                    createNewLaser(laserDisplay.transform.position, hit.point, laserDisplay.transform.rotation);

                    if (hit.transform.gameObject.CompareTag("DisplayLaser"))
                    {
                        LaserDisplay reflectLaser = hit.transform.gameObject.GetComponent<LaserDisplay>();
                        reflectLaser.isReflecting = true;
                    }
                }
            }
        }
    }*/

    private void createNewLaser(Vector3 initPos, Vector3 endPos, Quaternion rotation)
    {
        currentLaser = objectPooler.SpawnFromPool("Laser", initPos, rotation);
        mLineRenderer = currentLaser.GetComponent<LineRenderer>();

        lasers.Enqueue(currentLaser);

        mLineRenderer.SetPosition(0, initPos);
        mLineRenderer.SetPosition(1, endPos);
    }
}
