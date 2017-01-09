using UnityEngine;
using TouchScript;

public class TapController : MonoBehaviour
{
	[SerializeField]
	private GameObject rawBulletObject;

	void Start() {
		rawBulletObject = (GameObject)Resources.Load ("Prefabs/Bullet");
	}

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
		GameObject instBullet = (GameObject)Instantiate (rawBulletObject);
		instBullet.transform.localPosition = GameManager.Instance.PlayerObject.transform.localPosition;
		instBullet.GetComponent<Rigidbody> ().AddForce (new Vector3 (2000.0f, 0.0f, 0.0f));
    }

    private void touchesBeganHandler(object sender, TouchEventArgs e)
    {
        foreach (var point in e.Touches)
        {
			TouchAction(point.Position);
        }
    }
}