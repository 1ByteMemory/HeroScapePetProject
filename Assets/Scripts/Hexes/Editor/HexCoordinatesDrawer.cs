using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HexCoords))]
public class HexCoordinatesDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		HexCoords coords = new HexCoords(
			property.FindPropertyRelative("x").intValue,
			property.FindPropertyRelative("z").intValue);

		position = EditorGUI.PrefixLabel(position, label);
		GUI.Label(position, coords.ToString());
	}



}
