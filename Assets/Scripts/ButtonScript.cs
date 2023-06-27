using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	private Button _button = null;
	private bool _unplayed = true;
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
		if (_board.isClickable)
		{
			if (_unplayed)
			{
				Debug.Log("Button " + _button.name + " clicked");
				_button.GetComponentInChildren<Text>().text = _board.MakeMove().ToString();
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
