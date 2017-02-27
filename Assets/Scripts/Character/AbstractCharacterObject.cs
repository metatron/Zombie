using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractCharacterObject : MonoBehaviour {
	protected Animator _animator;

	public enum CharDirection : int {
		LEFT,
		RIGHT
	}

	public CharaData charaData = new CharaData();

	[SerializeField]
	protected GunObject _gunObject; //instantiatedオブジェクト
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	[SerializeField]
	protected SwordObject _swordObject; //referredオブジェクト
	public SwordObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

	public void Play(string anim) {
		_animator.Play (anim, -1, 0.0f);
	}

	public void InitChar(CharaData charaData, AbstractCharacterObject.CharDirection dir=AbstractCharacterObject.CharDirection.RIGHT) {
		this.charaData = charaData;
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

		//描画順設定
		string sortingLayerName = "Default";
		if (charaData.BattlePosition > 0) {
			sortingLayerName = "NpcPos" + charaData.BattlePosition;
		}
		SetSortingLayer (sortingLayerName);
	}

	public void FaceTo(CharDirection dir) {
		if (dir == CharDirection.LEFT) {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		} else {
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 180.0f, 0.0f));
		}
	}

	public void SetSortingLayer(string sortingLayerName) {
		foreach (SpriteRenderer spRenderer in GetComponentsInChildren<SpriteRenderer>()) {
			spRenderer.sortingLayerName = sortingLayerName;
		}
	}

	public void InitCharGunObject(string gunPrefabPath) {
		_gunObject = ((GameObject)Instantiate ((GameObject)Resources.Load ("Prefabs/Items/Guns/"+gunPrefabPath))).GetComponent<GunObject>();
		_gunObject.Owner = gameObject;
		_gunObject.transform.SetParent (transform);
		_gunObject.transform.localPosition = Vector3.zero;
	}

	public void InitCharSwordObject(string swordPrefabPath) {
		//剣の場合はweaponオブジェクトが既にあるのでパラメータのみの受け渡しOwnerだけ設定しておく
		//パラメータコピー
		_swordObject.CopyParamsFrom (swordPrefabPath);

		//Ower上書き
		_swordObject.Owner = gameObject;

		//自キャラの場合はマーカー移動
		if (gameObject.GetComponent<PlayerObject> () != null) {
			Vector3 markerPos = transform.position;
			markerPos.x += _swordObject.ReachLength;
			GameManager.Instance.swordReachMarker.transform.position = markerPos;
		}
	}


	public void Attack() {
		if (this.SwordObject != null && this.SwordObject.CanReachEnemy ()) {
			this.SwordObject.Slash ();
		}
		else if (this.GunObject != null) {
			this.GunObject.Fire();
		}
	}


}
