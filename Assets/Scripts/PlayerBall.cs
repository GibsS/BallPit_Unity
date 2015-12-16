using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour, IBallGroupHandler {

	public Ball ball { get; private set;}

	// Score 
	public int normalCount;
	public int goldCount;
	public int silverCount;
	public int bronzeCount;
	public int penaltyCount;
	public int getScore() {
		return normalCount + bronzeCount * 2 + silverCount * 3 + goldCount * 5 - penaltyCount*5;
	}
	int moveCount;
	int rotateCount;
	int strengthCount;

	// parametered
	float chargeTime = 2;
	float getStrength(float time) {
		return 3 + strengthCount*3 + (10 + 5*strengthCount)*time;
	}
	//

	public Ball armedBall {get; private set;}
	float armTime;
	Vector2 direction;

	public bool arm;
	bool release;

	// parametered
	float getMovementForce() {
		return 40 + moveCount * 10;
	}
	float getMovementTorque() {
		return 40 + rotateCount * 10;
	}
	//

	bool goUp;
	bool goDown;
	bool goLeft;
	bool goRight;
	bool rotateLeft;
	bool rotateRight;

	bool sticky;

	bool init = false;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!init) {
			ball.ballGroup.setHandler (this);
			ball.ballGroup.unStickable = true;
			ball.ballGroup.GetComponent<Rigidbody2D> ().mass *= 10;
			init = true;
		}

		// Movement
		Rigidbody2D rigid = ball.ballGroup.GetComponent<Rigidbody2D> ();
		if (goUp) {
			rigid.AddForce(Vector2.up*getMovementForce());
		}
		if (goDown) {
			rigid.AddForce(-Vector2.up*getMovementForce());
		}
		if (goLeft) {
			rigid.AddForce(-Vector2.right*getMovementForce());
		}
		if (goRight) {
			rigid.AddForce(Vector2.right*getMovementForce());
		}
		if (rotateLeft) {
			rigid.AddTorque(getMovementTorque());
		}
		if (rotateRight) {
			rigid.AddTorque(-getMovementTorque());
		}
		// Shooting balls
		if (!ball.ballGroup.balls.Contains (armedBall)) {
			release = arm = false;
		}
		if (ball.ballGroup.balls.Contains (armedBall) && (release || (Time.time - armTime > chargeTime && arm))) {
			release = false;
			armedBall.trigger(direction.normalized, getStrength(Time.time - armTime));
		}
	}

	public void ballCollisionEnter2D (Collision2D coll, Ball colliding) {

	}
	public void ballCollisionStay2D (Collision2D coll, Ball colliding) {
		Ball collided = coll.collider.GetComponent<Ball> ();
		if (collided != null && sticky && !this.ball.ballGroup.balls.Contains(collided)) {
			this.ball.ballGroup.addBall(colliding, collided);
		}
	}
	public void ballCollisionExit2D (Collision2D coll, Ball colliding) {
		
	}

	public void clearBall() {
		normalCount = 0;
		bronzeCount = 0;
		silverCount = 0;
		goldCount = 0;
		moveCount = 0;
		rotateCount = 0;
		strengthCount = 0;
	}

	public void gainBall(Ball ball) {
		switch (ball.getType ()) {
		case BallType.normal : normalCount ++; break;
		case BallType.bronze : bronzeCount ++; break;
		case BallType.silver : silverCount ++; break;
		case BallType.gold : goldCount ++; break;
		case BallType.mover : moveCount ++; break;
		case BallType.rotater : rotateCount ++; break;
		case BallType.strength : strengthCount ++; break;
		}
	}
	public void loseBall(Ball ball) {
		switch (ball.getType ()) {
		case BallType.normal : normalCount --; break;
		case BallType.bronze : bronzeCount --; break;
		case BallType.silver : silverCount --; break;
		case BallType.gold : goldCount --; break;
		case BallType.mover : moveCount --; break;
		case BallType.rotater : rotateCount --; break;
		case BallType.strength : strengthCount --; break;
		}
	}

	// MOVEMENT ACTION
	public void down() { goDown = true; goUp = false;}
	public void up() { goDown = false; goUp = true;}
	public void left() { goLeft = true; goRight = false;}
	public void right() { goRight = true; goLeft = false;}
	public void idleHorizontal() { goRight = false; goLeft = false; }
	public void idleVertical() { goUp = false; goDown = false; }
	public void turnLeft() { rotateLeft = true; rotateRight = false;}
	public void turnRight() { rotateLeft = false; rotateRight = true;}
	public void turnIdle() { rotateLeft = rotateRight = false; }

	// BALL TRIGGER
	public void armBall(Ball ball) { 
		if (!arm && ball != this.ball && this.ball.ballGroup.balls.Contains(ball)) {
			armedBall = ball; 
			armTime = Time.time; 
			arm = true;
		}
	}
	public void releaseBall(Vector2 direction) { 
		if (arm) {
			arm = false;
			release = true;
			this.direction = direction.normalized;
		}
	}

	// STICK
	public void trySticky() {
		sticky = true;
	}
	public void tryUnsticky() {
		sticky = false;
	}
}

