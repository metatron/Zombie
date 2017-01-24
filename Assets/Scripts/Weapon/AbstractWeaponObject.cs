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

	public static T CopyComponent<T>(T original, GameObject destination) where T : Component {
		System.Type type = original.GetType();
		Component copy = destination.GetComponent(type);
		if (copy == null) {
			copy = destination.AddComponent(type);
		}
		System.Reflection.FieldInfo[] fields = type.GetFields();
		Debug.Log (type + ", " + original + ", " + fields.Length);
		foreach (System.Reflection.FieldInfo field in fields) {
			Debug.LogError ("***: " + field);
			field.SetValue(copy, field.GetValue(original));
		}
		return copy as T;
	}}
