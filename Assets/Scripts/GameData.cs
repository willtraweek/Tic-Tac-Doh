using UnityEngine;
public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    
    public bool IsClickable { get; set; } = true;
    public char CurrentPlayer { get; set; } = 'X';
    public bool DidWin { get; set; } = false;

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