using UnityEngine;
using System.Collections;

public class BallInteraction : MonoBehaviour {

	void Start() {

	}
}

public interface IBallToBallCollisionHandler {

	void handleCollsion(Ball b1, Ball b2);
}