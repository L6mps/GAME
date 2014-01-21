using UnityEngine;
using System.Collections;

public class SlotProperties : MonoBehaviour {


	public GUIStyle sty;
	public GUIStyle missile;
	public GUIStyle nuke;
	public GUIStyle mine;
	public GUIStyle selected;
	public GUIStyle missileselected;
	public GUIStyle nukeselected;
	public GUIStyle mineselected;
	public static int selectedWeapon=0;
	// Use this for initialization
	void Start () {
		sty.fontSize = Screen.height / 50;
		missile.fontSize = Screen.height / 50;
		nuke.fontSize = Screen.height / 50;
		mine.fontSize = Screen.height / 50;
		selected.fontSize = Screen.height / 50;
		missileselected.fontSize = Screen.height / 50;
		nukeselected.fontSize = Screen.height / 50;
		mineselected.fontSize = Screen.height / 50;
	}
	
	void OnGUI() {
		int xLen = Screen.width/8;
		int yStep = Screen.height/16;
		GUI.BeginGroup (new Rect(7*(Screen.width/8), Screen.height/2, Screen.width/8, Screen.height));
		if(GUI.Button (new Rect (0,0, xLen, yStep), "Cannon", selectedWeapon==1?selected:sty)) {
			if(SideHUDLeft.researchState[0])
				selectedWeapon = 1;
		}
		if(GUI.Button (new Rect (0,yStep*1, xLen, yStep), "MissileLauncher", selectedWeapon==2?missileselected:missile)) {
			if(SideHUDLeft.researchState[1])
				selectedWeapon = 2;
		}
		if(GUI.Button (new Rect (0,yStep*2, xLen, yStep), "NukeLauncher", selectedWeapon==3?nukeselected:nuke)) {
			if(SideHUDLeft.researchState[2])
				selectedWeapon = 3;
		}
		if(GUI.Button (new Rect (0,yStep*3, xLen, yStep), "MineLauncher", selectedWeapon==4?mineselected:mine)) {
			if(SideHUDLeft.researchState[3])
				selectedWeapon = 4;
		}
		GUI.EndGroup();
	}
}
