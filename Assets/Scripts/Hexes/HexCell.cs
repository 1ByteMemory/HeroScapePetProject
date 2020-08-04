using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HexCell : MonoBehaviour
{
	public HexCoords coords;

	public Color color;

	[SerializeField]
	HexCell[] neighbors;

	public HexCell GetNeighbor(HexDirection direction)
	{
		return neighbors[(int)direction];
	}

	public void SetNeighbor(HexDirection direction, HexCell cell)
	{
		neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;

	}

}
