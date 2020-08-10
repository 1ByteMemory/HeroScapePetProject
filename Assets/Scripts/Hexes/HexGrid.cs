using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;

	public Color defaultColor = Color.white;
	public Color touchedColor = Color.red;

    public HexCell cellPrfab;

	public Text cellLabelPrefab;
	Canvas gridCanvas;

	HexMesh hexMesh;

    HexCell[] cells;

	Camera cam;

	private void Awake()
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
		


		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}
	}

	private void Start()
	{
		cam = Camera.main;
		hexMesh.Triangulate(cells);
	}




	public void ColorCell(Vector3 position, Color color)
	{
		position = transform.InverseTransformPoint(position);
		HexCoords coords = HexCoords.FromPosition(position);

		// Gets the index of the cell
		// (I don't know why this works)
		int index = coords.X + coords.Z * width + coords.Z / 2;

		// Checks that index is in the range of the array
		if (index >= cells.Length || index < 0)
			return;

		// get the cell using index
		HexCell cell = cells[index];

		cell.color = color;
		hexMesh.Triangulate(cells);

	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMatrics.innerRadius * 2);
		position.y = 0;
		position.z = z * (HexMatrics.outerRadius * 1.5f);

		// Create hex cells
		HexCell cell = cells[i] = Instantiate(cellPrfab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coords = HexCoords.FromOffsetCoords(x, z);

		cell.color = defaultColor;

		if (x > 0)
		{
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
		if (z > 0)
		{
			// this is a bitwise opertator. It's the sam as &&, but for each bit. 
			// both bits of a pair need to be 1 for the result to be 1. 
			// For example, 10101010 & 00001111 yields 00001010.

			// This one checks if the numbers are even. To check for odd numbers, change the 0 to 1
			if ((z & 1) == 0)
			{
				// Connect the SE cells
				cell.SetNeighbor(HexDirection.SE, cells[i - width]);

				// conect the SW cells (expect the first one)
				if (x > 0)
				{
					cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
				}
			}
			else
			{
				cell.SetNeighbor(HexDirection.SW, cells[i - width]);

				if (x < width - 1)
				{
					cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
				}
			}
		}

		if (gridCanvas != null)
		{
			// Make hex cells more visable
			Text label = Instantiate(cellLabelPrefab);
			label.rectTransform.SetParent(gridCanvas.transform, false);
			label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
			label.text = cell.coords.ToStringOnSeperateLines();
		}
	}

}
