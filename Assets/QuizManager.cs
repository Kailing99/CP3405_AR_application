using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<QuizQuestion> quizQuestions = new List<QuizQuestion>();

    void Start()
    {
        LoadQuizData();
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

    [System.Serializable]
    private class ListWrapper<T>
    {
        public List<T> items;
    }
}
