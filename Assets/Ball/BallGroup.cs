using UnityEngine;
using System.Collections.Generic;

public class BallGroup : MonoBehaviour {
	
	public List<Ball> balls;
	Ball mainBall;
	IBallGroupHandler handler;

	public bool unStickable;

	public void BallCollisionStay2D(Collision2D coll, Ball ball) {
		if(handler != null)
			handler.ballCollisionStay2D (coll, ball);
	}

	public void setHandler(IBallGroupHandler handler) {
		this.handler = handler;
	}
	public void setMain(Ball ball) {
		mainBall = ball;
	}

	public void addBall(Ball ball) {
		balls = new List<Ball> ();
		balls.Add (ball);
		ball.ballGroup = this;
		ball.transform.parent = this.transform;
		setMain (ball);
	}
	public void addBall(Ball first, Ball next) {
		if (!first.adjacentBalls.Contains (next) && !balls.Contains(next) && !next.ballGroup.unStickable) {
			GameObject g = next.ballGroup.gameObject;
			foreach (Ball child in next.ballGroup.balls) {
				child.transform.parent = this.transform;
				this.balls.Add (child.GetComponent<Ball>());
				child.GetComponent<Ball>().ballGroup = this; 
			}
			Destroy (g);
			
			first.adjacentBalls.Add (next);
			next.adjacentBalls.Add (first);

			if(handler != null) {
				handler.gainBall(next);
			}
		}
	}
	public void removeBall(Ball ball) {
		if (balls.Contains (ball)) {
			// remove link
			foreach(Ball b in new List<Ball>(ball.adjacentBalls)) {
				b.adjacentBalls.Remove(ball);
				ball.adjacentBalls.Remove(b);
			}
			
			// Obtain connexe component
			List<List<Ball>> components = connexeComponents(balls);
			
			// Create group
			foreach(List<Ball> l in components) {
				if(mainBall == null || !l.Contains(mainBall)) {
					BallGroup sg = Factory.createBallGroup();
					sg.balls = new List<Ball>();
					foreach(Ball s in l) {
						sg.balls.Add (s);
						s.transform.parent = sg.transform;
						s.ballGroup = sg;
						if(sg.handler != null) {
							sg.handler.gainBall(s);
						}
					}
				} else {
					this.balls.Clear();
					if(handler != null) {
						handler.clearBall();
					}
					foreach(Ball s in l) {
						this.balls.Add (s);
						s.transform.parent = this.transform;
						s.ballGroup = this;
						if(this.handler != null) {
							handler.gainBall(s);
						}
					}
				}
			}
			if(mainBall == null) {
				Destroy (this.gameObject);
			}
		}
	}

	public List<List<Ball>> connexeComponents(List<Ball> list) {
		List<List<Ball>> components = new List<List<Ball>> ();
		List<Ball> component;
		List<Ball> openSet = new List<Ball> ();
		List<Ball> neighbour = new List<Ball> ();
		Ball current;
		
		while(list.Count > 0) {
			component = new List<Ball>();
			openSet.Clear();
			
			openSet.Add(list[0]);
			while(openSet.Count > 0) {
				current = openSet[0];
				openSet.Remove(current);
				list.Remove(current);
				component.Add (current);
				
				neighbour.Clear();
				neighbour.AddRange(current.adjacentBalls);
				foreach(Ball s in neighbour) {
					if(list.Contains(s)) {
						openSet.Add (s);
					}
				}
			}
			components.Add (component);
		}
		return components;
	}
}

public interface IBallGroupHandler {

	void ballCollisionEnter2D (Collision2D coll, Ball ball);
	void ballCollisionStay2D (Collision2D coll, Ball ball);
	void ballCollisionExit2D (Collision2D coll, Ball ball);

	void clearBall();
	void gainBall(Ball ball);
	void loseBall(Ball ball);
}