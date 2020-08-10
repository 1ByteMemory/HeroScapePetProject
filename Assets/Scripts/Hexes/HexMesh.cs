using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
	Mesh mesh;
	MeshCollider meshCol;
	List<Vector3> vertices;
	List<int> triangles;
	List<Color> colors;

	private void Awake()
	{
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		meshCol = gameObject.AddComponent<MeshCollider>();

		mesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		colors = new List<Color>();
		triangles = new List<int>();


	}


	public void Triangulate(HexCell[] cells)
	{
		mesh.Clear();
		vertices.Clear();
		colors.Clear();
		triangles.Clear();

		// enums have int values, therfore you use a for loop like this to iterate through each enum state
		

		for (int i = 0; i < cells.Length; i++)
		{
			for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
			{
				Triangulate(d, cells[i]);
			}
		}

		mesh.vertices = vertices.ToArray();
		mesh.colors = colors.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();


		//Assign mesh collider after finishing triangulating
		meshCol.sharedMesh = mesh;

		

	}

	void Triangulate(HexDirection direction, HexCell cell)
	{
		Vector3 center = cell.transform.localPosition;
		Vector3 v1 = center + HexMatrics.GetFirstSolidCorner(direction);
		Vector3 v2 = center + HexMatrics.GetSecondSolidCorner(direction);


		AddTriangle(center, v1, v2);
		AddTriangleColor(cell.color);

		Vector3 bridge = HexMatrics.GetBridge(direction);

		Vector3 v3 = v1 + bridge;
		Vector3 v4 = v2 + bridge;

		AddQuad(v1, v2, v3, v4);

		HexCell prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
		HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
		HexCell nextneighbor = cell.GetNeighbor(direction.Next()) ?? cell;


		Color bridgeColor = (cell.color + neighbor.color) * 0.5f;

		AddQuadColor(cell.color, bridgeColor);

		AddTriangle(v1, center + HexMatrics.GetFirstCorner(direction), v3);
		AddTriangleColor
		(
			cell.color,
			(cell.color + prevNeighbor.color + neighbor.color) / 3f,
			bridgeColor
		);

		AddTriangle(v2, v4, center + HexMatrics.GetSecondCorner(direction));
		AddTriangleColor
		(
			cell.color,
			bridgeColor,
			(cell.color + nextneighbor.color + neighbor.color) / 3f
		);


	}

	void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);

		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);

	}

	void AddTriangleColor(Color c1, Color c2, Color c3)
	{
		colors.Add(c1);
		colors.Add(c2);
		colors.Add(c3);
	}
	void AddTriangleColor(Color color)
	{
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
	}

	void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
	{
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		vertices.Add(v4);

		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 2);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
		triangles.Add(vertexIndex + 3);

	}

	void AddQuadColor(Color c1, Color c2)
	{
		colors.Add(c1);
		colors.Add(c1);
		colors.Add(c2);
		colors.Add(c2);
	}
}
