using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle style;

	void Start() {
		style.fontSize = ((int) Screen.height/45);
	}

	void OnGUI() {
		int x1 = Screen.width/2 - Screen.width/8;
		int xLen = Screen.width/4;
		int y1 = Screen.height/2 - Screen.width/8;
		int yStep = Screen.height/8;

		if(GUI.Button (new Rect (x1,y1, xLen, yStep/2), "New game", style)) {
			Application.LoadLevel (1);
		}
		if(GUI.Button (new Rect (x1,y1+yStep, xLen, yStep/2), "Options", style)) {
				//TODO
		}
		if(GUI.Button (new Rect (x1,y1+2*yStep, xLen, yStep/2), "Credits", style)) {
				//TODO
		}
		if(GUI.Button (new Rect (x1,y1+3*yStep, xLen, yStep/2), "Exit game", style)) {
			Application.Quit ();
		}
	}
}
