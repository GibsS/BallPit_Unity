using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public static BallGroup createBallGroup() {
		GameObject g = new GameObject ("Ball group");
		Rigidbody2D r = g.AddComponent<Rigidbody2D> ();
		r.angularDrag = 0.5f;
		r.drag = 0.5f;
		return g.AddComponent<BallGroup> ();
	}
}

