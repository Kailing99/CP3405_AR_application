using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string lastScene; // Saves the last scene used/opened for back button

    public void LoadARScene()
    {
        StoreLastScene();
        Debug.Log("Begin button pressed!");
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadQuizScene()
    {
        StoreLastScene();
        Debug.Log("Quiz button pressed!");
        SceneManager.LoadScene("DifficultyScene");
    }

    public void LoadHelpGuideScene()
    {
        StoreLastScene();
        Debug.Log("Help button pressed!");
        SceneManager.LoadScene("HelpGuideScene");
    }

    public void LoadEasyScene()
    {
        Debug.Log("Easy button pressed!");
        SceneManager.LoadScene("QuizQuestionScene");
    }

    public void LoadNormalScene()
    {
        Debug.Log("Normal button pressed!");
        SceneManager.LoadScene("QuizQuestionScene");
    }

    public void LoadHardScene()
    {
        Debug.Log("Hard button pressed!");
        SceneManager.LoadScene("QuizQuestionScene");
    }

    private void StoreLastScene()
    {
        lastScene = SceneManager.GetActiveScene().name; // Get the current active scene name
    }

    // Function for the Back button
    public void GoBack()
    {
        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene); // Load the last scene
        }
        else
        {
            Debug.LogWarning("No last scene to go back to.");
        }
    }

    private void Update()
    {
        // Check if the Android back button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack(); 
        }
    }
}
