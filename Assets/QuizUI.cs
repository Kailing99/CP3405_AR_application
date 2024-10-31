using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class QuizUI : MonoBehaviour
{
    public Text questionText;
    public Button[] choiceButtons;
    public Text scoreText;
    public GameObject correctPanel;
    public GameObject incorrectPanel;
    public Text incorrectPanelCorrectAnswerText;

    private int score = 0;
    private QuizQuestion currentQuestion;
    private List<QuizQuestion> currentQuestions;
    private QuizManager quizManager;

    // Start is called before the first frame update
    void Start()
    {
        quizManager = FindAnyObjectByType<QuizManager>();
    }

    // Sets the difficulty level and fliters the question based on the selected difficulty
    public void SetDifficulty(DifficultyLevel difficulty)
    {
        currentQuestions =quizManager.quizQuestions.Where(q=>q.difficulty==difficulty).ToList();
        
        LoadNextQuestion();
    }

    //Load Next Question
    void LoadNextQuestion()
    {
        if (currentQuestions.Count > 0)
        {
            currentQuestion = currentQuestions[Random.Range(0, currentQuestions.Count)];

            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].GetComponentInChildren<Text>().text = currentQuestion.choices[i];

                choiceButtons[i].onClick.RemoveAllListeners();

                int choiceIndex = i;

                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choiceIndex));
            }
        }
        else
        {
            Debug.Log("No more questions.");
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
        else {

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
