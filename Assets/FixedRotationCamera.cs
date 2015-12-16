using UnityEngine;
using System.Collections;

public class FixedRotationCamera : MonoBehaviour {

	Vector2 position;

	public GameObject parallax1;
	public GameObject parallax2;
	public GameObject parallax3;

	void Start() {
		foreach (Transform t in transform) {
			if(t.gameObject.name == "parallax1")
				parallax1 = t.gameObject;
			else if(t.gameObject.name == "parallax2")
				parallax2 = t.gameObject;
			else
				parallax3 = t.gameObject;
		}
	}

	void LateUpdate() {
		transform.rotation = Quaternion.identity;
		parallax1.transform.localPosition += ((position - (Vector2)transform.position).x * 0.1f)*Vector3.right;
		parallax2.transform.localPosition += ((position - (Vector2)transform.position).x * 0.2f)*Vector3.right;
		parallax3.transform.localPosition += ((position - (Vector2)transform.position).x * 0.3f)*Vector3.right;
		position = transform.position;
	}
}
