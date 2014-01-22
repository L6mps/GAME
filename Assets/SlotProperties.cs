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
	public GUIStyle slot;
	public Texture cannonI;
	public Texture missileI;
	public Texture mineI;
	public Texture nukeI;
	public GUIStyle progressBarBack;
	public GUIStyle progressBarFront;
	public GUIStyle labelStyle;
	public static int selectedWeapon=0;
	int currentCannon;
	// Use this for initialization
	void Start () {
		currentCannon = Spawner.getControlledCannonByID ();
		sty.fontSize = Screen.height / 50;
		missile.fontSize = Screen.height / 50;
		nuke.fontSize = Screen.height / 50;
		mine.fontSize = Screen.height / 50;
		selected.fontSize = Screen.height / 50;
		missileselected.fontSize = Screen.height / 50;
		nukeselected.fontSize = Screen.height / 50;
		mineselected.fontSize = Screen.height / 50;
		slot.fontSize = Screen.height / 50;
		labelStyle.fontSize=Screen.height/50;
		progressBarFront.fontSize=Screen.height/50;
	}
	void Update(){
				currentCannon = Spawner.getControlledCannonByID ();
		}
	void OnGUI() {
		int xLen = Screen.width/8;
		int yStep = Screen.height/16;
		GUI.BeginGroup (new Rect(7*xLen, xLen, Screen.width/8, Screen.height-xLen));
		if(GUI.Button (new Rect (0,0, xLen, yStep), "Cannon", selectedWeapon==1?selected:sty)) {
			if(SideHUDLeft.researchState[0])
				selectedWeapon = 1;
		}
		if(GUI.Button (new Rect (0,yStep*1, xLen, yStep), "Missile", selectedWeapon==2?missileselected:missile)) {
			if(SideHUDLeft.researchState[1])
				selectedWeapon = 2;
		}
		if(GUI.Button (new Rect (0,yStep*2, xLen, yStep), "Nuke", selectedWeapon==3?nukeselected:nuke)) {
			if(SideHUDLeft.researchState[2])
				selectedWeapon = 3;
		}
		if(GUI.Button (new Rect (0,yStep*3, xLen, yStep), "Mine", selectedWeapon==4?mineselected:mine)) {
			if(SideHUDLeft.researchState[3])
				selectedWeapon = 4;
		}
		GUI.Box (new Rect(0,yStep*4,xLen,Screen.height-xLen-4*yStep)," Sector information", slot);
		int labelSize = Screen.height /40;
		GUI.BeginGroup(new Rect(0,yStep*4+labelSize,xLen,Screen.height-xLen-4*yStep-labelSize));
		bool cannon;
		if(currentCannon<10 &&currentCannon>=0){
			if (Spawner.cannons [currentCannon] != null)
							cannon = true;
					else
							cannon = false;
			string[] info = getCannonInfo ();
			GUI.Label (new Rect (0, 0, xLen, labelSize)," "+SideHUDLeft.sectorNames[currentCannon],labelStyle);
			GUI.Label (new Rect (0, labelSize, xLen, labelSize)," Type: "+(cannon?info[0]:" n/a"),labelStyle);
			GUI.Label (new Rect (0, 2*labelSize, xLen, labelSize)," Military: "+(cannon?""+Player.slotPopulation[currentCannon]:"n/a"),labelStyle);
			GUI.Label (new Rect (0, 3*labelSize, xLen, labelSize)," Speed: "+(cannon?info[1]:"n/a"),labelStyle);
			GUI.Label (new Rect (0, 4*labelSize, xLen, labelSize)," Range: "+(cannon?info[2]:"n/a"),labelStyle);
			GUI.Label (new Rect (0, 5*labelSize, xLen, labelSize)," Angle: "+(cannon?info[3]:"n/a"),labelStyle);
			int picSize=Screen.height-xLen-4*yStep-9*labelSize<xLen?Screen.height-xLen-4*yStep-9*labelSize:xLen;
			if(cannon){
				switch (info[0]){
				case "cannon":{
					GUI.Label(new Rect(0,6*labelSize,picSize,picSize), cannonI);
					break;
				}
				case "missile":{
					GUI.Label(new Rect(0,6*labelSize,picSize,picSize), missileI);
					break;
				}
				case "nuke":{
					GUI.Label(new Rect(0,6*labelSize,picSize,picSize), nukeI);
					break;
				}
				case "mine":{
					GUI.Label(new Rect(0,6*labelSize,picSize,picSize), mineI);
					break;
				}
				}
				GUI.Box (new Rect(xLen/8, 6*labelSize+picSize, (int)(SideHUDLeft.currentProgress[currentCannon]*((float)6*xLen/8)), labelSize), "", progressBarBack);
				GUI.Box (new Rect(xLen/8, 6*labelSize+picSize, 6*xLen/8, labelSize), SideHUDLeft.currentProgress[currentCannon]==1f?"Ready!":"Loading", progressBarFront);
			}
		}
		else{
			GUI.Label (new Rect (0, 0, xLen, labelSize)," Not selected",labelStyle);
			GUI.Label (new Rect (0, labelSize, xLen, labelSize)," Type: n/a",labelStyle);
			GUI.Label (new Rect (0, 2*labelSize, xLen, labelSize)," Military: n/a",labelStyle);
			GUI.Label (new Rect (0, 3*labelSize, xLen, labelSize)," Speed: n/a",labelStyle);
			GUI.Label (new Rect (0, 4*labelSize, xLen, labelSize)," Range: n/a",labelStyle);
			GUI.Label (new Rect (0, 5*labelSize, xLen, labelSize)," Angle: n/a",labelStyle);
		}
		GUI.EndGroup ();
		GUI.EndGroup();
	}
	string[] getCannonInfo(){
		string[] info=new string[4];
		for(int i=0;i<4;i++)
			info[i]="";
		if(Spawner.cannons[currentCannon]!=null){
			if(Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>()!=null){
				info[0]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().cannonType;
				if(info[0].Equals("cannon")){
					info[1]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().projectile.GetComponent<ProjectileFire>().maxSpeed.ToString();
					info[2]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().projectile.GetComponent<ProjectileFire>().range.ToString();
					info[3]=(Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().angleRange*2).ToString();
				}
				else if(info[0].Equals ("missile")){
					info[1]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().projectile.GetComponent<ProjectileFire>().maxSpeed.ToString();
					info[2]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().projectile.GetComponent<ProjectileFire>().range.ToString();
					info[3]=(Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().angleRange*2).ToString();
				}
				else if(info[0].Equals ("mine")){
					info[1]=Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().projectile.GetComponent<MineBehaviour>().maxSpeed.ToString();
					info[2]="infinite";
					info[3]=(Spawner.cannons[currentCannon].GetComponentInChildren<CannonControl>().angleRange*2).ToString();
				}
			}
			else if(Spawner.cannons[currentCannon].GetComponentInChildren<NukeControl>()!=null){
				info[0]=Spawner.cannons[currentCannon].GetComponentInChildren<NukeControl>().cannonType;
				info[1]=Spawner.cannons[currentCannon].GetComponentInChildren<NukeControl>().projectile.GetComponent<NukeBehaviour>().maxSpeed.ToString();
				info[2]="infinite";
				info[3]=(Spawner.cannons[currentCannon].GetComponentInChildren<NukeControl>().angleRange*2).ToString();
			}
		}
		return info;
	}

}
