using UnityEngine;
using System.Collections;

public class RotateBall : Ball {
	
	protected override bool collision (Ball ball) {
		return true;
	}
	public override BallType getType() {
		return BallType.rotater;
	}
}