using UnityEngine;
using System.Collections;

public class MenuBallGenerator : MonoBehaviour {

	public int count;
	public float cooldown;
	public float min;
	public float max;

	float lastBall;
	int c;
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastBall > cooldown && c < count) {
			c++;
			int type = (int)Mathf.Floor (Random.value * 4);
			GameObject g = null;
			switch (type) {
			case 0:
				g = (GameObject)Instantiate (Resources.Load ("Prefabs/Background/background ball1"));
				break;
			case 1:
				g = (GameObject)Instantiate (Resources.Load ("Prefabs/Background/background ball2"));
				break;
			case 2:
				g = (GameObject)Instantiate (Resources.Load ("Prefabs/Background/background ball3"));
				break;
			case 3:
				g = (GameObject)Instantiate (Resources.Load ("Prefabs/Background/background ball3"));
				break;
			}

			g.transform.position = new Vector2(this.transform.position.x + (max - min)*Random.value + min,transform.position.y);
			lastBall = Time.time;
		}
	}
}
