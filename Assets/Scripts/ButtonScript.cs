using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
	private Button _button = null;
	private TicTacToe _board;

	void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

	void Update()
	{
		_button.interactable = GameData.Instance.IsPlayerTurn;
	}

	private void Awake()
	{
        _button = GetComponent<Button>();
        _board = GetComponentInParent<TicTacToe>();
	}

	private void OnClick()
	{
		TextMeshProUGUI buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
		if (buttonText.text == "")
		{
			Debug.Log("Button " + _button.name + " clicked");
			buttonText.text = "X";
			GameData.Instance.IsPlayerTurn = false;
		}
		else
		{
			Debug.Log("Button " + _button.name + " already played");
		}
	}
}
