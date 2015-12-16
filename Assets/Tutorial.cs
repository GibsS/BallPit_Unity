using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	GameObject player;

	string[] text = new string[]{"Hey, little pink ball! Move around with ASDW. Go to the right to learn more!", 
								 "See these balls? Grab'em! Keep shift pressed to become sticky. Once the balls are stuck, they stay.",
								 "You can rotate with Q and E.",
						         "Each balls have a certain value, the gold is the best, then comes the silver, bronze and pink.",
								 "Feel a bit to heavy? Click one of your balls and it will go on without you in the direction you point toward.",
								 "The longer you click, the faster it will be ejected from you.",
								 "These red balls are a bit dangerous. If they are launched at high velocity, they will destroy balls they collide, try it out!",
								 "Finally, the blue balls increase your speed and these light blue balls increase your rotation speed",
								 "In arena mode, you will compete with others like you, the goal being to have the most ball points. Have fun! Press Escape to go to the main menu."};
	int currentText;
	float lastModification;

	void Start() {
		player = GameObject.Find ("Ball player");
		setCurrentText (0);
	}
	// Update is called once per frame
	void Update () {
		if (currentText == 0 && Time.time - lastModification > 8) {
			setCurrentText(1);
		}
		if (currentText <= 1 && player.transform.position.x > -30) {
			setCurrentText(2);
		}
		if (currentText == 2 && Time.time - lastModification > 8) {
			setCurrentText(3);
		}
		if (currentText == 3 && Time.time - lastModification > 8) {
			setCurrentText(4);
		}
		if (currentText == 4 && Time.time - lastModification > 8) {
			setCurrentText(5);
		}
		if (currentText <= 5 && Vector2.Distance (player.transform.position, new Vector2 (43, -14)) < 10) {
			setCurrentText(6);
		}
		if (currentText == 6 && Vector2.Distance (player.transform.position, new Vector2 (54, -47)) < 20) {
			setCurrentText(7);
		}
		if(currentText == 7 && Time.time - lastModification > 10) {
			setCurrentText(8);
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("Menu");
		}
	}

	void setCurrentText(int c) {
		currentText = c;
		lastModification = Time.time;
	}
	void OnGUI() {
		GUI.color = Color.black;
		for (int i = 0; i <= currentText; i++) {
			GUI.Label(new Rect(200, Screen.height -30 - 20*(currentText-i), Screen.width, 20),text[i]);
		}
	}
}
