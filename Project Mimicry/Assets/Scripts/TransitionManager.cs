using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] float countdown;

    [Header("Flocking Scene")]
    [SerializeField] ArmController arm;

    [Header("Exam Projector Scene")]
    [SerializeField] ExamManager exam;

    [Header("Screens Scene")]
    [SerializeField] Blink screen;

    [Header("Mentor Scene")]
    [SerializeField] VideoPlayer video;

    private bool isFlockingScene;
    private bool isExamScene;
    private bool isScreenScene;
    private bool isMentorScene;

    private float seconds = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        isFlockingScene = arm != null;
        isExamScene = exam != null;
        isScreenScene = screen != null;
        isMentorScene = video != null;

        if (isMentorScene) countdown = (float)video.length - 2f; // -2f because of the FadeIn animation duration
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if (isFlockingScene)
            checkArmClicks();
        else if (isExamScene)
            checkExamDuration();
        else if (isScreenScene)
            checkScreenEnd();
        else if (isMentorScene)
            checkCountdown();
    }

    private void checkCountdown()
    {
        if (seconds >= countdown)
            GM.startTransition = true;
    }

    private void checkArmClicks()
    {
        if(seconds >= countdown && arm.NumClicks >= 10)
        {
            GM.startTransition = true;
        }
    }

    private void checkExamDuration()
    {
        if(seconds >= countdown && exam.NumExams >= 3)
        {
            GM.startTransition = true;
        }
    }

    private void checkScreenEnd()
    {
        if (screen.endScene)
        {
            GM.startTransition = true;
        }
    }
}
