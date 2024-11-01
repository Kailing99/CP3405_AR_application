using System;
using System.Collections.Generic;

[Serializable]
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

public class QuizData
{
    public List<QuizQuestion> Easy;
    public List<QuizQuestion> Normal;
    public List<QuizQuestion> Hard;
}