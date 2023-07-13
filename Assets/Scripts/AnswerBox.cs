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

    private const float DisableTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        _box.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        ColorBlock colors = _box.colors;
        
        if (GameData.Instance.TimeOfLastAnswer != 0.0f &&
            Time.time - GameData.Instance.TimeOfLastAnswer < DisableTime)
        {
            _box.interactable = false;
            colors.disabledColor = correct_answer ? Color.green : Color.red;
        }
        else
        {
            _box.interactable = GameData.Instance.IsTriviaTime;
            colors.disabledColor = new Color(.66f, .66f, .66f);
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
        GameData.Instance.IsTriviaTime = false;
        if (correct_answer)
        {
            Debug.Log("Correct answer");
            _board.MakeMove("X");
        }
        else
        {
            Debug.Log("Incorrect answer");
            _board.MakeMove("O");
        }
        await Task.Delay((int)(DisableTime * 1000));
        _controller.GetNextQuestion();
    }
}
