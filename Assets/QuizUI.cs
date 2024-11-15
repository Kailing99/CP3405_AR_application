using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizUI : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] choiceButtons;
    public TMP_Text questionIndexText;
    public TMP_Text scoreText;
    public GameObject correctPanel;
    public GameObject incorrectPanel;
    public TMP_Text incorrectPanelCorrectAnswerText;
    public AudioSource quizAudioSource;
    public AudioSource correctSound;
    public AudioSource incorrectSound;
    public AudioSource winSound;
    public AudioSource loseSound;

    private int score = 0;
    private int currentQuestionIndex = 0;
    private List<QuizQuestion> currentQuestions;
    private QuizManager quizManager;
    private bool quizCompleted = false; // Flag to track if the quiz is completed
    private bool isWaiting = false; // for the wait

    void Start()
    {
        quizManager = QuizManager.Instance;
        currentQuestions = quizManager.GetQuestionsForDifficulty();

        currentQuestions = ShuffleQuestions(currentQuestions);

        // Quiz Audio strat playing
        if(quizAudioSource != null)
        {
            quizAudioSource.Play();
        }

        LoadNextQuestion();
    }

    void LoadNextQuestion()
    {
        if (currentQuestionIndex < currentQuestions.Count)  
        {
            QuizQuestion currentQuestion = currentQuestions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;
            questionIndexText.text = $"Qestion: {currentQuestionIndex + 1}/{currentQuestions.Count}";

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentQuestion.choices.Length)
                {
                    TMP_Text buttonText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
                    buttonText.text = currentQuestion.choices[i];
                    choiceButtons[i].gameObject.SetActive(true);  
                    choiceButtons[i].onClick.RemoveAllListeners();

                    int choiceIndex = i;
                    choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
                }
                else
                {
                    choiceButtons[i].gameObject.SetActive(false); 
                }
            }
        }
        else
        {
            ShowFinalScore(); 
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        SetChoiceButtonsInteractable(false); // make choice buttons not interactable to avoid spam

        QuizQuestion currentQuestion = currentQuestions[currentQuestionIndex];
        if (choiceIndex == currentQuestion.correctAnswerIndex)
        {
            correctPanel.SetActive(true);
            score++;
            UpdateScore();

            if (correctSound != null)
            {
                correctSound.Play();
            }
        }
        else
        {
            incorrectPanel.SetActive(true);
            incorrectPanelCorrectAnswerText.text = "Correct Answer: " + currentQuestion.choices[currentQuestion.correctAnswerIndex];

            if (incorrectSound != null)
            {
                incorrectSound.Play();
            }
        }


        Invoke("HidePanels", 1f);
        currentQuestionIndex++; 
        Invoke("LoadNextQuestion", 1f);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void ShowFinalScore()
    {
        questionText.text = "Quiz Completed! \nCongradulations you got \nFinal Score: " + score + "/" + currentQuestions.Count;
        questionText.alignment = TextAlignmentOptions.Center;

        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false); 
        }
        quizCompleted = true;

        // Quiz Audio stop playing
        if(quizAudioSource != null)
        {
            quizAudioSource.Stop();
        }

        if(score >= 5)
        {
            if(winSound != null)
            {
                winSound.Play();
            }
        }
        else
        {
            if(loseSound != null)
            {
                loseSound.Play();
            }
        }
    }
    void SetChoiceButtonsInteractable(bool interactable)
    {
        foreach (Button button in choiceButtons)
        {
            button.interactable = interactable;
        }
    }

    void HidePanels()
    {
        correctPanel.SetActive(false);
        incorrectPanel.SetActive(false);

        SetChoiceButtonsInteractable(true); // make choice button interactable again
    }

    List<QuizQuestion> ShuffleQuestions(List<QuizQuestion> questions)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QuizQuestion temp = questions[i];
            int randomIndex = Random.Range(i, questions.Count);
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
        return questions;
    }

    void Update()
    {
        if (quizCompleted && Input.GetMouseButtonDown(0) && !isWaiting) 
        {
            isWaiting = true; 

            StartCoroutine(WaitAndChangeScene());

            IEnumerator WaitAndChangeScene()
            {
                yield return new WaitForSeconds(1f); 
                SceneManager.LoadScene("DifficultyScene");
                Debug.Log("Returning to DifficultyUI");
                isWaiting = false; 
            }
        }
    }
}
