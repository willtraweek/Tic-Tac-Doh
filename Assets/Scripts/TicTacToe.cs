using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine._Scripting;

public class TicTacToe : MonoBehaviour
{
    private Button[,] _board = new Button[3,3];
    public bool isClickable = true;
    private char _turn = 'X';

    // Update is called once per frame
    void Update()
    {
        if (GameOver())
        {
            Console.WriteLine("Game Over");
            //TODO: Change Scene + Change GameStatus Text
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

    public char MakeMove()
    {
        char output = _turn;
        // return output before _turn is changed in order to make it easier to see who starts at the top of the file
        _turn = _turn == 'X' ? 'O' : 'X';
        return output;
    }
    
    private bool GameOver()
    {
        //TODO: Check for a winner
        return false;
    }
}
