using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterObject : MonoBehaviour {
	protected Animator _animator;

	public enum CharDirection : int {
		LEFT,
		RIGHT
	}

	[SerializeField]
	protected GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	[SerializeField]
	protected SwordObject _swordObject; //referredオブジェクト
	public SwordObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

	public void Play(string anim) {
		_animator.Play (anim, -1, 0.0f);
	}

	public void InitChar(AbstractCharacterObject.CharDirection dir=AbstractCharacterObject.CharDirection.RIGHT) {
		//Animatorオブジェクトを見つける
		_animator = GetComponentInChildren<Animator> ();
		if (_animator == null) {
			Debug.LogError ("PlayerObject.Skeleton does not have Animator attached!");
			return;
		}
		_animator.Play ("stand");

		//weaponオブジェクトを見つける。（ソード持ってる手のweaponオブジェクト）
		_swordObject = GameManager.getChildGameObject(gameObject, "weapon").GetComponent<SwordObject>();

		//ディフォルトは右向き
		FaceTo(dir);
	}

	public void FaceTo(CharDirection dir) {
		if (dir == CharDirection.LEFT) {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		} else {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}

	public void InitCharGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load (gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}

	public void InitCharSwordObject(string gunPrefabPath) {
		//剣の場合はweaponオブジェクトが既にあるのでパラメータのみの受け渡しOwnerだけ設定しておく
		//パラメータコピー
		_swordObject.CopyParamsFrom (gunPrefabPath);

		//Owerだけ上書き
		_swordObject.Owner = gameObject;

		//マーカー移動
		Vector3 markerPos = transform.position;
		markerPos.x += _swordObject.ReachLength;
		GameManager.Instance.swordReachMarker.transform.position = markerPos;
	}


	public void Attack() {
		if (this.SwordObject.CanReachEnemy ()) {
			this.SwordObject.Slash ();
		}
		else {
			this.GunObject.Fire();
		}
	}


}
