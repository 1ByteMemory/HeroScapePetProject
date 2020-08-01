using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour
{
    public HexGrid grid;
    public Color[] colors;

    private Color activeColor;
    private Camera cam;

	private void Awake()
	{
        cam = Camera.main;

        SelectColor(0);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
            HandleInput();
		}
    }

    void HandleInput()
    {
        Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(inputRay, out RaycastHit hit))
            grid.ColorCell(hit.point, activeColor);
    }


    public void SelectColor(int index)
	{
        activeColor = colors[index];
	}

}
