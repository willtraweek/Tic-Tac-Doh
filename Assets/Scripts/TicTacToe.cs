using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

public enum GameState
{
    PlayerWin,
    AIWin,
    Tie,
    InProgress
}

public class TicTacToe : MonoBehaviour
{
    private Button[,] _board;
    private TextMeshProUGUI[,] _text;

    // Update is called once per frame
    private void Update()
    {
        // TODO: MOVE THE BELOW TO A MANUAL CHECK WHEN AI MAKES MOVE OR USER MAKES MOVE
        // SHOULD SAVE PROCESSING POWER
        if (IsGameOver() != GameState.InProgress)
        {
            // TODO: NEED TO DO SOMETHING FOR TIES HERE
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
            GameData.Instance.DidPlayerWin = GameData.Instance.MostRecentTurnWasPlayer;
            GameData.Instance.TimeOfLastAnswer = 0.0f; // RESETS EVERYTHING TIME RELATED IN THE GAME
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

        _board = new Button[3, 3];
        _text = new TextMeshProUGUI[3, 3];

        _board[0, 0] = findButton("TopLeft");
        _board[0, 1] = findButton("TopMiddle");
        _board[0, 2] = findButton("TopRight");

        _board[1, 0] = findButton("CenterLeft");
        _board[1, 1] = findButton("CenterMiddle");
        _board[1, 2] = findButton("CenterRight");

        _board[2, 0] = findButton("BottomLeft");
        _board[2, 1] = findButton("BottomMiddle");
        _board[2, 2] = findButton("BottomRight");
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _text[i, j] = _board[i, j].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }
    
    public static GameState IsGameOver(string[,] board)
    {
        // Check for horizontal wins
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0] == "X" ? GameState.PlayerWin : GameState.AIWin;
            }
        }
        
        // Check for vertical wins
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                return board[0, i] == "X" ? GameState.PlayerWin : GameState.AIWin;
            }
        }
        
        // Check for diagonal wins
        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[1, 1] == "X" ? GameState.PlayerWin : GameState.AIWin;
        }
        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[1, 1] == "X" ? GameState.PlayerWin : GameState.AIWin;
        }
        
        // Check for tie
        bool tie = true;
        foreach (string text in board)
        {
            if (text == "")
            {
                tie = false;
            }
        }

        return tie ? GameState.Tie : GameState.InProgress;
    }
    
    public string[,] GetArrayGameState()
    {
        string[,] output = new string[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                output[i,j] = _text[i, j].text;
            }
        }
        return output;
    }
    
    public GameState IsGameOver()
    {
        return IsGameOver(GetArrayGameState());
    }
}
