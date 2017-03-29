using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObject : AbstractCharacterObject {

	private float waitTime = 0.0f;



	void Update() {
		if (GameManager.Instance.PauseGame) {
			return;
		}

		waitTime += Time.deltaTime;
		if (waitTime >= charaData.atkIntervalCrnt) {
			Attack ();

			waitTime = 0.0f;
		}
	}
}
