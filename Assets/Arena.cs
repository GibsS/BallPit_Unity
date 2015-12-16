using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {

	GameObject[] player;

	float startDate;

	//int winner = -1;
	bool fini;
	float score;

	void Start() {
		player = new GameObject[4];
		player [0] = GameObject.Find ("Ball player");
		/*player [1] = GameObject.Find ("Ball ai1");
		player [2] = GameObject.Find ("Ball ai2");
		player [3] = GameObject.Find ("Ball ai3");*/

		startDate = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if (getSeconde () == 40) {
			fini = true;
			score = player[0].GetComponent<PlayerBall>().getScore();
		}
		/*if (winner == -1 && getMinute() >= 3) {
			winner = 0;
			int score = player[0].GetComponent<PlayerBall>().getScore();
			for(int i = 1; i < 4; i++) {
				int tmp = player[0].GetComponent<PlayerBall>().getScore();
				if(tmp > score) {
					score = tmp;
					winner = i;
				}
			}
		}*/

		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Menu");
		}
		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel("Arena");
		}
	}

	void OnGUI() {
		//if (winner == -1) {
			int seconde = getSeconde ();
			int minute = getMinute ();
			/*GUI.color = Color.black;
			GUI.Label (new Rect (150, Screen.height - 130, Screen.width, 20), 
		           "Time : " + minute + ":" + (seconde > 10 ? "" + seconde : "0" + seconde) + "/3:00");
			GUI.Label (new Rect (150, Screen.height - 110, Screen.width, 20), "Score : ");
			GUI.Label (new Rect (150, Screen.height - 90, Screen.width, 20), 
		           	"player : " + player [0].GetComponent<PlayerBall> ().getScore ());
			GUI.Label (new Rect (150, Screen.height - 70, Screen.width, 20), 
		            "ai 1 : " + player [1].GetComponent<PlayerBall> ().getScore ());
			GUI.Label (new Rect (150, Screen.height - 50, Screen.width, 20), 
		           	"ai 2 : " + player [2].GetComponent<PlayerBall> ().getScore ());
			GUI.Label (new Rect (150, Screen.height - 30, Screen.width, 20), 
		            "ai 3 : " + player [3].GetComponent<PlayerBall> ().getScore ());
		} else {
			GUI.color = Color.black;
			if(winner == 0) {
				GUI.Label (new Rect (150, Screen.height - 130, Screen.width, 20), "You win !");
			} else {
				GUI.Label (new Rect (150, Screen.height - 130, Screen.width, 20), "You lost !");
			}
			GUI.Label (new Rect (150, Screen.height - 110, Screen.width, 20), "R : Restart");
			GUI.Label (new Rect (150, Screen.height - 90, Screen.width, 20), "Escape : Menu");
		//}*/
		GUI.color = Color.black;
		if (!fini) {
			GUI.Label (new Rect (150, Screen.height - 130, Screen.width, 20), 
		           "Time : " + minute + ":" + (seconde > 10 ? "" + seconde : "0" + seconde) + "/3:00");
		} else  {
			GUI.Label (new Rect (150, Screen.height - 130, Screen.width, 20), "Your score : " + score);
		}
	}
	public int getMinute() {
		return ((int)Mathf.Floor (Time.time - startDate)) / 60;
	}
	public int getSeconde() {
		return ((int)Mathf.Floor (Time.time - startDate)) % 60;
	}
}
