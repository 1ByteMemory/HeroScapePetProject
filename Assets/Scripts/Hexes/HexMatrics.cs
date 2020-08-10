using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HexMatrics : MonoBehaviour
{
	public const float outerRadius = 10;

	public const float innerRadius = outerRadius * 0.866025404f;

	public const float solidFactor = 0.75f;

	public const float blendFactor = 1f - solidFactor;


	static Vector3[] corners = 
	{
		new Vector3(0f, 0f, outerRadius),					// NE
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),	// E
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),	// SE
		new Vector3(0f, 0f, -outerRadius),					// SW
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),	// W
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),	// NW
		
		// This is a dub of the first corner to avoid complications
		new Vector3(0f, 0f, outerRadius)
	};

	public static Vector3 GetFirstCorner (HexDirection direction)
	{
		return corners[(int)direction];
	}

	public static Vector3 GetSecondCorner (HexDirection direction)
	{
		return corners[(int)direction + 1];
	}

	public static Vector3 GetFirstSolidCorner(HexDirection direction)
	{
		return corners[(int)direction] * solidFactor;
	}

	public static Vector3 GetSecondSolidCorner(HexDirection direction)
	{
		return corners[(int)direction + 1] * solidFactor;
	}

	public static Vector3 GetBridge (HexDirection direction)
	{
		return (corners[(int)direction] + corners[(int)direction + 1]) * 0.5f * blendFactor;
	}

}
