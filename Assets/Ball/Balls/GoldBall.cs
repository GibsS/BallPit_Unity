using UnityEngine;
using System.Collections;

public class GoldBall : Ball {
	
	protected override bool collision (Ball ball) {
		return true;
	}
	public override BallType getType() {
		return BallType.gold;
	}
}

