using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[System.Serializable]
public class PlayerObject : AbstractCharacterObject {
	
	void OnTriggerEnter(Collider other) {
		Debug.LogError ("Player hit with: " + other.gameObject);
		//choose NPC to injure.
		//if there's no npc, injure player.


		UiController.Instance.OpenDialogPanel ("", () => {
		});

		GameManager.Instance.InitStage(PlayerData.crntStageID);
	}
}
