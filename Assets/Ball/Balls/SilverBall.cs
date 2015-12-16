using UnityEngine;
using System.Collections;

public class SilverBall : Ball {
	
	protected override bool collision (Ball ball) {
		return true;
	}
	public override BallType getType() {
		return BallType.silver;
	}
}