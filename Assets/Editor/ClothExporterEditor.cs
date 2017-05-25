using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ClothExporterEditor : EditorWindow {

	private GameObject dataCharacterAsset;
	string[] lines;
	private string[] headerElements = null;
	private List <List<string>> elementList = new List<List<string>> ();

	[MenuItem ("Zombie/ClothExporterEditor")]
	static void Init() {
		CsvEditor window = EditorWindow.GetWindow<CsvEditor>();
		window.Show();
	}

	public void Initialize() {
		titleContent.text = "ClothExporter";
	}

	void OnGUI() {
		//Load data from file
		GUILayout.Label( "CharacterObject",GUILayout.Width(76f) );
		dataCharacterAsset = EditorGUILayout.ObjectField ("Character", dataCharacterAsset, typeof(GameObject), false) as GameObject;


		EditorGUILayout.TextField("Cloth name ID: ", "fc1");

		if (dataCharacterAsset == null) {
			GUILayout.Label ("Set GameObject.");
			return;
		}



		if(lines == null || lines.Length == 0) {
			GameObject[] childObjectList = dataCharacterAsset.GetComponents<GameObject> ();
		}

		//set button and function
		if (GUILayout.Button ("Output to CSV")) {
			if (headerElements.Length > 0 && elementList.Count > 0) {
				//Converting List<List<string>> into csv
				string outputCsv = string.Join(",", headerElements) + "\n";
				foreach (List<string> elementRow in elementList) {
					outputCsv += string.Join (",", elementRow.ToArray ()) + "\n";
				}
				Debug.Log (AssetDatabase.GetAssetPath(dataTextAsset));
				//output to file
				File.WriteAllText(AssetDatabase.GetAssetPath(dataTextAsset), outputCsv);
				EditorUtility.SetDirty(dataTextAsset);
			}
		}

		//===== Init datas =====

		//1. Init headers
		headerElements = lines [0].Trim ().Split (',');
		DisplayHeader ();

		//2. check if the system needs to update the content or not.
		//if it is loaded into memory don't need to load from file
		if (elementList != null && elementList.Count != 0) {
			DisplayElement();
			return;
		}
			

		//3. Init elements
		//load data into elementList.
		//must be done only once during the file load.
		for (int i = 1; i < lines.Length; i++) {
			if (lines [i] == null || lines [i].Trim () == "") {
				continue;
			}

			string[] datas = lines[i].Trim().Split(',');
			elementList.Add(new List<string> (datas));
		}

		DisplayElement();
	}

	private void DisplayHeader() {
		GUILayout.BeginHorizontal(GUI.skin.box);
		for (int i=0; i<headerElements.Length; i++) {
			string headerElem = headerElements[i];
			//IDはwidthは小さめ
			if (i == 0) {
				GUILayout.Label (headerElem, GUILayout.Width (50f));
			}
			//その他のエレメント
			else {
				GUILayout.Label (headerElem, GUILayout.Width (150f));
			}
		}
		GUILayout.EndHorizontal();
	}

	private void DisplayElement() {
		for (int i=0; i<elementList.Count; i++) {
			//datasがRow
			List<string> datas = elementList[i];
			if (datas == null && datas.Count == 0 && datas[0] == null && datas[0].Length == 0) {
				return ;
			}

			//1行を表示
			GUILayout.BeginHorizontal(GUI.skin.box);
			for (int j=0; j<datas.Count; j++) {
				
				//IDはwidthは小さめ
				if (j == 0) {
					datas[j] = GUILayout.TextField (datas[j], GUILayout.Width (50f));
				}
				//その他のエレメント
				else {
					datas[j] = GUILayout.TextField (datas[j], GUILayout.Width (150f));
				}
			}
			GUILayout.EndHorizontal();
		}
	}
}
