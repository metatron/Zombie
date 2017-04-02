using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharaData {
	public enum Gender: int {
		Male, 
		Female
	}

	public enum WeaponType: int {
		Sword,
		Gun
	}

	public string Name { get; set; }

	public float HpBase { get; private set; }
	public float hpCrnt = 10.0f;

	public float MinAtk { get; private set; }
	public float MaxAtk { get; private set; }

	//次アタックするのに要する時間（NPCのみ）
	public float AtkInterval { get; private set; }
	public float atkIntervalCrnt = 2.0f;

	//空腹度
	public float hanger = 100.0f;

	//体の見た目
	public string BodyPrefab { get; set; }


	//バトルの位置。0は参加しない（NPC ONLY）
	public int BattlePosition { get; set; }

	//セットされている剣のID
	public string SwordID { get; set; }
	//セットされている銃のID
	public string GunID { get; set; }


	//怪我をしているかどうか
	private bool _injured = false;
	public bool Injured { get {return _injured; } set { _injured = value; } }


	public int CurrentExp { set; get; }



	public Gender gender = Gender.Male;
	public WeaponType favoriteWpn = WeaponType.Sword;

}
