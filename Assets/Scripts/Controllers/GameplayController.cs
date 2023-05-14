using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayController : MonoBehaviour
{    
    public static GameplayController instance;
    public BoxSpawner spawner;
    public MultipleSpawnBox multipleSpawner;
    public BoxScript currentBox;
    public CameraFollow camera;
    public GameOverScreen gameOverScreen;
    private int moveCount;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        spawner.spawnBox();
    }

    void Update()
    {
        detectInput();
        pressAnyKey();
    }

    void detectInput()
    {
        if (Input.GetMouseButtonDown(0))
            if (EventSystem.current.IsPointerOverGameObject() == false)
                currentBox.dropBox();
    }

    public void spawnNewBox()
    {
        spawner.spawnBox();
    }

    public void moveCamera()
    {
        countBoxes();
    }

    public void gameOver()
    {
        gameOverScreen.setup();
    }

    public void restartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );

        ScoreManager.score = 0;
        Time.timeScale = 1f;
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void pressAnyKey()
    {
        if (Input.GetKeyDown("space"))
        {
            //for (int i = 0; i < 3; i++)
                multipleSpawner.spawnMultipleBoxes();

            ScoreManager.score += 3;
        }
    }

    private void countBoxes()
    {
        moveCount++;

        if (moveCount == 3)
        {
            moveCount = 0;
            camera.targetPos.y += 2f;
        }
    }
}