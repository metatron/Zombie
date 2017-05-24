using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingSystem : MonoBehaviour {
	public enum BaseParts : int {
		HEAD,			//0
		BODY,
		ARM_L,
		ARM_R,
		HIP,
		LEG_UPPER_L,     //5
		LEG_UPPER_R,
		LEG_LOWER_L,
		LEG_LOWER_R      //8
	}

	//服を置くベースとなるボーンのリスト
	public List<GameObject> BodyBaseList = new List<GameObject>();

}
