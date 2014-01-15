using UnityEngine;
using System.Collections;

public class SideHUDLeft : MonoBehaviour {

	public GUIStyle sty;
	public GUIStyle selected;
	public GUIStyle progressBarFront;
	public GUIStyle progressBarBack;
	int currentCannon;
	static float[] currentProgress = new float[10];
	static bool[] isProgressing = new bool[10];
	static float[] timerDurations = new float[10];
	private string[] sectorNames = new string[10];

	void Start() {
		currentCannon = Spawner.getControlledCannonByID();
		sectorNames[0] = "Alpha";
		sectorNames[1] = "Bravo";
		sectorNames[2] = "Charlie";
		sectorNames[3] = "Delta";
		sectorNames[4] = "Echo";
		sectorNames[5] = "Foxtrot";
		sectorNames[6] = "Golf";
		sectorNames[7] = "Hotel";
		sectorNames[8] = "India";
		sectorNames[9] = "Juliet";
		for(int i=0; i<10; i++) {
			currentProgress[i] = 1f;
			isProgressing[i] = false;
		}
		/*moveProgressBar (0,10f);
		moveProgressBar (1, 15f);
		moveProgressBar (2, 20f);*/
	}

	void Update() {
		currentCannon = Spawner.getControlledCannonByID();
		for (int i = 0; i < 10; i++) {
			if(isProgressing[i])
				updateProgressBar(i);
	    }
	}

	public static void setTimerDuration(int slot, float duration){
		timerDurations[slot]=duration;
	}

	public static void moveProgressBar(int slot, float duration) {
		currentProgress[slot] = 0f;
		timerDurations[slot] = duration;
		isProgressing[slot] = true;
	}

	static void updateProgressBar(int slot) {
		currentProgress[slot] += Time.deltaTime*1/(timerDurations[slot]);
		if(currentProgress[slot] >= 1f) {
			isProgressing[slot] = false;
			currentProgress[slot] = 1f;
			timerDurations[slot] = 0f;
		}
	}

	void OnGUI() {
		int boxWidth = Screen.width/16;
		int boxHeight = Screen.height/32;

		GUI.BeginGroup(new Rect(0,0,2*boxWidth, Screen.height));
		GUI.Box (new Rect(0,0*boxHeight,2*boxWidth, boxHeight), "Sectors", sty);
		for(int i=0; i<10; i++) {
			if(GUI.Button (new Rect(0,(2*i+1)*boxHeight,boxWidth, boxHeight), sectorNames[i],currentCannon==i?selected:sty )) {
				GetComponent<CameraBehaviour>().moveToSelectedCannon(i);
				Spawner.setControlledCannonByID(i);
			}
			GUI.Box (new Rect(boxWidth,(2*i+1)*boxHeight,boxWidth, boxHeight), "0", sty);
			GUI.Box (new Rect(0, (2*i+2)*boxHeight, (int)(currentProgress[i]*((float)boxWidth)*2f), boxHeight), "", progressBarBack);
			GUI.Box (new Rect(0, (2*i+2)*boxHeight, boxWidth*2, boxHeight), currentProgress[i]==1f?"Ready!":"Loading", progressBarFront);
		}
		GUI.Box (new Rect(0,21*boxHeight,boxWidth*2, boxHeight), "Enemies", sty);
		GUI.Box (new Rect(0,22*boxHeight,boxWidth, boxHeight), "Portals", sty);
		GUI.Box (new Rect(0+boxWidth,22*boxHeight,boxWidth, boxHeight), Player.portalCount.ToString(), sty);
		GUI.Box (new Rect(0,23*boxHeight,boxWidth, boxHeight), "Motherships", sty);
		GUI.Box (new Rect(0+boxWidth,23*boxHeight,boxWidth, boxHeight), Player.mothershipCount.ToString(), sty);
		GUI.Box (new Rect(0,24*boxHeight,boxWidth, boxHeight), "Kamikazes", sty);
		GUI.Box (new Rect(0+boxWidth,24*boxHeight,boxWidth, boxHeight), Player.kamikazeCount.ToString(), sty);
		GUI.Box (new Rect(0,28*boxHeight,boxWidth, boxHeight), "Research", sty);
		GUI.Box (new Rect(0,29*boxHeight,boxWidth, boxHeight), "R1", sty);
		GUI.Box (new Rect(0,30*boxHeight,boxWidth, boxHeight), "R2", sty);
		GUI.Box (new Rect(0,31*boxHeight,boxWidth, boxHeight), "R3", sty);
		GUI.EndGroup();
	}
}
