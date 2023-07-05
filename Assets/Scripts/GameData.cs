using UnityEngine;
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public bool IsPlayerTurn { get; set; } = false;
    public bool MostRecentTurnWasPlayer { get; set; } = false;
    public bool DidPlayerWin { get; set; } = false;
    public float TimeOfLastAnswer { get; set; } = 0.0f;
    public bool IsTriviaTime { get; set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}