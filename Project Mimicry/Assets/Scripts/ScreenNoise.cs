using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenNoise : MonoBehaviour
{
    private Renderer screen;

    // Start is called before the first frame update
    void Start()
    {
        screen = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        screen.sharedMaterial.SetFloat("_Factor1", Random.Range(1000, 3000));
        screen.sharedMaterial.SetFloat("_Factor2", Random.Range(400, 800));
        screen.sharedMaterial.SetFloat("_Factor3", Random.Range(4000, 6000));
    }
}
