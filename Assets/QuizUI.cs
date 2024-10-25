using UnityEngine.UI;
using System.Linq;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
