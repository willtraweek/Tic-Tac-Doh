using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TicTacToe : MonoBehaviour
{
    private Button[,] _board = new Button[3,3];

    // Update is called once per frame
    private void Update()
    {
        if (GameOver())
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
            GameData.Instance.DidPlayerWin = GameData.Instance.MostRecentTurnWasPlayer;
        }
    }

    private void Awake()
    {
        Button[] temp = GetComponentsInChildren<Button>();
        
        Button findButton(string name)
        {
            foreach (Button button in temp)
            {
                if (button.name == name)
                {
                    return button;
                }
            }
            throw new Exception("Button " + name + " not found");
        }
        
        _board[0, 0] = findButton("TopLeft");
        _board[0, 1] = findButton("TopMiddle");
        _board[0, 2] = findButton("TopRight");

        _board[1, 0] = findButton("CenterLeft");
        _board[1, 1] = findButton("CenterMiddle");
        _board[1, 2] = findButton("CenterRight");

        _board[2, 0] = findButton("BottomLeft");
        _board[2, 1] = findButton("BottomMiddle");
        _board[2, 2] = findButton("BottomRight");
        
    }

    private bool GameOver()
    {
        char CheckRows()
        {
            for (int row = 0; row < 3; row++)
            {
                if (_board[row, 0].GetComponentInChildren<TextMeshProUGUI>().text ==
                    _board[row, 1].GetComponentInChildren<TextMeshProUGUI>().text &&
                    _board[row, 1].GetComponentInChildren<TextMeshProUGUI>().text ==
                    _board[row, 2].GetComponentInChildren<TextMeshProUGUI>().text &&
                    _board[row, 0].GetComponentInChildren<TextMeshProUGUI>().text != "")
                {
                    return _board[row, 0].GetComponentInChildren<TextMeshProUGUI>().text[0];
                }
            }
            return ' ';
        }

        char CheckColumns()
        {
            for (int col = 0; col < 3; col++)
            {
                if (_board[0, col].GetComponentInChildren<TextMeshProUGUI>().text ==
                    _board[1, col].GetComponentInChildren<TextMeshProUGUI>().text &&
                    _board[1, col].GetComponentInChildren<TextMeshProUGUI>().text ==
                    _board[2, col].GetComponentInChildren<TextMeshProUGUI>().text &&
                    _board[0, col].GetComponentInChildren<TextMeshProUGUI>().text != "")
                {
                    return _board[0, col].GetComponentInChildren<TextMeshProUGUI>().text[0];
                }
            }

            return ' ';
        }

        char CheckDiagonals()
        {
            if (((_board[0, 0].GetComponentInChildren<TextMeshProUGUI>().text == _board[1, 1].GetComponentInChildren<TextMeshProUGUI>().text &&
                 _board[1, 1].GetComponentInChildren<TextMeshProUGUI>().text == _board[2, 2].GetComponentInChildren<TextMeshProUGUI>().text)
                ||
                (_board[0, 2].GetComponentInChildren<TextMeshProUGUI>().text == _board[1, 1].GetComponentInChildren<TextMeshProUGUI>().text &&
                 _board[1, 1].GetComponentInChildren<TextMeshProUGUI>().text == _board[2, 0].GetComponentInChildren<TextMeshProUGUI>().text))
                && _board[1,1].GetComponentInChildren<TextMeshProUGUI>().text != "")
                 
            {
                return _board[1, 1].GetComponentInChildren<TextMeshProUGUI>().text[0];
            }
            return ' ';
        }
        char[] players = new char[] { 'X', 'O' };
        foreach (char player in players)
        {
            if (CheckRows() == player || CheckColumns() == player || CheckDiagonals() == player)
            {
               Debug.Log(player + "won!");
               return true;
            }
        }
        return false;
    }
}
