using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

struct MiniMaxResult
{
    public int score;
    public int x;
    public int y;
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

    private MiniMaxResult minimax(string[,] board, string player = "O", int depth = 0)
    {
        MiniMaxResult result = new MiniMaxResult();
        if (IsGameOver(board))
        {
            // the below works since we're only ever running MiniMax on an "O" player's turn, hence the static "O"
            // What this does is check if the player who is winning is the "O" player -- if it's him, then the score
            // will be positive, and if it's the "X" player, then the score will be negative
            
            // we're also weighting the score by the depth of the recursion, so the AI will prefer to win sooner
            result.score = (9-depth) * GetPlayerValue(player) * GetPlayerValue("O");
            return result;
        } 
        
        bool move = false;
        int score = -2;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i,j] == "")
                {
                    string[,] tempBoard = new string[3, 3];
                    // Copy board
                    for (int x = 0; x < 3; x++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            tempBoard[x, z] = board[x, z];
                        }
                    }
                    
                    tempBoard[i, j] = player;
                    MiniMaxResult temp_result = minimax(tempBoard, player == "O" ? "X" : "O", depth + 1);
                    if (-temp_result.score > score)
                    {
                        result.score = -temp_result.score;
                        result.x = i;
                        result.y = j;
                        move = true;
                    }
                }
            }
        }
        
        if (!move)
        {
            result = new MiniMaxResult();
            result.score = 0;
        }

        return result;
    }

    public void MakeAiMove()
    {
        MiniMaxResult bestAIMove = minimax(GetArrayGameState());
        _text[bestAIMove.x, bestAIMove.y].text = "O";
    }
    
    public bool IsGameOver(string[,] board)
    {
        // Check for horizontal wins
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return true;
            }
        }
        
        // Check for vertical wins
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                return true;
            }
        }
        
        // Check for diagonal wins
        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return true;
        }
        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return true;
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
        return tie;
    }
    
    public bool IsGameOver()
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
        if (_board.IsGameOver())
        {
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
