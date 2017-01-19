﻿using UnityEngine;
using TouchScript;

public class TapController : MonoBehaviour
{
    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan += touchesBeganHandler;
        }

    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
        }
    }

    private void TouchAction(Vector2 position)
    {
		//Vector3 touchAt = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
		GameManager.Instance.PlayerObject.GunObject.Fire();
		GameManager.Instance.PlayerObject.Play ("swing1");
    }

    private void touchesBeganHandler(object sender, TouchEventArgs e)
    {
        foreach (var point in e.Touches)
        {
			TouchAction(point.Position);
        }
    }
}