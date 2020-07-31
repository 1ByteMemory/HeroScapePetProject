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

	private void Update()
	{
		if (Input.GetMouseButton(0))
			HandleInput();
	}

	void HandleInput()
	{
		Ray inputRay = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(inputRay, out RaycastHit hit))
			TouchCell(hit.point);
	}

	void TouchCell(Vector3 position)
	{
		position = transform.InverseTransformPoint(position);
		HexCoords coords = HexCoords.FromPosition(position);
		Debug.Log(coords.ToString());

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

		// Make hex cells more visable
		Text label = Instantiate(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = cell.coords.ToStringOnSeperateLines();


	}

}
