[System.Serializable]
public class QuizQuestion
{
    public string questionText;
    public string[] choices;
    public int correctAnswerIndex;
    public DifficultyLevel difficulty;
}

public enum DifficultyLevel
{
    Easy,
    Normal,
    Hard
}

