using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelection : MonoBehaviour
{
    public void SetDifficulty(int difficulty)
    {
        DifficultyLevel selectedDifficulty = (DifficultyLevel)difficulty;
        QuizManager.Instance.SetDifficulty(selectedDifficulty);
        SceneManager.LoadScene("QuizQuestionScene"); 
    }
}