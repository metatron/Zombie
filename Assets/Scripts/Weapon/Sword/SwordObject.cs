using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : AbstractWeaponObject {
	[SerializeField]
	private float _reachLength = 1.0f;
	public float ReachLength { get { return _reachLength; } set {_reachLength = value; } }

	[SerializeField]
	private GameObject _swingEffectPrefab;
	public GameObject SwingEffectPrefab { get { return _swingEffectPrefab; } set {_swingEffectPrefab = value; } }

	public override void CopyParamsTo(AbstractWeaponObject target) {
		base.CopyParamsTo (target);
		((SwordObject)target).GetComponent<SpriteRenderer> ().sprite = GetComponent<SpriteRenderer> ().sprite;
		((SwordObject)target).ReachLength = _reachLength;

		((SwordObject)target).SwingEffectPrefab = _swingEffectPrefab;
	}

	public override bool CanReachEnemy () {
		float distance = 0.0f;
		EnemyObject nearestObject = GameManager.Instance.GetClosestEnemyObjectTo (Owner.gameObject, ref distance);
		if (nearestObject == null) {
			return false;
		}

		//敵の半径もリーチ計算に入れる
		float extX = nearestObject.GetComponent<BoxCollider>().bounds.extents.x;
		if (distance <= (_reachLength + extX)) {
			return true;
		}

		return false;
	}

	public void Slash() {
		GameManager.Instance.PlayerObject.Play ("swing1");
		GameObject swingEffect = (GameObject)Instantiate (_swingEffectPrefab, transform);
		swingEffect.GetComponent<AbstractDamageObject> ().WeaponObject = this;

		swingEffect.transform.localPosition = Vector3.zero;
		//親をグローバルにしておく
		swingEffect.transform.SetParent(null);
	}
}
