﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : AbstractData {
	public float Damage { get; private set; }
	public int MaxAmmo { get; private set; }
	public float ReloadTime { get; private set; }
	public string GunPrefab { get; private set; }
	public string BulletPrefab { get; private set; }

}
