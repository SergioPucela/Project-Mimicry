using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrows = new List<GameObject>();
    [SerializeField] private List<GameObject> checkBox = new List<GameObject>();

    private int lastRandIndex = -1;

    // Start is called before the first frame update
    void Start()
    {      
        nextQuestion(generateRandIndex());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (lastRandIndex == 1)
                print("Correcto");
            else
                print("Incorrecto");
            nextQuestion(generateRandIndex());
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            if (lastRandIndex == 2)
                print("Correcto");
            else
                print("Incorrecto");
            nextQuestion(generateRandIndex());
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if (lastRandIndex == 0)
                print("Correcto");
            else
                print("Incorrecto");
            nextQuestion(generateRandIndex());
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (lastRandIndex == 3)
                print("Correcto");
            else
                print("Incorrecto");
            nextQuestion(generateRandIndex());
        }
    }

    private void nextQuestion(int randIndex)
    {
        if(lastRandIndex != randIndex)
        {
            if (lastRandIndex != -1)
                arrows[lastRandIndex].SetActive(true);

            lastRandIndex = randIndex;
            arrows[randIndex].SetActive(false);
        }
        else
        {
            nextQuestion(generateRandIndex());
        }
    }

    private int generateRandIndex()
    {
        int numRand = Random.Range(0, 4);

        return numRand;
    }
}
