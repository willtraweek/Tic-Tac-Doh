using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private Button _reset_button = null;
    
    // Start is called before the first frame update
    void Start()
    {
        _reset_button.onClick.AddListener(OnClick);
    }

    private void Awake()
    {
        _reset_button = GetComponent<Button>();
    }
    
    private void OnClick()
    {
        Debug.Log("Reset button clicked");
        GameData.Instance.IsPlayerTurn = false;
        SceneManager.LoadScene("Main");
    }
}
