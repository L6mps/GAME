using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	int numberOfGlossaryItems = 13;
	public GUIStyle style;
	public GUIStyle glossaryStyle;
	public GUIStyle background;
	public Texture[] glossaryPic;
	public string[] glossaryText;
	public string[] glossaryLabels;
	int selected;
	int currentGlossaryPage = -1;
	Vector2 scrollPosition = Vector2.zero;
	Vector2 smallScrollPosition = Vector2.zero;

	void Start() {
		glossaryText = new string[numberOfGlossaryItems];
		glossaryLabels = new string[numberOfGlossaryItems];
		style.fontSize = ((int) Screen.height/40);
		selected = 0;
		glossaryLabels[0] = "Sector";
		glossaryLabels[1] = "Enemies";
		glossaryLabels[2] = "Papa(portal)";
		glossaryLabels[3] = "Mike(mothership)";
		glossaryLabels[4] = "Kilo(kamikaze)";
		glossaryLabels[5] = "Weapons";
		glossaryLabels[6] = "Cannon";
		glossaryLabels[7] = "Missile";
		glossaryLabels[8] = "Nuke";
		glossaryLabels[9] = "Mine";
		glossaryLabels[10] = "Population";
		glossaryLabels[11] = "Research";
		glossaryLabels[12] = "Radar";
		glossaryText[0] = "Earth has been divided into 10 military sectors. " +
			"Each weapons system that has been built requires a portion of the population to be dedicated to it." +
			"Upon building a weapons system on a slot, 200 million citizens of Earth are recruited into that system's force." +
			"A weapons system can be built into a sector's appropriate slot by selecting a weapon type from the right-hand menu," +
			" then CTRL-clicking the slot." +
			"When the military population of a sector is destroyed, that sector's weapon is also destroyed, but can be rebuilt.";
		glossaryText[1] = "There are three types of enemies. \n1) Portals, with the codename Papa. Their task is to transport motherships near Earth." +
			"\n2)Motherships, with the codename Mike. Their task is to spawn Kamikaze attackers." +
			"\n3)Kamikazes, with the codename Kilo. Their task is to destroy Earth by whatever means neccessary!";
		glossaryText[2] = "Portals, codenamed Papa, are destroyable transport gates for bringing motherships near Earth. In order to destroy a portal, " +
			"a skilled nuke or mine shot is usually required. As the attack progresses, more and more portals are sent.";
		glossaryText[3] = "Motherships, codenamed Mike, are destroyable Kamikaze carriers that orbit Earth. They can spawn a seemingly unlimited amount" +
			" of Kamikazes, and usually require a skilled nuke or mine shot to be destroyed.";
		glossaryText[4] = "Kamikazes, codenamed Kilo, are the aliens' main weapon of destroying Earth. They launch themselves in fast groups of 3 or slow groups of 10 towards Earth, " +
			"where upon impact they kill 50 to 100 million of the surrounding population. A portion of that is the military defense controlling the current sector's weapons system, if present.";
		glossaryText[5] = "There are four types of upgradable weapons available:" +
			"\n1)Cannon - basic non-explosive projectile. Fast reload, small flying range." +
			"\n2)Missile - basic explosive projectile. Slower reload than cannon, area-of-effect damage capabilities, small flying range. " +
			"\n3)Nuke - large-scale explosive projectile, designed to destroy tactical objects. Slow reload, high damage, long-range capabilities." +
			"\n4)Mine - explosive projectile for strategic destruction of tactical objects. Slow reload, precise delivery, long-range capabilities.";
		glossaryText[6] = "The cannon is the most basic of available weapons. It is already researched when starting the game. Properties: small flying range, fast reload, " +
			"NO area-of-effect damage.";
		glossaryText[7] = "The missile launcher is a basic area-of-effect damage projectile that must be researched in order to be unlocked. Its explosion can usually destroy a group of 10 kamikazes easily. " +
			"Its flying range usually is not enough to reach motherships and portals, though. Its reload time is slower than the cannon's.";
		glossaryText[8] = "The nuke launcher is an advanced area-of-effect damage projectile that must be researched in order to be unlocked. " +
			"It will attempt to travel to the targeted spot and explode there. It will destroy any kamikazes in its path. Properties: slow reload, high damage, can reach any enemy.";
		glossaryText[9] = "The mine launcher is an advanced tactical weapon, designed to destroy motherships and portals in their paths. It will move to the targeted spot, avoiding any " +
			"enemies on the way, and arm itself upon reaching the destination. The mine will wait in the destination until it collides with an enemy and will then explode in an area-of-effect manner. The launcher has a long reload time though.";
		glossaryText[10] = "Earth's population is the counter in the center top part of the HUD. It is updated in real-time, accounting for general population growth as well." +
			"Each game is started with 8 billion population, which may be gradually reduced by kamikaze attacks by the aliens. Some of the population is converted into a sector's military population when building a weapons system.";
		glossaryText[11] = "Research allows unlocking and upgrading of weapons systems. Upgrades on a weapons system are implemented on already built weapons automatically. The only resource a research needs is time.";
		glossaryText[12] = "The radar is displayed in the top-right part of the HUD. It shows incoming kamikazes and the current viewing direction.";
	}

	void OnGUI() {
		GUI.Box (new Rect(Screen.width/2-2*Screen.height/3,0,Screen.height, Screen.height), "", background);
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
			scrollPosition = GUI.BeginScrollView(new Rect(0, 0, xStart, 6*yStart), scrollPosition, new Rect(0, 0, (int)((0.8)*xStart), numberOfGlossaryItems*yStart/2), false, true);
			for(int i=0; i<numberOfGlossaryItems; i++) {
				if(GUI.Button (new Rect(0, i*(yStart/2), (xStart*7)/8, yStart/2), glossaryLabels[i], glossaryStyle))
					currentGlossaryPage = i;
			}
			GUI.EndScrollView();
			switch(currentGlossaryPage) {
			case 0:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[0]);
				smallScrollPosition = GUI.BeginScrollView(new Rect(xStart, 4*yStart, 5*xStart, 2*yStart), smallScrollPosition, new Rect(0,0,5*xStart, 4*yStart), false, false);
					GUI.Label (new Rect(0, 0, 4*xStart, 4*yStart),glossaryText[0]);
				GUI.EndScrollView ();
				break;
			case 1:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[1]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[1]);
				break;
			case 2:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[2]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[2]);
				break;
			case 3:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[3]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[3]);
				break;
			case 4:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[4]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[4]);
				break;
			case 5:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[5]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[5]);
				break;
			case 6:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[6]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[6]);
				break;
			case 7:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[7]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[7]);
				break;
			case 8:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[8]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[8]);
				break;
			case 9:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[9]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[9]);
				break;
			case 10:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[10]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[10]);
				break;
			case 11:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[11]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[11]);
				break;
			case 12:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),glossaryPic[12]);
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),glossaryText[12]);
				break;

			default:
				GUI.Label (new Rect(xStart, 0, 6*xStart, 4*yStart),"");
				GUI.Label (new Rect(xStart, 4*yStart, 5*xStart, 2*yStart),"");
				break;
			}
			GUI.EndGroup ();
			if(GUI.Button (new Rect(xStart, 7*yStart, xStart, yStart/2), "Return to main menu", style))
				selected = 0;
			break;

		}
	}
}
