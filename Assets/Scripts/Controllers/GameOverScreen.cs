using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreGameOver;

    private void Start()
    {
        scoreGameOver = GetComponent<TextMeshProUGUI>();
    }

    public void setup()
    {
        gameObject.SetActive(true);
        scoreGameOver.text = "Score: " + ScoreManager.score.ToString() + " points";
    }

    public void restartGame()
    {
        GameplayController.instance.restartGame();
    }

    public void exitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
