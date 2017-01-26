using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : AbstractWeaponObject {
	[SerializeField]
	private float _reachLength = 10.0f;
	public float ReachLength { get { return _reachLength; } set {_reachLength = value; } }

	public override void CopyParamsTo(AbstractWeaponObject target) {
		base.CopyParamsTo (target);
		((SwordObject)target).GetComponent<SpriteRenderer> ().sprite = GetComponent<SpriteRenderer> ().sprite;
		((SwordObject)target).ReachLength = _reachLength;
	}

	public override bool CanReachEnemy () {
		float distance = 0.0f;
		EnemyObject nearestObject = GameManager.Instance.GetClosestEnemyObjectTo (Owner.gameObject, ref distance);
		if (nearestObject == null) {
			return false;
		}

		Debug.LogError (nearestObject + ", " + distance);
		if (_reachLength <= distance) {
			return true;
		}

		return false;
	}

	public void Slash() {
		GameManager.Instance.PlayerObject.Play ("swing1");
	}
}
