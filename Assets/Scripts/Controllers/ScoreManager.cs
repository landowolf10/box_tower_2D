using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static TextMeshProUGUI scoreText;
    public static int score = 0;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + score.ToString() + " points";
    }
}
