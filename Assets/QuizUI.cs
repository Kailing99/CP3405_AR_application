using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class QuizUI : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] choiceButtons;
    public TMP_Text scoreText;
    public GameObject correctPanel;
    public GameObject incorrectPanel;
    public TMP_Text incorrectPanelCorrectAnswerText;

    private int score = 0;
    private QuizQuestion currentQuestion;
    private List<QuizQuestion> currentQuestions;
    private QuizManager quizManager;

    void Start()
    {
        quizManager = QuizManager.Instance;
        currentQuestions = quizManager.GetQuestionsForDifficulty();
        LoadNextQuestion();
    }

    // Load Next Question
    void LoadNextQuestion()
    {
        currentQuestions = quizManager.GetQuestionsForDifficulty(); 
        if (currentQuestions.Count > 0)
        {
            currentQuestion = currentQuestions[Random.Range(0, currentQuestions.Count)];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (i < currentQuestion.choices.Length) 
                {

                    TMP_Text buttonText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
                    if (buttonText != null)
                    {
                        buttonText.text = currentQuestion.choices[i];
                    }
                    else
                    {
                        Debug.LogError($"No Text component found in choice button at index {i}.");
                    }

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
            Debug.LogWarning("No more questions available for the selected difficulty.");
        }
    }

    // Called when a choice button is clicked
    void OnChoiceSelected(int choiceIndex)
    {
        if (choiceIndex == currentQuestion.correctAnswerIndex)
        {
            correctPanel.SetActive(true);
            score++;
            UpdateScore();
        }
        else
        {
            incorrectPanel.SetActive(true);
            incorrectPanelCorrectAnswerText.text = "Correct Answer: " + currentQuestion.choices[currentQuestion.correctAnswerIndex];
        }

        Invoke("HidePanels", 2f);
        Invoke("LoadNextQuestion", 2f);
    }

    // Update Score
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Hides the correct and incorrect feedback panels
    void HidePanels()
    {
        correctPanel.SetActive(false);
        incorrectPanel.SetActive(false);
    }
}
