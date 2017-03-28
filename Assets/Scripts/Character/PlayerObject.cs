using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[System.Serializable]
public class PlayerObject : AbstractCharacterObject {
	
	void OnTriggerEnter(Collider other) {
		Debug.LogError ("Player hit with: " + other.gameObject);
		//
		GameManager.Instance.InitStage(PlayerData.crntStageID);
	}
}
