using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClothData : AbstractData {
	public string GroupID { get; set; }
	public string Position { get; set; }
	public string Rotation { get; set; }
	public int OrderInLayer { get; set; }

	public Vector3 GetPositionVec() {
		Debug.LogError ("=====Position: " + Position);
		string[] parsedPos = Position.Split ('|');
		if (parsedPos == null || parsedPos.Length < 3) {
			Debug.LogError ("Error converting Position on ClothData: " + ID);
			return Vector3.zero;
		}

		double x = double.Parse (parsedPos [0]);
		double y = double.Parse (parsedPos [1]);
		double z = double.Parse (parsedPos [2]);
		Debug.LogError ("@@@@@@@@@@@@@@@@@@@@@1: " + (float)x + ", " + (float)y + ", " + (float)z);
		return new Vector3((float)x, (float)y, (float)z);
	}

	public Vector3 GetRotationVec() {
		Debug.LogError ("=====Rotation: " + Rotation);
		string[] parsedRot = Rotation.Split ('|');
		if (parsedRot == null || parsedRot.Length < 3) {
			Debug.LogError ("Error converting Rotation on ClothData: " + ID);
			return Vector3.zero;
		}

		double x = double.Parse (parsedRot [0]);
		double y = double.Parse (parsedRot [1]);
		double z = double.Parse (parsedRot [2]);
		Debug.LogError ("@@@@@@@@@@@@@@@@@@@@@2: " + (float)x + ", " + (float)y + ", " + (float)z);
		return new Vector3((float)x, (float)y, (float)z);
	}


}
