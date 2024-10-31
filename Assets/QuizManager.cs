using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance { get; private set; } // Singleton instance

    public List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
    private List<QuizQuestion> filteredQuestions = new List<QuizQuestion>();
    private DifficultyLevel selectedDifficulty;

    void Awake()
    {
        // Set up Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            LoadQuizData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadQuizData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("quizData");
        if (jsonData != null)
        {
            quizQuestions = JsonUtility.FromJson<ListWrapper<QuizQuestion>>(jsonData.text).items;
            Debug.Log("Quiz data loaded successfully!");
        }
        else
        {
            Debug.LogError("Failed to load quiz data.");
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        selectedDifficulty = difficulty;
        filteredQuestions = quizQuestions.FindAll(q => q.difficulty == selectedDifficulty);
    }

    public List<QuizQuestion> GetQuestionsForDifficulty()
    {
        return filteredQuestions;
    }

    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> items;
    }
}
