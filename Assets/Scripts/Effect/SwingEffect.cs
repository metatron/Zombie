using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEffect : AbstractDamageObject {
	void Start () {
		iTween.ValueTo(gameObject, 
			iTween.Hash(
				"from", 1f, 
				"to", 0f, 
				"time", 0.15f, 
				"onupdate", "SetValue",
				"oncomplete", "EffectCompleted"
			));

		Vector3 moveDestPos = GameManager.Instance.swordReachMarker.transform.position;
		moveDestPos.x -= transform.localScale.x / 2.0f;
		Debug.LogError ("@@@@@@@markerPos: " + GameManager.Instance.swordReachMarker.transform.position + ", scale: " + transform.localScale.x + ", distPos: " + moveDestPos);
		iTween.MoveTo (gameObject, 
			iTween.Hash (
				"position", moveDestPos,
				"islocal", false, 
				"time", 0.15f
			));
	}

	private void SetValue(float val) {
		GetComponent<SpriteRenderer> ().color = new Color(1.0f,1.0f,1.0f, val);
	}

	private void EffectCompleted() {
		Destroy (gameObject);
	}

}
