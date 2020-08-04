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


	void Triangulate(HexCell cell)
	{
		// enums have int values, therfore you use a for loop like this to iterate through each enum state
		for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++)
		{
			Triangulate(d, cell);
		}

	}

	void Triangulate(HexDirection direction, HexCell cell)
	{
		Vector3 center = cell.transform.localPosition;

		for (int i = 0; i < 6; i++)
		{
			AddTriangle(
				center,
				center + HexMatrics.GetFirstCorner(direction),
				center + HexMatrics.GetSecondCorner(direction));

			HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
			AddTriangleColor(cell.color, neighbor.color, neighbor.color);
		}
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
}
