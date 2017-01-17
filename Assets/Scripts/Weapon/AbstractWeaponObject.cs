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
	protected GameObject _owner;
	public GameObject Owner { get { return _owner; } set { _owner = value; } }

	/**
	 * 
	 * GunObject、SwordObjectのアニメーション
	 * 
	 */
	virtual public void AttackAction() {
	}
}
