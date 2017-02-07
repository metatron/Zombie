using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof(SwordTable) )]
public class NewBehaviourScript : Editor {
	public override void OnInspectorGUI() {
		SwordTable data = target as SwordTable;
		data.Load ();
	}}
