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
	protected SwordObject _swordObject; //instantiatedオブジェクト
	public SwordObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

	public void Play(string anim) {
		_animator.Play (anim, -1, 0.0f);
	}
}
