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

		GUILayoutOption[] options = new GUILayoutOption[]{ GUILayout.MaxWidth(100.0f), GUILayout.MinWidth(10.0f)};

		EditorGUILayout.BeginHorizontal( GUI.skin.box );
		{
			foreach (string element in swordDataTable.HeaderElements) {
				EditorGUILayout.LabelField (element, options);
			}
		}
		EditorGUILayout.EndHorizontal ();

		foreach (SwordData data in swordDataList) {
			EditorGUILayout.BeginHorizontal (GUI.skin.box);
			{
				EditorGUILayout.LabelField (""+data.ID, options);
				EditorGUILayout.LabelField (data.Name, options);
				EditorGUILayout.LabelField (""+data.Damage, options);
				EditorGUILayout.LabelField (""+data.ReachLength, options);
				EditorGUILayout.LabelField (""+data.Image, options);
				EditorGUILayout.LabelField (data.RequirementStr, options);
			}
			EditorGUILayout.EndHorizontal ();
		}
	}
}
