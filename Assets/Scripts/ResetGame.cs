using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private Button _reset_button = null;
    private GameData _game_data = null;
    
    // Start is called before the first frame update
    void Start()
    {
        _reset_button.onClick.AddListener(OnClick);
    }

    private void Awake()
    {
        _reset_button = GetComponent<Button>();
        _game_data = FindAnyObjectByType<GameData>();
    }
    
    private void OnClick()
    {
        Debug.Log("Reset button clicked");
        _game_data.CurrentPlayer = 'X';
        SceneManager.LoadScene("Main");
    }
}
