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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		int xLen = Screen.width/8;
		int yStep = Screen.height/16;
		GUI.BeginGroup (new Rect(7*(Screen.width/8), Screen.height/2, Screen.width/8, Screen.height));
		if(GUI.Button (new Rect (0,0, xLen, yStep), "Weapon 1", selectedWeapon==1?selected:sty)) {
			selectedWeapon = 1;
		}
		if(GUI.Button (new Rect (0,yStep*1, xLen, yStep), "Weapon 2", selectedWeapon==2?missileselected:missile)) {
			selectedWeapon = 2;
		}
		if(GUI.Button (new Rect (0,yStep*2, xLen, yStep), "Weapon 3", selectedWeapon==3?nukeselected:nuke)) {
			selectedWeapon = 3;
		}
		if(GUI.Button (new Rect (0,yStep*3, xLen, yStep), "Weapon 4", selectedWeapon==4?mineselected:mine)) {
			selectedWeapon = 4;
		}
		GUI.EndGroup();
	}
}
