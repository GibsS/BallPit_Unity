using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	Texture2D arenaHover;
	Texture2D arenaActive;
	Texture2D arena;

	Texture2D tutorialHover;
	Texture2D tutorialActive;
	Texture2D tutorial;

	Texture2D ballPit;

	GUIContent contentTutorial;
	GUIContent contentArena;

	void Start() {
		arenaHover = (Texture2D)Resources.Load ("Textures/ArenaHover");
		arenaActive = (Texture2D)Resources.Load ("Textures/ArenaActive");
		arena = (Texture2D)Resources.Load ("Textures/Arena");

		tutorialHover = (Texture2D)Resources.Load ("Textures/TutorialHover");
		tutorialActive = (Texture2D)Resources.Load ("Textures/TutorialActive");
		tutorial = (Texture2D)Resources.Load ("Textures/Tutorial");

		ballPit = (Texture2D)Resources.Load ("Textures/Ball pit");

		contentTutorial = new GUIContent ();
		contentTutorial.image = (Texture2D)Resources.Load ("Textures/Tutorial");
		contentArena = new GUIContent ();
		contentArena.image = (Texture2D)Resources.Load ("Textures/Arena");
	}

	void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void OnGUI() {

		GUI.DrawTexture (new Rect (200, 10, 600, 300), ballPit);

		GUI.skin.button.normal.background = tutorial;
		GUI.skin.button.hover.background = tutorialHover;
		GUI.skin.button.active.background = tutorialActive;
		//GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 20, 200, 40),"BALL PIT");
		if (GUI.Button (new Rect (Screen.width - 548, Screen.height - 275, 548, 201), contentTutorial)) {
			Application.LoadLevel("Tutorial");
		}

		GUI.skin.button.normal.background = arena;
		GUI.skin.button.hover.background = arenaHover;
		GUI.skin.button.active.background = arenaActive;
		if (GUI.Button (new Rect (0, Screen.height - 210, 548, 201), contentArena)) {
			Application.LoadLevel("Arena");
		}
	}
}
