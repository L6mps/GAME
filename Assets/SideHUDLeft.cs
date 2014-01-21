using UnityEngine;
using System.Collections;

public class SideHUDLeft : MonoBehaviour {

	public GUIStyle sectorButtons;
	public GUIStyle selected;
	public GUIStyle progressBarFront;
	public GUIStyle progressBarBack;
	public GUIStyle enemyButtons;
	public GUIStyle researchButtons;
	int currentCannon;
	static float[] currentProgress = new float[10];
	static bool[] isProgressing = new bool[10];
	static float[] timerDurations = new float[10];
	private string[] sectorNames = new string[10];
	public static bool[] researchState = new bool[20];
	public static int selectedResearch=-1;
	private int researchingWeapon=-1;
	static float researchProgress = 0;

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
		researchState [0] = true;
		for(int i = 1;i<20;i++){
			researchState[i]=false;
		}
	}

	void Update() {
		currentCannon = Spawner.getControlledCannonByID();
		for (int i = 0; i < 10; i++) {
			if(isProgressing[i])
				updateProgressBar(i);
	    }
		if(researchingWeapon!=-1){
			researchProgress+=Time.deltaTime/10;
			if(researchProgress>=1) {
				researchState[researchingWeapon]=true;
				researchingWeapon=-1;
				researchProgress=0;
			}
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
		GUI.Box (new Rect(0,0*boxHeight,2*boxWidth, boxHeight), "Sectors", sectorButtons);
		for(int i=0; i<10; i++) {
			if(GUI.Button (new Rect(0,(2*i+1)*boxHeight,boxWidth, boxHeight), sectorNames[i],currentCannon==i?selected:sectorButtons )) {
				GetComponent<CameraBehaviour>().moveToSelectedCannon(i);
				Spawner.setControlledCannonByID(i);
			}
			GUI.Box (new Rect(boxWidth, (2*i+1)*boxHeight, (int)(currentProgress[i]*((float)boxWidth)), boxHeight), "", progressBarBack);
			GUI.Box (new Rect(boxWidth, (2*i+1)*boxHeight, boxWidth, boxHeight), currentProgress[i]==1f?"Ready!":"Loading", progressBarFront);
			GUI.Box (new Rect(0,(2*i+2)*boxHeight,2*boxWidth, boxHeight), Player.slotPopulation[i].ToString(), sectorButtons);
		}
		GUI.Box (new Rect(0,21*boxHeight,boxWidth*2, boxHeight), "Enemies", enemyButtons);
		GUI.Box (new Rect(0,22*boxHeight,boxWidth, boxHeight), "Portals", enemyButtons);
		GUI.Box (new Rect(0+boxWidth,22*boxHeight,boxWidth, boxHeight), Player.portalCount.ToString(), enemyButtons);
		GUI.Box (new Rect(0,23*boxHeight,boxWidth, boxHeight), "Motherships", enemyButtons);
		GUI.Box (new Rect(0+boxWidth,23*boxHeight,boxWidth, boxHeight), Player.mothershipCount.ToString(), enemyButtons);
		GUI.Box (new Rect(0,24*boxHeight,boxWidth, boxHeight), "Kamikazes", enemyButtons);
		GUI.Box (new Rect(0+boxWidth,24*boxHeight,boxWidth, boxHeight), Player.kamikazeCount.ToString(), enemyButtons);
		GUI.Box (new Rect(0,25*boxHeight,boxWidth*2, boxHeight), "Research", researchButtons);
		for(int i=0;i<4;i++){
			if(researchingWeapon!=i){
				if(GUI.Button (new Rect(0,(26+i*1.5f)*boxHeight,boxWidth,boxHeight*1.5f), "R"+i, selectedResearch==i?selected:(researchState[i]?sectorButtons:researchButtons))){
					if(researchState[i]){
						selectedResearch=i;
					}
					else if(researchingWeapon==-1){
						researchingWeapon=i;
					}
				}
			}
			else{
				GUI.Box (new Rect(0,(26+i*1.5f)*boxHeight, (int)(researchProgress*((float)boxWidth)), boxHeight*1.5f), "", progressBarBack);
				GUI.Box (new Rect(0,(26+i*1.5f)*boxHeight,boxWidth,boxHeight*1.5f), "R"+i, progressBarFront);
			}
		}
		GUI.EndGroup();
		if(selectedResearch!=-1){
			researchSelection(selectedResearch);
		}
	}
	void researchSelection(int r){
		int boxWidth = Screen.width/16;
		int boxHeight = Screen.height/32;
		for(int j=0;j<3;j++){
				if(researchingWeapon!=(r+1)*4+j){
					if(!researchState[(r+1)*4+j]){
						if(GUI.Button (new Rect(boxWidth,(26+2*j)*boxHeight,boxWidth,boxHeight*2), "V"+j, researchButtons)){
							if(researchingWeapon==-1){
								researchingWeapon=(r+1)*4+j;
							}
						}
					}
					else{
						GUI.Box (new Rect(boxWidth,(26+2*j)*boxHeight,boxWidth,boxHeight*2), "V"+j, selected);
					}
				}
				else{
						GUI.Box (new Rect(boxWidth,(26+2*j)*boxHeight, (int)(researchProgress*((float)boxWidth)), boxHeight*2), "", progressBarBack);
						GUI.Box (new Rect(boxWidth,(26+2*j)*boxHeight,boxWidth,boxHeight*2), "V"+j, progressBarFront);
				}
		}
	}
}
