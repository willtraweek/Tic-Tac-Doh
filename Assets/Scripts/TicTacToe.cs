using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TicTacToe : MonoBehaviour
{
    private Button[,] _board = new Button[3,3];
    private char _turn = 'X';

    // Update is called once per frame
    private void Update()
    {
        if (GameOver())
        {
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
            GameData.Instance.DidWin = _turn != 'X';
        }
    }

    private void Awake()
    {
        _turn = GameData.Instance.CurrentPlayer;
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

    public char MakeMove()
    {
        char output = _turn;
        // return output before _turn is changed in order to make it easier to see who starts at the top of the file
        _turn = _turn == 'X' ? 'O' : 'X';
        return output;
    }
    
    private bool GameOver()
    {
        char CheckRows()
        {
            for (int row = 0; row < 3; row++)
            {
                if (_board[row, 0].GetComponentInChildren<Text>().text ==
                    _board[row, 1].GetComponentInChildren<Text>().text &&
                    _board[row, 1].GetComponentInChildren<Text>().text ==
                    _board[row, 2].GetComponentInChildren<Text>().text &&
                    _board[row, 0].GetComponentInChildren<Text>().text != "")
                {
                    return _board[row, 0].GetComponentInChildren<Text>().text[0];
                }
            }
            return ' ';
        }

        char CheckColumns()
        {
            for (int col = 0; col < 3; col++)
            {
                if (_board[0, col].GetComponentInChildren<Text>().text ==
                    _board[1, col].GetComponentInChildren<Text>().text &&
                    _board[1, col].GetComponentInChildren<Text>().text ==
                    _board[2, col].GetComponentInChildren<Text>().text &&
                    _board[0, col].GetComponentInChildren<Text>().text != "")
                {
                    return _board[0, col].GetComponentInChildren<Text>().text[0];
                }
            }

            return ' ';
        }

        char CheckDiagonals()
        {
            if (((_board[0, 0].GetComponentInChildren<Text>().text == _board[1, 1].GetComponentInChildren<Text>().text &&
                 _board[1, 1].GetComponentInChildren<Text>().text == _board[2, 2].GetComponentInChildren<Text>().text)
                ||
                (_board[0, 2].GetComponentInChildren<Text>().text == _board[1, 1].GetComponentInChildren<Text>().text &&
                 _board[1, 1].GetComponentInChildren<Text>().text == _board[2, 0].GetComponentInChildren<Text>().text))
                && _board[1,1].GetComponentInChildren<Text>().text != "")
                 
            {
                return _board[1, 1].GetComponentInChildren<Text>().text[0];
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