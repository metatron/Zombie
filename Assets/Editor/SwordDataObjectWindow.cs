using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwordDataTableObject))]
public class SwordDataObjectWindow : Editor
{
	private SwordDataTable swordDataTable;
	private List<SwordData> swordDataList;

	void OnEnable () {
		Debug.Log ("@@@@@@");
		((SwordDataTableObject)target).InitData ();
		swordDataTable = ((SwordDataTableObject)target).Table;
		swordDataList = swordDataTable.All;
	}

	public override void OnInspectorGUI() {
		if (swordDataList == null || swordDataList.Count == 0) {
			return;
		}

		foreach (SwordData swordData in swordDataList) {
//			EditorGUILayout.BeginHorizontal(GUILayoutOption.Equals
		}
	}
}
