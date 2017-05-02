using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePositionController : MonoBehaviour {
	//バトルポジション。0はプレイヤーの隣の箇所。
	public List<GameObject> BattlePosBtnList = new List<GameObject>();

	private CharaData _charaData;

	public void InitButtons(CharaData charaData) {
		_charaData = charaData;
		//0はプレイヤーのとなり。1からWallの場所となるため+1
		int availablePos = PlayerData.GetItemNum ("Wall") + 1;

		for (int i=0; i<BattlePosBtnList.Count; i++) {
			//playerだった場合は全部disable
			if (charaData.ID == PlayerData.PLAYERID) {
				BattlePosBtnList [i].GetComponent<Button> ().interactable = false;
				continue;
			}


			//プレイヤーの隣 or 所持しているWallの数だけ有効化
			if (i == 0 || i < availablePos) {
				//既に他のNPCがとってる箇所はdisable
				bool activate = true;
				string npcID = PlayerData.GetBattlePosNpcId (i);
				if (npcID != "") {
					activate = false;
				}
				BattlePosBtnList [i].GetComponent<Button> ().interactable = activate;

				//自分が設置されていたら色変化
				if (npcID == charaData.ID) {
					Color disabledColor = BattlePosBtnList [i].GetComponent<Button> ().colors.disabledColor;
					disabledColor = Color.blue;
				}
			} 

			//まだWallを設置していない箇所はdisable
			else {
				BattlePosBtnList [i].GetComponent<Button> ().interactable = false;
			}
		}
	}

	/**
	 * 
	 * バトル画面でのキャラクターの配置。
	 * 
	 */
	public void OnPositionButtonPressed(string name) {
		Debug.LogError ("*****OnPositionButtonPressed: " + name);
		PlayerData.SetBattlePosNpcId (Int32.Parse (name), _charaData);
	}


}
