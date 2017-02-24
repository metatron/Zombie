using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaData {
	public enum Gender: int {
		Male, 
		Female
	}

	public enum WeaponType: int {
		Sword,
		Gun
	}

	public float HpBase { get; private set; }
	public float hpCrnt = 10.0f;


	public float AtkBase { get; private set; }
	public float atkCrnt = 10.0f;

	//次アタックするのに要する時間（NPCのみ）
	public float AtkInterval { get; private set; }
	public float atkIntervalCrnt = 2.0f;

	//空腹度
	public float hanger = 100.0f;

	//体の見た目
	public string BodyPrefab { get; set; }


	//バトルに参加するかしないか（NPC ONly）
	public bool isBattleMode { get; set; }


	public Gender gender = Gender.Male;
	public WeaponType favoriteWpn = WeaponType.Sword;

}
