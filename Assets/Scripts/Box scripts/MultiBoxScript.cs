using UnityEngine;

public class MultiBoxScript : MonoBehaviour
{
    private bool gameOver;
    private bool ignoreCollision;

    //public static Vector3 boxPosition;

    void multiStack()
    {
        if (gameOver)
            return;

        ignoreCollision = true;

        GameplayController.instance.moveCamera();
    }

    void restartGame()
    {
        GameplayController.instance.restartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform" || target.gameObject.tag == "Box")
        {
            Invoke("multiStack", 2f);
            //boxPosition = GameObject.FindGameObjectWithTag("Box").transform.position;
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "GameOver")
        {
            CancelInvoke("landed");

            gameOver = true;

            Invoke("restartGame", 2f);
        }
    }
}