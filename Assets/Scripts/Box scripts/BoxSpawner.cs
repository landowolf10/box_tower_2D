using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    private Color32[] colors = { 
        new Color32(16, 179, 114, 255),
        new Color32(115, 255, 199, 255),
        new Color32(179, 125, 114, 255),
        new Color32(255, 52, 13, 255) 
    };
    public static Color newColor;

    public GameObject boxPrefab;
    public static GameObject boxObject;

    public void spawnBox()
    {
        boxObject = Instantiate(boxPrefab);

        newColor = colors[Random.Range(0, colors.Length)];
        
        boxObject.GetComponent<SpriteRenderer>().color = newColor;
        Vector3 temp = transform.position;

        temp.z = 0;
        boxObject.transform.position = temp;
    }

    public GameObject getBoxPrefab()
    {
        return boxPrefab;
    }
}

//Proactivo
//Dosificado
//Que no me haga daño

//¿Qué emociones son más sencillas de expresar para mi?
//Hablar de como me siento. Ponerle palabras.
//Vulnerabilidad, no sé como expresar mi vulnerabilidad.
//Ser vulnerable es complicado