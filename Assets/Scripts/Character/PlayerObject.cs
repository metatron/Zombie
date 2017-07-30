using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[System.Serializable]
public class PlayerObject : AbstractCharacterObject {
	
	void OnTriggerEnter(Collider other) {
		//Injured!
		GameManager.Instance.PauseGame = true;

		CharaData injuredChar = null;
		//choose NPC to injure.
		List<CharaData> npcList = PlayerData.GetBattleNpcList();
		if (npcList.Count > 0) {
			injuredChar = Utils.RandomElementAt<CharaData> (npcList);
		} 
		//if there's no npc, injure player.
		else {
			injuredChar = GameManager.Instance.PlayerObject.charaData;
		}
		injuredChar.IsDead = true;

		Debug.LogError ("*******1: " + other);
		UiController.Instance.OpenDialogPanel (injuredChar.Name + " has Injuered!", () => {
			GameManager.Instance.PauseGame = false;
			//全員おっちんだらホームへ。
			if(IsAllCharaDead()) {
				//TODO 遷移後も他の敵に当たってしまうため敵は全消し。
				Destroy(other.gameObject);

				TransitionManager.Instance.FadeTo ("HomeScene");
			}
			//そうでないならキャラObjをDestroy
			else {
				Destroy(injuredChar.charaObject);
			}
		});
	}

	public bool IsAllCharaDead() {
		bool isAllDead = true;
		//プレイヤーチェック
		isAllDead &= PlayerData.playerCharData.IsDead;

		//バトルに参加しているNPCチェック
		foreach (CharaData npcData in PlayerData.playerNpcDictionary.Values) {
			if (npcData.BattlePosition < 0) {
				continue;
			}

			isAllDead &= npcData.IsDead;
		}

		return isAllDead;
	}
}
