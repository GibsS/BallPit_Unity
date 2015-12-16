using UnityEngine;
using System.Collections;

public class DestructorBall : Ball {

	bool lethal;

	protected override bool collision (Ball ball) {
		PlayerBall p = ball.GetComponent<PlayerBall> ();
		if (lethal) {
			//Debug.Log ("test " + this.speed + " " + p);
			if (this.speed.magnitude > 0.5
				&& this.ballGroup.balls.Count == 1
				&& p == null) {
				ball.ballGroup.removeBall (ball);
				Destroy (ball.ballGroup.gameObject);
				Destroy (this.ballGroup.gameObject);
				return false;
			} else if (this.speed.magnitude > 0.5
				&& this.ballGroup.balls.Count == 1
				&& p != null) {
				p.penaltyCount ++;
				Destroy (this.ballGroup.gameObject);
				return false;
			} else {
				return false;
			}
		} else {
			return true;
		}
	}
	void Update() {
		speed = this.ballGroup.GetComponent<Rigidbody2D> ().velocity;
		if (speed.magnitude < 1) {
			lethal = false;
		}
	}

	public override void trigger(Vector2 direction, float quantity) {
		this.ballGroup.removeBall (this);
		this.ballGroup.GetComponent<Rigidbody2D> ().velocity = /*velocity + */direction * quantity;
		lethal = true;
	}

	public override BallType getType() {
		return BallType.destructeur;
	}
}

