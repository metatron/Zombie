using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEffect : MonoBehaviour {
	void Start () {
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", 1f, 
				"to", 0f, 
				"time", 0.15f, 
				"onupdate", "SetValue",
				"oncomplete", "EffectCompleted"
			));
	}

	private void SetValue(float val) {
		GetComponent<SpriteRenderer> ().color = new Color(1.0f,1.0f,1.0f, val);
	}

	private void EffectCompleted() {
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other) {
		EnemyObject enemyObj = other.gameObject.GetComponent<EnemyObject> ();
		if (enemyObj != null) {
			Destroy (gameObject);
			Destroy (other.gameObject);
		}
	}
}
