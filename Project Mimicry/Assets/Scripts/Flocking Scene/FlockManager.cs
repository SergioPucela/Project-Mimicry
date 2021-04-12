using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFishes;
    public Vector3 swimLimits;
    public Vector3 goalPos;

    [Header("Fish settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        allFishes = new GameObject[numFish];

        for(int i = 0; i < numFish; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x), Random.Range(-swimLimits.y, swimLimits.y), Random.Range(-swimLimits.z, swimLimits.z));

            allFishes[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
            allFishes[i].GetComponent<Flock>().myManager = this;
        }

        goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0.0f, 100.0f) < 10.0f)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x), Random.Range(-swimLimits.y, swimLimits.y), Random.Range(-swimLimits.z, swimLimits.z));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, swimLimits*2);
    }
}
