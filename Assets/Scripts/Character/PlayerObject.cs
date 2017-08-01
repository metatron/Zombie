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

		//一旦ポーズ
		GameManager.Instance.PauseGame = true;

		//誰がやられたかポップアップで知らせる。
		UiController.Instance.OpenDialogPanel (injuredChar.Name + " has Injuered!", () => {
			GameManager.Instance.PauseGame = false;
			if(injuredChar.charaObject != null) {
				GameManager.Instance.DeathEffect(injuredChar.charaObject);
			}

			//全員おっちんだらホームへ。
			if(IsAllCharaDead()) {
				//TODO 遷移後も他の敵に当たってしまうため敵は全消し。
				GameManager.Instance.DeleteAllEnemies();
				Destroy(other.gameObject);

				TransitionManager.Instance.FadeTo ("HomeScene");
			}
			//何人か生存
			else {
				//現存する敵を右に少し追いやる（「間」が欲しい）
				foreach(EnemyObject enemyObj in GameManager.Instance.crntEnemyDictionary.Values) {
					if(enemyObj == null) {
						continue;
					}
					Vector3 reposVec = enemyObj.transform.localPosition;
					reposVec.x += 5.0f;
					enemyObj.transform.localPosition = reposVec;
				}
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
