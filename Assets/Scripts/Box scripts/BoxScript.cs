using UnityEngine;
using static Unity.VisualScripting.Member;

public class BoxScript : MonoBehaviour
{
    private float minX = -1.95f;
    private float maxX = 1.95f;
    private float moveSpeed = 2f;
    private string suffix = " points";

    private Rigidbody2D rb;
    private Collider2D boxCollider;
    private Bounds boxBounds;

    private bool canMove;
    private bool gameOver;
    private bool ignoreCollision;

    public static GameObject[] boxPosition;
    public static float xPos;
    public static float yPos;
    public AudioClip landAudio;
    public AudioClip gameOverAudio;
    AudioSource clip;

    public GameObject landedParticle;
    ParticleSystem ps;
    public ParticleSystem particleSystemPrefab;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        clip = GetComponent<AudioSource>();
        boxCollider = GetComponent<Collider2D>();
        //ps = GetComponent<ParticleSystem>();
        rb.gravityScale = 0;
    }

    void Start()
    {
        canMove = true;

        if (Random.Range(0, 2) > 0)
            moveSpeed *= -1f;

        GameplayController.instance.currentBox = this;
    }

    void Update()
    {
        moveBox();
    }

    void moveBox()
    {
        //Debug.Log("Game over? " + gameOver);

        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > maxX)
                moveSpeed *= -1f;
            else if (temp.x < minX)
                moveSpeed *= -1f;

            transform.position = temp;
        }
    }

    public void dropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    void landed()
    {
        if (!gameOver)
        {
            ignoreCollision = true;

            GameplayController.instance.spawnNewBox();
            GameplayController.instance.moveCamera();
        }
        else
        {
            ignoreCollision = false;
            gameOver = true;
        }

        
    }

    private void getObjectPosition()
    {
        boxPosition = GameObject.FindGameObjectsWithTag("Box");
        
        foreach (GameObject obj in boxPosition)
        {
            xPos = obj.transform.position.x;
            yPos = obj.transform.position.y;
            boxPosition = new GameObject[0];
        }

        //Debug.Log("Landed x position: " + xPos);
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform" || target.gameObject.tag == "Box")
        {
            clip.PlayOneShot(landAudio);
            Invoke("landed", 0.5f);
            getObjectPosition();
            ignoreCollision = true;
            ScoreManager.score++;
            ParticleSystem newParticleSystem = Instantiate(particleSystemPrefab);
            
            newParticleSystem.transform.position = transform.position;

            var main = newParticleSystem.main;
            main.startColor = BoxSpawner.newColor;

            Instantiate(landedParticle, transform.position, transform.rotation); 

            if (ScoreManager.score == 1)
                suffix = " point";

            ScoreManager.scoreText.text = "Score: " + ScoreManager.score.ToString() + suffix;

            boxBounds = boxCollider.bounds;

            Debug.Log("boxBounds: " + boxBounds);

            // Store a reference to the last instantiated prefab
            GameObject lastInstantiatedBox = GameObject.FindGameObjectWithTag("Box");
            Bounds lastBoxBounds = lastInstantiatedBox.GetComponent<Collider2D>().bounds;

            Vector2 sizeDifference = lastBoxBounds.size - boxBounds.size;

            // Do something with the size difference
            //Debug.Log("Size difference: " + sizeDifference);

            //if (sizeDifference.x <= 0.01)
                //ScoreManager.score += 5;

            //float leftEdge = boxBounds.center.x - boxBounds.extents.x;
            //float rightEdge = boxBounds.center.x + boxBounds.extents.x;

                //Get bounds of last object and current object

                //Debug.Log("Left edge: " + leftEdge);
                //Debug.Log("Right edge: " + rightEdge);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "GameOver")
        {
            clip.PlayOneShot(gameOverAudio);
            gameOver = true;
            canMove = false;
            GameplayController.instance.gameOver();
            CancelInvoke("landed");
        }
    }
}