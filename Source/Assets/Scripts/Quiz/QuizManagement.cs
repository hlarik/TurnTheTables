using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManagement : MonoBehaviour
{

    public Quiz quiz;
    private GameObject QuestionCanvas;
    private GameObject QPanel;

    private GameObject FeedbackCanvas;
    private GameObject FeedbackPanel;
    private TextMeshProUGUI FeedBackText;

    private int numberOfQuestions;
    private int currentQuestion = 0;

    private TextMeshProUGUI questionText;
    private TextMeshProUGUI buttonText; //Answer Button Text
    private TextMeshProUGUI buttonText1; //Answer Button Text
    private TextMeshProUGUI buttonText2; //Answer Button Text

    private Button nextButton;
    private Button answerButton;
    private Button answerButton1;
    private Button answerButton2;
    private Button feedBackButton;

    private GameObject cameraScript;




    // Start is called before the first frame update
    void Start()
    {

        cameraScript = Camera.main.gameObject;

        QuestionCanvas = GameObject.Find("QuestionCanvas");
        QPanel = GameObject.Find("QPanel");

        Transform[] children = QuestionCanvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children)
        {
            if (t.name == "QPanel")
            {
                QPanel = t.gameObject;
            }
        }

        FeedbackCanvas = GameObject.Find("FeedbackCanvas");
        FeedbackPanel = GameObject.Find("FeedbackPanel");
        Transform[] children2 = FeedbackCanvas.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in children2)
        {
            if (t.name == "FeedbackPanel")
            {
                FeedbackPanel = t.gameObject;
            }
        }

        numberOfQuestions = quiz.Sorular.Length;

        questionText = (GameObject.Find("QuestionText")).GetComponent<TextMeshProUGUI>();
        buttonText = (GameObject.Find("ButtonText")).GetComponent<TextMeshProUGUI>();
        buttonText1 = (GameObject.Find("ButtonText1")).GetComponent<TextMeshProUGUI>();
        buttonText2 = (GameObject.Find("ButtonText2")).GetComponent<TextMeshProUGUI>();


        nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.AddListener(showCurrentQuestion);

        answerButton = GameObject.Find("AnswerButton").GetComponent<Button>();
        answerButton.onClick.AddListener(delegate { showFeedBack(0); });

        answerButton1 = GameObject.Find("AnswerButton1").GetComponent<Button>();
        answerButton1.onClick.AddListener(delegate { showFeedBack(1); });

        answerButton2 = GameObject.Find("AnswerButton2").GetComponent<Button>();
        answerButton2.onClick.AddListener(delegate { showFeedBack(2); });


       
        QPanel.SetActive(false);
      //  startQuiz(); // it is for testing purposes



    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void startQuiz()
    {
        QPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        cameraScript.GetComponent<CameraController>().disableCameraMouse();
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = false;


        showCurrentQuestion();
    }

    public void endQuiz()
    {
        QPanel.SetActive(false);

        cameraScript.GetComponent<CameraController>().enableCameraMouse();
        GameObject.Find("Violet").GetComponent<PlayerController>().enabled = true;
        Cursor.visible = false; //?


        currentQuestion = 0;
    }


    public void showCurrentQuestion()
    {
        if (numberOfQuestions != currentQuestion)
        {
            questionText.text = quiz.Sorular[currentQuestion].Question;
            buttonText.text = "A) " + quiz.Sorular[currentQuestion].Answer[0].Answer;
            buttonText1.text = "B) " + quiz.Sorular[currentQuestion].Answer[1].Answer;
            buttonText2.text = "C) " + quiz.Sorular[currentQuestion].Answer[2].Answer;
            /*if(quiz.Sorular[currentQuestion].Answer.Length == 3)
            {
                GameObject.Find("AnswerButton2").SetActive(true);

            }
            else
            {
                GameObject.Find("AnswerButton2").SetActive(false);
            }
            */

            currentQuestion++;
        }
        else
        {
            endQuiz();
        }

    }

    public void showFeedBack(int questionNumber)
    {
        currentQuestion--;
        QPanel.SetActive(false);
        FeedbackPanel.SetActive(true);
        FeedBackText = (GameObject.Find("FeedbackText")).GetComponent<TextMeshProUGUI>();
        FeedBackText.text = quiz.Sorular[currentQuestion].Answer[questionNumber].Feedback;
        feedBackButton = GameObject.Find("closeButton").GetComponent<Button>();
        feedBackButton.onClick.AddListener(closeFeedbackScreen);
        currentQuestion++;
    }

    public void closeFeedbackScreen()
    {
        FeedbackPanel.SetActive(false);
        QPanel.SetActive(true);

    }


}
