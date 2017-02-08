using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDataTableObject : MonoBehaviour {
	SwordDataTable table = new SwordDataTable();
	public void InitData() {
		table.Load ();
		SwordData swordData = table.All [0];
		Debug.LogError (swordData.ID + ", " + swordData.Damage);
	}

	void Start() {
		InitData ();
	}

}
