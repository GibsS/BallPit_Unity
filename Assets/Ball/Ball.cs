using UnityEngine;
using System.Collections.Generic;

public abstract class Ball : MonoBehaviour {

	public List<Ball> adjacentBalls;
	public BallGroup ballGroup;

	CircleCollider2D ballCollider;

	public int id { get; private set; }
	static int availableId;

	protected Vector2 speed;

	void Start () {
		id = availableId++;
		adjacentBalls = new List<Ball> ();
		ballCollider = GetComponent<CircleCollider2D> ();
		ballGroup = Factory.createBallGroup ();
		ballGroup.addBall (this);
	}

	void Update() {
		speed = this.ballGroup.GetComponent<Rigidbody2D> ().velocity;
	}

	void OnCollisionStay2D(Collision2D coll) {
		Ball ball = coll.collider.GetComponent<Ball> ();
		if(/*id > ball.id && */ball != null && collision (ball)) {
			ballGroup.BallCollisionStay2D (coll, this);
			ball.ballGroup.BallCollisionStay2D(coll, this);
		}
		/*if (ball != null && collision (ball)) {
			ballGroup.BallCollisionStay2D (coll, this);
		}*/
	}

	public float getRadius() {
		return ballCollider.radius;
	}

	public virtual void trigger(Vector2 direction, float quantity) {
		this.ballGroup.removeBall (this);
		this.ballGroup.GetComponent<Rigidbody2D> ().velocity = /*velocity + */direction * quantity;
	}

	protected abstract bool collision (Ball ball);
	public abstract BallType getType();
}

public enum BallType {
	normal = 0, destructeur = 1, gold = 2, silver = 3, bronze = 4, mover = 5, rotater = 6, strength
}