using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static void Main()
    {
        string filePath = "C:/QUESTION_BANK/question-bank-master/sample-questions.md"; 
        var questions = QuestionParser.ParseFile(filePath);

        foreach (var question in questions)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Topic: {question.Topic}");
            Console.WriteLine();
            Console.WriteLine($"Sub-Topic: {question.SubTopic}");
            Console.WriteLine();
            Console.WriteLine($"Question ID: {question.Id}");
            Console.WriteLine($"Title: {question.Title}");
            Console.WriteLine($"Options:");
            foreach (var option in question.Options)
            {
                Console.WriteLine($"{option.Id}. {option.Title} (Correct: {option.IsCorrect})");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

