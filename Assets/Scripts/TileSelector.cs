using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileSelector : MonoBehaviour
{
    public Material highlightedMaterial;

    public TextMeshProUGUI text;

    GameObject[] tilesSelected;
    GameObject selectedCharacter;

    public float selectedRaiseAmount = 0.5f;

    int availableMoves;

    RaycastHit tileHitInfo;
    RaycastHit charHitInfo;
    Camera cam;
    public LayerMask tileLayermask;
    public LayerMask characterLayermask;

    Movement charMovement;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!SelectCharacter() && selectedCharacter != null)
			{
                if (charMovement.spacesUsed < availableMoves)
                {
                    // Add the tile to list useing moves left as the index
                    //tilesSelected[charMovement.spacesUsed] = SelectTile();
                    SelectTile();

                    charMovement.spacesUsed++;

                    text.text = (availableMoves - charMovement.spacesUsed).ToString();
                }
            }
        }
    }



    bool SelectCharacter()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out charHitInfo, 100, characterLayermask))
        {
            if (charHitInfo.transform.gameObject != selectedCharacter)
			{
                if (selectedCharacter != null)
                {
                    // Reset position of previously selected character
                    selectedCharacter.transform.position += new Vector3(0, -selectedRaiseAmount);
                }

                // switch the the newly selected character
                selectedCharacter = charHitInfo.transform.gameObject;

                // Raise the newly selected character to show that it's been selected
                selectedCharacter.transform.position += new Vector3(0, selectedRaiseAmount);

                // set available moves to how much the character can move
                charMovement = selectedCharacter.GetComponent<Movement>();


                availableMoves = charMovement.moveSpaces - charMovement.spacesUsed;

                tilesSelected = new GameObject[availableMoves];

                // Show how many moves are available for that character
                text.text = availableMoves.ToString();

                return true;
			}
        }
        return false;
    }


    GameObject SelectTile()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out tileHitInfo, 100, tileLayermask) && tileHitInfo.transform.GetComponent<Tile>() != null)
        {
            //hitInfo.transform.GetComponent<Tile>().isHighlighted = true;
            tileHitInfo.transform.GetComponent<Tile>().ChangeMaterial(highlightedMaterial);

            return tileHitInfo.transform.gameObject;
        }

        return null;
    }

}
