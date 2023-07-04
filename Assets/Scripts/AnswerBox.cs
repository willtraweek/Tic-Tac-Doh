using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AnswerBox : MonoBehaviour
{
    private TriviaController _controller = null;
    private Button _box = null;
    private TicTacToe _board = null;
    [HideInInspector]
    public bool correct_answer = false;

    private static float DISABLE_TIME = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        _box.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        ColorBlock colors = _box.colors;
        
        if (GameData.Instance.TimeOfLastAnswer != 0.0f &&
            Time.time - GameData.Instance.TimeOfLastAnswer < DISABLE_TIME)
        {
            _box.interactable = false;
            colors.disabledColor = correct_answer ? Color.green : Color.red;
        }
        else
        {
            _box.interactable = true;
            colors.normalColor = Color.white;
        }
        
        _box.colors = colors;
    }

    private void Awake()
    {
        _box = GetComponent<Button>();
        _controller = GetComponentInParent<TriviaController>();
        _board = FindObjectOfType<TicTacToe>();
    }
    
    private async void OnClick()
    {
        Debug.Log("Answer box clicked");
        GameData.Instance.TimeOfLastAnswer = Time.time;
        if (correct_answer)
        {
            Debug.Log("Correct answer");
            GameData.Instance.IsPlayerTurn = true;
        }
        else
        {
            Debug.Log("Incorrect answer");
            _board.MakeAIMove();
        }
        // TODO: Add timer for disable time before doing next question
        await Task.Delay((int)(DISABLE_TIME * 1000 + 42)); // 42 = 1000/24 WAIT AN EXTRA FRAME BEFORE RESETTING
        _controller.GetNextQuestion();
    }
}
