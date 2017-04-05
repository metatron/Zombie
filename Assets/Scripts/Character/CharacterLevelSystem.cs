using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevelSystem {
	public enum LevelPattern: int {
		Normal = 1,     //普通
		Early  = 2,     //早熟
		Late   = 3      //晩成
	}

	/**
	 * 
	 * キャラクターのレベルによるパラメータを取得。（ATK, etc）
	 * 
	 */
	public static int CalcLevelParameter(float min, float max, int level, int maxLevel, LevelPattern pattern = LevelPattern.Normal, bool overEnable = false) {
		//上限Lvを無視するパラメータ取得以外(敵は上限Lvを無視して取得する)
		if(!overEnable){
			if(level >= maxLevel){
				return (int)max;
			}
		}

		float x=0;
		switch(pattern){
		case LevelPattern.Early:
			x = Mathf.Abs(Mathf.Log(level,maxLevel)-Mathf.Log(1,maxLevel));
			break;
		case LevelPattern.Late:
			x = Mathf.Abs(Mathf.Log(maxLevel-level+1,maxLevel)-Mathf.Log(maxLevel,maxLevel));
			break;
		default:
			if(maxLevel!=1){
				x = (level-1)/(maxLevel-1);
			}
			break;
		}
		return (int)(min+(max-min)*x);
	}


	public static CharaData GenerateCharacterData(int rarity) {
		CharaData genCharData = new CharaData ();

		//RarityData
		genCharData.Rarity = rarity; //これはSerialize対象。
		genCharData.RarityData = RarityDataTableObject.Instance.Table.All.FirstOrDefault (rarityData => rarityData.Rarity == ""+rarity); //こっちはSerializeされない

		//攻撃力
		float minAtk = 1;
		float maxAtk = 10;
		switch (rarity) {
		case 1:
		case 2:
		case 3:
		case 4:
			minAtk = 1.0f;
			maxAtk = (float)UnityEngine.Random.Range(10,65);
			break;

		case 5:
		case 6:
		case 7:
			minAtk = (float)UnityEngine.Random.Range(5,20);
			maxAtk = (float)UnityEngine.Random.Range(80,100);
			break;

		case 8:
			minAtk = (float)UnityEngine.Random.Range(70,80);
			maxAtk = (float)UnityEngine.Random.Range(100,150);
			break;

		case 9:
			minAtk = (float)UnityEngine.Random.Range(150,200);
			maxAtk = (float)UnityEngine.Random.Range(230,400);
			break;

		case 10:
			minAtk = (float)UnityEngine.Random.Range(300,400);
			maxAtk = (float)UnityEngine.Random.Range(500,700);
			break;
		}

		genCharData.MinAtk = minAtk;
		genCharData.MaxAtk = maxAtk;

		//gender
		genCharData.gender = (CharaData.Gender)Enum.ToObject(typeof(CharaData.Gender), UnityEngine.Random.Range(0,1));

		//favorite weapon
		genCharData.favoriteWpn = (CharaData.WeaponType)Enum.ToObject(typeof(CharaData.WeaponType), UnityEngine.Random.Range(0,1));

		genCharData.Name = "";

		return genCharData;
	}

}
