using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeaponObject : MonoBehaviour {
	[SerializeField]
	private string _name;
	public string Name { get { return _name; } set { _name = value; } }

	[SerializeField]
	private double _damage = 1.0d;
	public double Damage { 
		get { 
			return _owner.GetComponent<AbstractCharacterObject> ().charaData.CrntAtk () * _damage;
		} 

		set { _damage = value; } 
	}

	[SerializeField]
	private GameObject _owner;
	public GameObject Owner { get { return _owner; } set { _owner = value; } }

	protected Sprite weaponSprite;

	public virtual void CopyParamsTo(AbstractWeaponObject target) {
		target.Name		= _name;
		target.Damage	= _damage;
	}

	protected Sprite LoadSprite(string name) {
		weaponSprite = GameManager.Instance.GetSpriteFromPath ("WeaponAtlas", name);
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

	public void DisplaySprite(bool display) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = display;
	}
}
