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
		KeyValuePair<float, EnemyObject> nearestObject = GameManager.Instance.GetClosestEnemyObjectTo (Owner.gameObject);
		if (nearestObject.Value == null) {
			return false;
		}

		if (_reachLength <= nearestObject.Key) {
			return true;
		}

		return false;
	}

	public void Slash() {
		GameManager.Instance.PlayerObject.Play ("swing1");
	}
}
