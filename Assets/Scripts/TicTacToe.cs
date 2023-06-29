using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.SocialPlatforms.Impl;

struct MiniMaxResult
{
    public int score;
    public int x;
    public int y;

    public MiniMaxResult(int score, int x, int y)
    {
        this.score = score;
        this.x = x;
        this.y = y;
    }
}

public enum GameState
{
    PlayerWin,
    AIWin,
    Tie,
    InProgress
}

public class TicTacToeBoard
{
    private Button[,] _board = new Button[3, 3];
    private TextMeshProUGUI[,] _text = new TextMeshProUGUI[3, 3];
    
    public TicTacToeBoard(Button[,] board)
    {
        _board = board;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _text[i, j] = _board[i, j].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
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

    private int GetPlayerValue(string player)
    {
        // AI = 1
        // Human player = -1
        // Blank space = 0
        
        return player == "O" ? 1 : player == "X" ? -1 : 0;
    }

    private (int, int) GetBoardDifference(string[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i,j] != _text[i, j].text)
                {
                    return (i, j);
                }
            }
        }
        throw new Exception("No difference found between boards");
    }

    private MiniMaxResult minimax(string[,] board, int depth, string player = "O")
    {
        //https://github.com/Cledersonbc/tic-tac-toe-minimax/blob/master/py_version/minimax.py
        MiniMaxResult result = new MiniMaxResult();
        result.x = -1;
        result.y = -1;
        if (player == "O")
            result.score = -1000;
        else
            result.score = 1000;
        
        if (depth == 0 || IsGameOver() != GameState.InProgress)
        {
            result.score = (depth+1) * GetPlayerValue(player);
            return result;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i,j] == "")
                {
                    // put the player in the empty space
                    board[i, j] = player;
                    MiniMaxResult tempResult = new MiniMaxResult(
                        minimax(board, depth - 1, player == "O" ? "X" : "O").score,
                        i,
                        j
                    );
                    board[i, j] = "";
                    
                    if (player == "O")
                    {
                        if (tempResult.score > result.score)
                        {
                            result.score = tempResult.score;
                            result.x = i;
                            result.y = j;
                        }
                    }
                    else
                    {
                        if (tempResult.score < result.score)
                        {
                            result.score = tempResult.score;
                            result.x = i;
                            result.y = j;
                        }
                    }
                }
            }
        }

        return result;
    }

    public void MakeAiMove()
    {
        string[,] board = GetArrayGameState();
        int moves = 0;
        foreach (string text in board)
        {
            if (text != "")
            {
                moves++;
            }
        }

        if (moves == 0)
        {
            _text[0, 0].text = "O";
        }
        else
        {
            MiniMaxResult bestAIMove = minimax(board, 9-moves);
            _text[bestAIMove.x, bestAIMove.y].text = "O";
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
    
    public GameState IsGameOver()
    {
        return IsGameOver(GetArrayGameState());
    }
}
public class TicTacToe : MonoBehaviour
{
    private TicTacToeBoard _board;

    // Update is called once per frame
    private void Update()
    {
        // TODO: MOVE THE BELOW TO A MANUAL CHECK WHEN AI MAKES MOVE OR USER MAKES MOVE
        // SHOULD SAVE PROCESSING POWER
        if (_board.IsGameOver() != GameState.InProgress)
        {
            // TODO: NEED TO DO SOMETHING FOR TIES HERE
            Debug.Log("GameOver");
            SceneManager.LoadScene("GameOver");
            GameData.Instance.DidPlayerWin = GameData.Instance.MostRecentTurnWasPlayer;
            GameData.Instance.TimeOfLastAnswer = 0.0f; // RESETS EVERYTHING TIME RELATED IN THE GAME
        }
    }
    
    public void MakeAIMove()
    {
        _board.MakeAiMove();
        GameData.Instance.MostRecentTurnWasPlayer = false;
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
        
        Button[,] temp_board = new Button[3, 3];
        
        temp_board[0, 0] = findButton("TopLeft");
        temp_board[0, 1] = findButton("TopMiddle");
        temp_board[0, 2] = findButton("TopRight");

        temp_board[1, 0] = findButton("CenterLeft");
        temp_board[1, 1] = findButton("CenterMiddle");
        temp_board[1, 2] = findButton("CenterRight");

        temp_board[2, 0] = findButton("BottomLeft");
        temp_board[2, 1] = findButton("BottomMiddle");
        temp_board[2, 2] = findButton("BottomRight");
        
        _board = new TicTacToeBoard(temp_board);
    }
}
