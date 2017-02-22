using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData {
	enum Gender: int {
		Male, 
		Female
	}

	enum WeaponType: int {
		Sword,
		Gun
	}

	public float HpBase { get; private set; }
	public float hpCrnt = 10.0f;


	public float AtkBase { get; private set; }
	public float atkCrnt = 10.0f;

	//次アタックするのに要する時間
	public float atkInterval = 2.0f;

	public float hanger = 100.0f;

	public Gender gender = Gender.Male;
	public WeaponType favoriteWpn = WeaponType.Sword;



}
