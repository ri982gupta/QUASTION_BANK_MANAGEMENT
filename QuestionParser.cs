using System;
using System.Collections.Generic;
using System.IO;

public class QuestionParser
{
    public static List<Question> ParseFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var questions = new List<Question>();
        Question currentQuestion = null;
        string currentTopic = null;
        string currentSubTopic = null;

        foreach (var line in lines)
        {
            if (line.StartsWith("## "))
            {
                // New Topic
                currentTopic = line.Substring(3).Trim();
                currentQuestion = null; 
                currentSubTopic = null; 
            }
            else if (line.StartsWith("### "))
            {
                // New Sub-Topic
                currentSubTopic = line.Substring(4).Trim();
                currentQuestion = null; 
            }
            else if (line.Contains('.') && int.TryParse(line.Split('.')[0], out int questionNumber))
            {
                // New Question
                var questionParts = line.Split(new[] { '.' }, 2);
                if (questionParts.Length == 2)
                {
                    var questionId = questionParts[0].Trim();
                    var questionTitle = questionParts[1].Trim();
                    currentQuestion = new Question
                    {
                        Id = questionId,
                        Title = questionTitle,
                        Options = new List<Option>(),
                        Topic = currentTopic,
                        SubTopic = currentSubTopic
                    };
                    questions.Add(currentQuestion);
                }
            }
            else if (currentQuestion != null && !string.IsNullOrWhiteSpace(line) && line.Contains('.'))
            {
                // Option for the current question
                var optionParts = line.Split(new[] { '.' }, 2);
                if (optionParts.Length == 2)
                {
                    var optionId = optionParts[0].Trim();
                    var optionTitle = optionParts[1].Trim();
                    bool isCorrect = optionId.EndsWith("*");
                    if (isCorrect)
                        optionId = optionId.TrimEnd('*');

                    var option = new Option
                    {
                        Id = optionId,
                        Title = optionTitle,
                        IsCorrect = isCorrect
                    };
                    currentQuestion.Options.Add(option);
                }
            }
        }

        return questions;
    }
}

