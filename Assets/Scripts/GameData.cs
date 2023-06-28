using UnityEngine;
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public char CurrentPlayer { get; set; } = 'X';
    public bool DidPlayerWin { get; set; } = false;
    public float TimeOfLastAnswer { get; set; } = 0.0f;

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