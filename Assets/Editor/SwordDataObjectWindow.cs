using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwordDataTableObject))]
public class SwordDataObjectWindow : Editor
{
	void OnEnable () {
		Debug.Log ("@@@@@@");
	}

	public override void OnInspectorGUI() {
		Debug.Log ("***: " + target);
	}

}
