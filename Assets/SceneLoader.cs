using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadARScene()
    {
        Debug.Log("Begin button pressed!");
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadQuizScene()
    {
        Debug.Log("Quiz button pressed!");
        SceneManager.LoadScene("DifficultyScene");
    }

    public void LoadHelpGuideScene()
    {
        Debug.Log("Help button pressed!");
        SceneManager.LoadScene("HelpGuideScene");
    }
}
