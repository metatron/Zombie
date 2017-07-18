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

		UiController.Instance.OpenDialogPanel (injuredChar.Name + " has Injuered!", () => {
			GameManager.Instance.PauseGame = false;
			GameManager.Instance.InitStage(PlayerData.crntStageID);
		});
	}
}
