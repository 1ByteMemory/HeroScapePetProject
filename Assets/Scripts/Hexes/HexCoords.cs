using System;
using UnityEngine;

[System.Serializable]
public struct HexCoords
{
	[SerializeField]
	private int x, z;

	public int X { get { return x; } }
	public int Z { get { return z; } }

	public int Y { get { return -X - Z; } }

	public HexCoords(int x, int z)
	{
		this.x = x;
		this.z = z;
	}

	public static HexCoords FromOffsetCoords (int x, int z)
	{
		return new HexCoords(x - z / 2, z);
	}

	public static HexCoords FromPosition(Vector3 position)
	{
		float x = position.x / (HexMatrics.innerRadius * 2f);
		float y = -x;

		float offest = position.z / (HexMatrics.outerRadius * 3f);
		x -= offest;
		y -= offest;

		int ix = Mathf.RoundToInt(x);
		int iy = Mathf.RoundToInt(y);
		int iz = Mathf.RoundToInt(-x -y);

		// rounding errors occour at hex edges
		if (ix + iy + iz != 0)
		{
			// discard the coordinate with the largest rounding delta, 
			// reconstruct it from the other two.
			// as we only need X and Z, we don't need to bother with reconstructing Y.

			float dX = Mathf.Abs(x - ix);
			float dY = Mathf.Abs(y - iy);
			float dZ = Mathf.Abs(-x - x - iz);

			if (dX > dY && dX > dZ)
			{
				ix = -iy - iz;
			} else if (dZ > dY)
			{
				iz = -ix - iy;
			}

		}

		return new HexCoords(ix, iz);

	}

	public override string ToString()
	{
		return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
	}

	public string ToStringOnSeperateLines()
	{
		return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
	}

}
