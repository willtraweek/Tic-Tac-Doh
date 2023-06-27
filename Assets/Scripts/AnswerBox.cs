using UnityEngine;
using UnityEngine.UI;

public class AnswerBox : MonoBehaviour
{
    private TriviaController _controller = null;
    private Button _box = null;
    [HideInInspector]
    public bool correct_answer = false;
    // Start is called before the first frame update
    void Start()
    {
        _box.onClick.AddListener(OnClick);
    }

    private void Awake()
    {
        _box = GetComponent<Button>();
        _controller = GetComponentInParent<TriviaController>();
    }
    
    private void OnClick()
    {
        Debug.Log("Answer box clicked");
        if (correct_answer)
        {
            Debug.Log("Correct answer");
            // make the tic-tac-toe clickable
        }
        else
        {
            Debug.Log("Incorrect answer");
            // let the AI take a turn
        }
        _controller.GetNextQuestion();
    }
    
}
