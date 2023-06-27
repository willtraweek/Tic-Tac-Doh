using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	private Button _button = null;
	private TicTacToe _board;

	void Start()
    {
        _button.onClick.AddListener(OnClick);
    }

	private void Awake()
	{
        _button = GetComponent<Button>();
        _board = GetComponentInParent<TicTacToe>();
	}

	private void OnClick()
	{
		if (GameData.Instance.IsClickable)
		{
			Text buttonText = _button.GetComponentInChildren<Text>();
			if (buttonText.text == "")
			{
				Debug.Log("Button " + _button.name + " clicked");
				buttonText.text = _board.MakeMove().ToString();
				Debug.Log(buttonText.text);
			}
			else
			{
				Debug.Log("Button " + _button.name + " already played");
			}
		}
		else
		{
			Debug.Log("Button " + _button.name + " not clickable");
		}
	}
}
