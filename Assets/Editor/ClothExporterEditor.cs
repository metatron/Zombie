using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ClothExporterEditor : EditorWindow {

	private GameObject dataCharacterAsset;

	[MenuItem ("Zombie/ClothExporterEditor")]
	static void Init() {
		ClothExporterEditor window = EditorWindow.GetWindow<ClothExporterEditor>();
		window.Show();
	}

	public void Initialize() {
		titleContent.text = "ClothExporter";
	}

	void OnGUI() {
		//Load data from file
		dataCharacterAsset = Selection.activeGameObject;

		string groupId = EditorGUILayout.TextField("Cloth name ID: ", "fc1");

		if (dataCharacterAsset == null) {
			return;
		}

		GUILayout.Label ("Creating Cloth Data On: " + dataCharacterAsset.name + ", groupId: " + groupId,GUILayout.Width(300f) );

		string csvText = "";
		Transform[] childObjectList = dataCharacterAsset.GetComponentsInChildren<Transform> ();
		foreach (Transform childObject in childObjectList) {
			if (!childObject.name.Contains (groupId)) {
				continue;
			}

			csvText += childObject.name + "," + 
			groupId + "," + 
			childObject.name + "," + 
			childObject.name + "," + 
			childObject.localPosition.x + "|" + childObject.localPosition.y + "|" + childObject.localPosition.z + "," + 
			childObject.localRotation.x + "|" + childObject.localRotation.y + "|" + childObject.localRotation.z + "," + 
			childObject.GetComponent<SpriteRenderer>().sortingOrder + "\n";
		}


		if(string.IsNullOrEmpty(csvText)) {
			return;
		}

		GUIStyle areaTextStyle = new GUIStyle ();
		areaTextStyle.stretchHeight = true;
		areaTextStyle.stretchWidth = true;
		GUI.TextArea(new Rect(10, 50, 600, 500), csvText, areaTextStyle);

	}
}
