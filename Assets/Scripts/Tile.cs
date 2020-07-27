using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    Material defaultMat;

    [HideInInspector]
    public bool isHighlighted;
    [HideInInspector]
    public Material newMat;


    // Start is called before the first frame update
    void Start()
    {
        defaultMat = GetComponentsInChildren<Renderer>()[1].material;
        newMat = defaultMat;
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeToDefault();
    }

    public void ChangeToDefault()
	{
        GetComponentsInChildren<Renderer>()[1].material = defaultMat;
        /*
        if (newMat != defaultMat)
        {
            //isHighlighted = false;
            Debug.Log("Changing back to default");
            newMat = defaultMat;
        }*/
    }

    public void ChangeMaterial(Material material)
	{
        GetComponentsInChildren<Renderer>()[1].material = material;
        
        //isHighlighted = true;
        //newMat = material;

    }

}
