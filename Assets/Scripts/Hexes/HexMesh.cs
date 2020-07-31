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

		for (int i = 0; i < cells.Length; i++)
		{
			Triangulate(cells[i]);
		}

		mesh.vertices = vertices.ToArray();
		mesh.colors = colors.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();


		//Assign mesh collider after finishing triangulating
		meshCol.sharedMesh = mesh;

	}

	void Triangulate(HexCell cell)
	{
		Vector3 center = cell.transform.localPosition;

		for (int i = 0; i < 6; i++)
		{
			AddTriangle(center, center + HexMatrics.corners[i], center + HexMatrics.corners[i + 1]);
			AddTriangleColor(cell.color);
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

	void AddTriangleColor(Color color)
	{
		colors.Add(color);
		colors.Add(color);
		colors.Add(color);
	}
}
