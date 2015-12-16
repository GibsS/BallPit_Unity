using UnityEngine;
using System.Collections;

public class PlayerStrategy : MonoBehaviour {

	PlayerBall playerBall;

	void Start () {
		playerBall = GetComponent<PlayerBall> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Movement
		if(Input.GetKey(KeyCode.Q) && !Input.GetKey (KeyCode.D)) {
			playerBall.left ();
		} else if (Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.Q)) {
			playerBall.right ();
		} else {
			playerBall.idleHorizontal();
		}
		if(Input.GetKey(KeyCode.Z) && !Input.GetKey (KeyCode.S)) {
			playerBall.up ();
		} else if (Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.Z)) {
			playerBall.down ();
		} else {
			playerBall.idleVertical();
		}
		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.E)) {
			playerBall.turnLeft();
		} else if (Input.GetKey (KeyCode.E) && !Input.GetKey (KeyCode.A)) {
			playerBall.turnRight ();
		} else {
			playerBall.turnIdle();
		}

		if (Input.GetMouseButton (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
			Collider2D coll = Physics2D.OverlapPoint (mousePosition);
			if(coll != null) {
				Ball ball = coll.GetComponent<Ball>();
				if(ball != null && playerBall.ball.ballGroup.balls.Contains(ball) && ball != playerBall.ball) {
					playerBall.armBall(ball);
				}
			}
		}
		if (Input.GetMouseButtonUp (0) && playerBall.arm) {
			playerBall.releaseBall(Camera.main.ScreenToWorldPoint(Input.mousePosition)-playerBall.armedBall.transform.position);
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			playerBall.trySticky();
		} else {
			playerBall.tryUnsticky();
		}
	}
}
