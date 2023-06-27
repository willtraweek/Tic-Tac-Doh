using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = GameData.Instance.DidWin ? "You Win!" : "You Lose!";
    }
}
