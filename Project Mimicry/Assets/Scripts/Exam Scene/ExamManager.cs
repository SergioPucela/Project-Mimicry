using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [Header("Bots")]
    [SerializeField] bool isBot;
    [SerializeField] float answerSpeed;

    [Header("Sounds")]
    [SerializeField] AudioSource audioSource;

    [Header("Lists")]
    [SerializeField] private List<GameObject> arrows = new List<GameObject>();
    [SerializeField] private List<Renderer> checkBox = new List<Renderer>();

    [Header("Materials")]
    [SerializeField] Material incorrectMat;
    [SerializeField] Material correctMat;
    [SerializeField] Material neutralMat;
    [SerializeField] Material examMaterial;

    [Header("Exam")]
    [SerializeField] GameObject exam;

    [Header("Light")]
    [SerializeField] Light projectorLight;

    private int lastRandIndex = -1;
    private int currentQuestion = 0;

    private bool examRunning = true;

    [HideInInspector] public int NumExams = 0;

    // Start is called before the first frame update
    void Start()
    {      
        nextQuestion(generateRandIndex());

        if (isBot)
            StartCoroutine("randomAnswer");
    }

    // Update is called once per frame
    void Update()
    {
        if (examRunning && !isBot)
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

    private IEnumerator randomAnswer()
    {
        int rand = Random.Range(0, 2);

        if(rand >= 1)
            checkBox[currentQuestion].material = correctMat;
        else
            checkBox[currentQuestion].material = incorrectMat;

        currentQuestion++;
        nextQuestion(generateRandIndex());

        if (currentQuestion >= checkBox.Count)
            StartCoroutine("resetExam");

        yield return new WaitForSeconds(answerSpeed);

        StartCoroutine("randomAnswer");
    }

    private IEnumerator resetExam()
    {
        NumExams++;

        foreach (GameObject GO in arrows)
        {
            if (!GO.activeSelf) GO.SetActive(true);
        }
        currentQuestion = 0;

        yield return new WaitForSeconds(0.5f);

        audioSource.Play();

        foreach(Renderer CB in checkBox)
        {
            CB.material = neutralMat;
        }
        exam.SetActive(false);
        lastRandIndex = -1;
        projectorLight.enabled = false;

        yield return new WaitForSeconds(0.5f);

        exam.SetActive(true);
        examRunning = true;
        projectorLight.enabled = true;

        nextQuestion(generateRandIndex());
    }
}
