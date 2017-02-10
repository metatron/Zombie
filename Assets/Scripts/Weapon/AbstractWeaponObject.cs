using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeaponObject : MonoBehaviour {
	[SerializeField]
	private string _name;
	public string Name { get { return _name; } set { _name = value; } }

	[SerializeField]
	private double _damage = 1.0d;
	public double Damage { get { return _damage; } set { _damage = value; } }

	[SerializeField]
	private GameObject _owner;
	public GameObject Owner { get { return _owner; } set { _owner = value; } }

	//全ての武器の画像を一旦保存
	private static Sprite[] loadedSprites;
	protected Sprite weaponSprite;

	public virtual void CopyParamsTo(AbstractWeaponObject target) {
		target.Name		= _name;
		target.Damage	= _damage;
	}

	protected Sprite LoadSprite(string name) {
		if (loadedSprites == null || loadedSprites.Length == 0) {
			string weaponImgPath = "Atlases/WeaponAtlas";
			loadedSprites = Resources.LoadAll<Sprite> (weaponImgPath);
		}
		weaponSprite = GetComponent<SpriteRenderer> ().sprite = System.Array.Find<Sprite> (loadedSprites, (sprite) => sprite.name.Equals (name));
		return weaponSprite;
	}

	/**
	 * 
	 * 銃は常にtrue.
	 * 剣はリーチ内であればtrue.
	 * 
	 * @return bool
	 * 
	 */
	public virtual bool CanReachEnemy() {
		return true;
	}

}
