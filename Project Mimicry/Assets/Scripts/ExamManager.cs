using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> arrows = new List<GameObject>();
    [SerializeField] private List<Renderer> checkBox = new List<Renderer>();

    [Header("Materials")]
    [SerializeField] Material incorrectMat;
    [SerializeField] Material correctMat;
    [SerializeField] Material neutralMat;
    [SerializeField] Material examMaterial;

    [Header("Exam")]
    [SerializeField] Renderer exam;

    [Header("Light")]
    [SerializeField] GameObject projectorLight;

    private int lastRandIndex = -1;
    private int currentQuestion = 0;

    private bool examRunning = true;

    // Start is called before the first frame update
    void Start()
    {      
        nextQuestion(generateRandIndex());
    }

    // Update is called once per frame
    void Update()
    {
        if (examRunning)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (lastRandIndex == 1)
                    checkAnswer(true);
                else
                    checkAnswer(false);            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (lastRandIndex == 2)
                    checkAnswer(true);
                else
                    checkAnswer(false);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (lastRandIndex == 0)
                    checkAnswer(true);
                else
                    checkAnswer(false);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (lastRandIndex == 3)
                    checkAnswer(true);
                else
                    checkAnswer(false);
            }
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

    private void checkAnswer(bool isCorrect)
    {
        if (isCorrect)
            checkBox[currentQuestion].material = correctMat;
        else
            checkBox[currentQuestion].material = incorrectMat;

        currentQuestion++;
        nextQuestion(generateRandIndex());

        if (currentQuestion >= checkBox.Count)
        {
            examRunning = false;
            StartCoroutine("resetExam");
        }
    }

    private IEnumerator resetExam()
    {
        foreach (GameObject GO in arrows)
        {
            if (!GO.activeSelf) GO.SetActive(true);
        }
        currentQuestion = 0;

        yield return new WaitForSeconds(1f);

        foreach(Renderer CB in checkBox)
        {
            CB.material = neutralMat;
        }
        exam.material = neutralMat;
        lastRandIndex = -1;
        projectorLight.SetActive(false);

        yield return new WaitForSeconds(1f);

        exam.material = examMaterial;
        examRunning = true;
        projectorLight.SetActive(true);

        nextQuestion(generateRandIndex());
    }
}
