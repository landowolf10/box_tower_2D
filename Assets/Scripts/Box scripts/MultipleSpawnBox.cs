using UnityEngine;

public class MultipleSpawnBox : MonoBehaviour
{
    public GameObject multiBoxPrefab;
    float currentYPos;

    public void spawnMultipleBoxes()
    {
        //Maybe instantiate a different way 
        //GameObject boxObject = Instantiate(multiBoxPrefab);

        //Check x.
        Debug.Log("X multi spawned: " + BoxScript.xPos);
        //float boxColliderPosY = boxObject.GetComponent<Collider2D>().bounds.size.y;
        //currentYPos += boxColliderPosY;

        //Debug.Log("boxColliderPosY: " + boxColliderPosY);

        //boxObject.transform.position = new Vector3(BoxScript.xPos, BoxScript.yPos + currentYPos);
    }
}