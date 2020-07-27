using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public int moveSpaces = 2;
    public Material availableSpacesMat;
    public Material landingSpaceMat;

    GameObject[] availableSpaces;

    int spacesUsed = 0;
    RaycastHit rayHit;

    // The size of the hexs
    const float hexSize = 1.72f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
            ShowAvailableSpaces(true);
		}
        if (Input.GetKeyUp(KeyCode.Space))
		{
            ShowAvailableSpaces(false);
		}


    }

    void ShowAvailableSpaces(bool isHighlighting)
	{
        Ray ray = new Ray(transform.position + new Vector3(0, -0.3f, 0), transform.forward);

        // Raycast to get all space the character can move to
        while (Physics.Raycast(ray, out rayHit, hexSize) && spacesUsed < moveSpaces)
		{
            Debug.Log(spacesUsed);
            spacesUsed++;

            Vector3 newPos = rayHit.transform.position + new Vector3(0, -0.05f);

            // Change Ray to be sent out from rayHit tile
            ray = new Ray(newPos, transform.forward);

            if (isHighlighting)
                rayHit.transform.GetComponent<Tile>().ChangeMaterial(availableSpacesMat);
            else
                rayHit.transform.GetComponent<Tile>().ChangeToDefault();

		}

        spacesUsed = 0;
	}


	void Move(int moveSpaces)
	{

	}


}
