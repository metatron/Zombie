using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	public string ID { get; set; }

	public string Name { get; set; }

	private int _level = 1;
	public int Level { get {return _level; } set { _level = value; } }

	public float HpBase { get; set; }
	public float hpCrnt = 10.0f;

	[System.NonSerialized]
	private RarityData _rarityData;
	public RarityData RarityData { get { return _rarityData; } set { _rarityData = value; } }

	private int _rarity = 1;
	public int Rarity { get {return _rarity; } set { _rarity = value; } }

	public float MinAtk { get; set; }
	public float MaxAtk { get; set; }

	public float CrntAtk() {
		if (_rarityData == null) {
			_rarityData = RarityDataTableObject.Instance.Table.All.FirstOrDefault (rarityData => rarityData.Rarity == ""+Rarity);
		}

		float crntAtk = CharacterLevelSystem.CalcLevelParameter(MinAtk, MaxAtk, _level, _rarityData.MaxLevel);
		return crntAtk;
	}

	//次アタックするのに要する時間（NPCのみ）
	public float AtkInterval { get; private set; }
	public float atkIntervalCrnt = 2.0f;

	//空腹度
	public float hanger = 100.0f;

	//体の見た目
	public string BodyPrefab { get; set; }


	//バトルの位置。0は参加しない（NPC ONLY）
	private int _battlePos = -1;
	public int BattlePosition { get{ return _battlePos; } set{ _battlePos = value; } }

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
