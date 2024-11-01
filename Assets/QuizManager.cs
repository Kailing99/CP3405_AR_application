using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance { get; private set; }

    private List<QuizQuestion> allQuestions = new List<QuizQuestion>();
    public List<QuizQuestion> quizQuestions = new List<QuizQuestion>();
    private DifficultyLevel selectedDifficulty;

    private QuizData quizData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("QuizManager initialized.");
            LoadQuizData();
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate QuizManager destroyed.");
        }
    }

    void LoadQuizData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("quizData");
        if (jsonData != null)
        {
            quizData = JsonUtility.FromJson<QuizData>(jsonData.text);
            if (quizData != null)
            {
                // Clear previous questions before adding new ones
                allQuestions.Clear();
                allQuestions.AddRange(quizData.Easy ?? new List<QuizQuestion>());
                allQuestions.AddRange(quizData.Normal ?? new List<QuizQuestion>());
                allQuestions.AddRange(quizData.Hard ?? new List<QuizQuestion>());

                Debug.Log($"Loaded {quizData.Easy.Count} easy questions, {quizData.Normal.Count} normal questions, and {quizData.Hard.Count} hard questions.");
            }
            else
            {
                Debug.LogError("Failed to deserialize QuizData from JSON.");
            }
        }
        else
        {
            Debug.LogError("Failed to load quiz data from Resources.");
        }
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        selectedDifficulty = difficulty;
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                quizQuestions = quizData.Easy;
                break;
            case DifficultyLevel.Normal:
                quizQuestions = quizData.Normal;
                break;
            case DifficultyLevel.Hard:
                quizQuestions = quizData.Hard; 
                break;
        }
    }


    public List<QuizQuestion> GetQuestionsForDifficulty()
    {
        Debug.Log($"Returning {quizQuestions.Count} questions for difficulty: {selectedDifficulty}");
        return quizQuestions;
    }

    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> items;
    }
}
