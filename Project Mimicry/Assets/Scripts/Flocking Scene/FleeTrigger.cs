using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeTrigger : MonoBehaviour
{
    Collider[] hitCol;
    List<Flock> fishesInRange = new List<Flock>();
    [SerializeField] float influenceRadius;
    [SerializeField] LayerMask layerMask;

    private IEnumerator flee(Flock fish)
    {
        fish.speed = fish.myManager.maxSpeed * 1.5f;
        fish.accelerateSwim();

        yield return new WaitForSeconds(0.7f);

        fish.speed = Random.Range(fish.myManager.minSpeed, fish.myManager.maxSpeed);
        fish.decelerateSwim();
    }

    public void StartFlee()
    {
        hitCol = Physics.OverlapSphere(transform.position, influenceRadius, layerMask);

        foreach (Collider col in hitCol)
        {
            fishesInRange.Add(col.gameObject.GetComponent<Flock>());
        }

        foreach (Flock fish in fishesInRange)
        {
            StartCoroutine(flee(fish));
            fish.fleeBehaviour(this);
        }

        fishesInRange.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, influenceRadius);
    }
}
