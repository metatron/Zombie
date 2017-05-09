using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BattlePositionController : MonoBehaviour {
	//バトルポジション。0はプレイヤーの隣の箇所。
	public List<GameObject> BattlePosBtnList = new List<GameObject>();

	private CharaData _charaData;

	private int availablePos;

	public void InitButtons(CharaData charaData) {
		_charaData = charaData;
		//0はプレイヤーのとなり。1からWallの場所となるため+1
		availablePos = PlayerData.GetItemNum ("Wall") + 1;

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
					SetDisableButtonColor (BattlePosBtnList [i].GetComponent<Button> (), new Color(0.0f, 0.0f, 1.0f, 0.3f));
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
		//既にセットされているかどうか
		int charCrntPos = _charaData.BattlePosition;
		//セットされている場合はセットされていた場所を一旦disable
		if (charCrntPos != -1) {
			//プレイヤーの隣や、Wallの設置してある箇所は再び利用可能状態にする。
			if (charCrntPos < availablePos) {
				BattlePosBtnList [charCrntPos].GetComponent<Button> ().interactable = true;
			}
			//それ以外はOFF
			else {
				BattlePosBtnList [charCrntPos].GetComponent<Button> ().interactable = false;
				SetDisableButtonColor (BattlePosBtnList [charCrntPos].GetComponent<Button> (), new Color (0.8f, 0.8f, 0.8f, 0.5f));
			}
			PlayerData.UnSetBattlePosNpcId (charCrntPos);
		}

		//押されたボタンのポジにIDをセットし、色をかえる。
		int nextPos = Int32.Parse (name);

		PlayerData.SetBattlePosNpcId (nextPos, _charaData);
		//ボタンの色変え
		BattlePosBtnList [nextPos].GetComponent<Button> ().interactable = false;
		SetDisableButtonColor (BattlePosBtnList [nextPos].GetComponent<Button> (), new Color(0.0f, 0.0f, 1.0f, 0.3f));
	}


	/**
	 * 
	 * ALLリセットボタンがおされた際の処理。
	 * 
	 * 
	 */
	public void OnResetPositionButtonPressed() {
		for (int i = 0; i < BattlePosBtnList.Count; i++) {
			//ポジションをリセット
			PlayerData.UnSetBattlePosNpcId (i);
			//Playerの横のポジション及び作成したWallの数以下の場合
			if (i < availablePos) {
				BattlePosBtnList [i].GetComponent<Button> ().interactable = true;
				continue;
			}
			//そのほか
			BattlePosBtnList [i].GetComponent<Button> ().interactable = false;
			SetDisableButtonColor (BattlePosBtnList [i].GetComponent<Button> (), new Color (0.8f, 0.8f, 0.8f, 0.5f));
		}
	}

	private void SetDisableButtonColor(Button button, Color color) {
		ColorBlock colors = button.colors;
		colors.disabledColor = color;
		button.colors = colors;
	}


}
