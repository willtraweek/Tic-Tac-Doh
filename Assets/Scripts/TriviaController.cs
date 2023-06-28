using System.Linq;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;

public class TriviaQuestion
{
    public string category { get; set; }
    public string type { get; set; }
    public string difficulty { get; set; }
    public string question { get; set; }
    public string correct_answer { get; set; }
    public string[] incorrect_answers { get; set; }
    public bool played { get; set; } = false;
}

public class WebResponse
{
    public int response_code { get; set; }
    public TriviaQuestion[] results { get; set; }
}

public class TriviaController : MonoBehaviour
{
    List<TriviaQuestion> questions = new List<TriviaQuestion>();
    int current_question = 0;
    System.Random random = new System.Random();
    AnswerBox[] answer_boxes = null;

    private void Awake()
    {
        Debug.Log("Waking Trivia Controller");
        GetQuestions();
        answer_boxes = GetComponentsInChildren<AnswerBox>();
        GetCurrentQuestion();
    }

    public TriviaQuestion GetCurrentQuestion()
    {
        TriviaQuestion question = questions[current_question];
        // Set the question text
        GetComponentInChildren<TextMeshProUGUI>().text = question.question;
        
        // Set the answer box texts
        List<string> possible_answers = question.incorrect_answers.ToList();
        possible_answers.Add(question.correct_answer);
        possible_answers = possible_answers.OrderBy(x => random.Next()).ToList();
        for (int i = 0; i < answer_boxes.Length; i++)
        {
            answer_boxes[i].GetComponentInChildren<TextMeshProUGUI>().text = possible_answers[i];
            answer_boxes[i].correct_answer = possible_answers[i] == question.correct_answer;
        }
        return question;
    }
    
    public TriviaQuestion GetNextQuestion()
    {
        Debug.Log($"Question #: {current_question} out of {questions.Count}");
        if (current_question >= questions.Count-1)
        {
            GetQuestions();
        }
        current_question++;
        return GetCurrentQuestion();
    }

    void GetQuestions()
    {
        Debug.Log("Getting questions");
        string difficulty = "easy";
        int count = 10;
        string url = $"https://opentdb.com/api.php?amount={count}&category=9&difficulty={difficulty}&type=multiple";
        using HttpClient client = new HttpClient();
        
        string response = client.GetStringAsync(url).Result;

        questions.AddRange(JsonConvert.DeserializeObject<WebResponse>(response).results);
    }
}
