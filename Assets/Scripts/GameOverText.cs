using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = GameData.Instance.DidPlayerWin ? "You Win!" : "You Lose!";
    }
}
