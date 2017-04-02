using System;
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
	public int CalcLevelParameter(int min, int max, int level, int maxLevel, LevelPattern pattern, bool overEnable = false) {
		//上限Lvを無視するパラメータ取得以外(敵は上限Lvを無視して取得する)
		if(!overEnable){
			if(level >= maxLevel){
				return max;
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


}
