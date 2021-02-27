using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeTrigger : MonoBehaviour
{
    Collider[] hitCol;
    List<Flock> fishesInRange = new List<Flock>();
    [SerializeField] float influenceRadius;
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Awake()
    {
        hitCol = Physics.OverlapSphere(transform.position, influenceRadius, layerMask);

        foreach(Collider col in hitCol)
        {
            fishesInRange.Add(col.gameObject.GetComponent<Flock>());
        }

        print(fishesInRange.Count);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, influenceRadius);
    }
}
