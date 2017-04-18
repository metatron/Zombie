using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(PlayerObject))]
public class CharacterObjectDisplayer : Editor {
	
	public override void OnInspectorGUI() {
//		InitCsvDatas ();

		AbstractCharacterObject charaObj = target as AbstractCharacterObject;
		CharaData charaData = charaObj.charaData;

		EditorGUILayout.LabelField( "体力(現在/最大)" );
		EditorGUILayout.BeginHorizontal();
		charaData.hpCrnt     = EditorGUILayout.FloatField( charaData.hpCrnt, GUILayout.Width(48) );
		charaData.HpBase     = EditorGUILayout.FloatField( charaData.HpBase, GUILayout.Width(48) );
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField( "攻撃力(最小/最大)" );
		EditorGUILayout.BeginHorizontal();
		charaData.MinAtk     = EditorGUILayout.FloatField( charaData.MinAtk, GUILayout.Width(48) );
		charaData.MaxAtk     = EditorGUILayout.FloatField( charaData.MaxAtk, GUILayout.Width(48) );
		EditorGUILayout.FloatField( charaData.CrntAtk(), GUILayout.Width(48) );
		EditorGUILayout.EndHorizontal();
	}

	protected void InitCsvDatas() {
		//データ初期化
		SwordDataTableObject.Instance.InitData ();
		GunDataTableObject.Instance.InitData ();
		CraftItemDataTableObject.Instance.InitData ();
		StageDataTableObject.Instance.InitData ();
		RarityDataTableObject.Instance.InitData ();
		ExperienceDataTableObject.Instance.InitData ();
	}
}
