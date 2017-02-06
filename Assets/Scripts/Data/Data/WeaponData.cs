using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : AbstractData {
	public string ID { get; private set; }
	public string Name { get; private set; }
	public int Atk { get; private set; }

	//exp: 
	//Sword1
	//Metal:3|Wood:1
	public string MaterialListStr { get; private set; }

	public List<MaterialData> MaterialList = new List<MaterialData>();

	public void InitMaterialList() {
		
	}
}
