using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothData : AbstractData {
	public string GroupID { get; set; }
	public string Position { get; set; }
	public string Rotation { get; set; }

	public Vector3 GetPositionVec() {
		string[] parsedPos = Position.Split ('|');
		if (parsedPos == null || parsedPos.Length < 3) {
			Debug.LogError ("Error converting Position on ClothData: " + ID);
			return Vector3.zero;
		}

		return new Vector3(float.Parse(parsedPos[0]), float.Parse(parsedPos[1]), float.Parse(parsedPos[2]));
	}

	public Vector3 GetRotationVec() {
		string[] parsedRot = Rotation.Split ('|');
		if (parsedRot == null || parsedRot.Length < 3) {
			Debug.LogError ("Error converting Rotation on ClothData: " + ID);
			return Vector3.zero;
		}

		return new Vector3(float.Parse(parsedRot[0]), float.Parse(parsedRot[1]), float.Parse(parsedRot[2]));
	}


}
