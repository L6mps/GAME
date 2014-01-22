using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUIStyle style;
	public GUIStyle glossaryStyle;
	public Texture[] glossaryPic;
	public string[] glossaryText = new string[11];
	public string[] glossaryLabels = new string[11];
	int selected;
	int currentGlossaryPage = -1;
	Vector2 scrollPosition = Vector2.zero;

	void Start() {
		style.fontSize = ((int) Screen.height/45);
		selected = 0;
	}

	void OnGUI() {
		int x1 = Screen.width/2 - Screen.width/8;
		int xLen = Screen.width/4;
		int y1 = Screen.height/2 - Screen.height/4;
		int yStep = Screen.height/8;
		switch(selected) {
		case 0:
			if(GUI.Button (new Rect (x1,y1, xLen, yStep/2), "New game", style)) {
				Application.LoadLevel (1);
			}
			if(GUI.Button (new Rect (x1,y1+yStep, xLen, yStep/2), "Options", style)) {
				selected = 0; //change to 1 when options are implemented
			}
			if(GUI.Button (new Rect (x1,y1+2*yStep, xLen, yStep/2), "Glossary", style)) {
				selected = 2;
			}
			if(GUI.Button (new Rect (x1,y1+3*yStep, xLen, yStep/2), "Credits", style)) {
				selected = 0; //change to 3  when implemented
			}
			if(GUI.Button (new Rect (x1,y1+4*yStep, xLen, yStep/2), "Exit game", style)) {
				Application.Quit ();
			}
			break;
		case 2:
			int xStart = Screen.width / 8;
			int yStart = Screen.height / 8;

			GUI.BeginGroup(new Rect(xStart, yStart, 6*xStart, 6*yStart));
			scrollPosition = GUI.BeginScrollView(new Rect(0, 0, xStart, 6*yStart), scrollPosition, new Rect(0, 0, (int)((0.9)*xStart), 6*yStart), false, true);
			for(int i=0; i<11; i++) {
				if(GUI.Button (new Rect(0, i*(yStart/2), xStart, yStart/2), glossaryLabels[i], glossaryStyle))
					currentGlossaryPage = i;
			}
			GUI.EndScrollView();
			switch(currentGlossaryPage) {
			case 0:
				break;
			default:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),"OHAI");
				GUI.Label (new Rect(xStart, 4*yStart, 6*xStart, 2*yStart),"OHAI");
				break;
			}
			GUI.EndGroup ();
			break;

		}
	}
}
