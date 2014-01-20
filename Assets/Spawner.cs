using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject cannon;
	public GameObject slot;
	public GameObject missile;
	public GameObject nuke;
	public GameObject mine;
	static GameObject[] slots =new GameObject[10];
	public static GameObject[] cannons= new GameObject[10];
	static int currentCannon = -1;
	public static string controlledCannon;
	public static string getControlledCannon(){
				return controlledCannon;
		}

	public static Vector3 getSlotPos(int slot) {
		return slots[slot].transform.position;
	}

	public static void setControlledCannonByID(int i) {
		if(cannons[i]!=null) {
			currentCannon = i;
			controlledCannon = cannons[i].name;
		}
	}

	public static int getControlledCannonByID() {
		return currentCannon;
	}

	// Use this for initialization
	void Start () {
		Vector3 newPosition = Vector3.zero;
		newPosition.y = 0;
		newPosition.x = 384;
		float angle = 0;
		Transform tran =transform;
		tran.Rotate (0,0,-90);
		for(int i=0;i<10;i++){
			slots[i]=(GameObject)Instantiate (slot,newPosition,tran.rotation);
          slots[i].transform.position=newPosition;
          slots[i].transform.rotation=tran.rotation;
          angle-=36;
          newPosition.x=384*Mathf.Cos (angle*Mathf.Deg2Rad);
          newPosition.y=384*Mathf.Sin ((angle)*Mathf.Deg2Rad);
			tran.Rotate (0,0,-36);
  		}
		tran.Rotate (0,0,90);

	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && Input.GetKey ("left ctrl")) {
			Vector3 newPosition = Vector3.zero;
			Vector3 mouse = Input.mousePosition;
			mouse.z = 1000;
			newPosition = Camera.main.ScreenToWorldPoint (mouse);
			newPosition.z = 0;
			for (int i=0; i<10; i++) {
				if (Mathf.Abs (newPosition.x - slots [i].transform.position.x) < 50 &&
				    Mathf.Abs (newPosition.y - slots [i].transform.position.y) < 50) {
					if(cannons[i]!=null){
						if(controlledCannon==cannons[i].name){
							Destroy (cannons[i]);
							switch(SlotProperties.selectedWeapon){
							case(1): {cannonSpawn (i,cannon);
								break;}
							case(2): {cannonSpawn (i,missile);
								break;}
							case(3): {cannonSpawn (i,nuke);
								break;}
							case(4): {cannonSpawn (i,mine);
								break;}
							}
						}
						controlledCannon = cannons [i].name;
						currentCannon = i;
					}
					else{
						switch(SlotProperties.selectedWeapon){
						case(1): {cannonSpawn (i,cannon);
							break;}
						case(2): {cannonSpawn (i,missile);
							break;}
						case(3): {cannonSpawn (i,nuke);
							break;}
						case(4): {cannonSpawn (i,mine);
							break;}
						}
					}
					
				}
			}
		}
		
	}
	void cannonSpawn(int i,GameObject launcher){
		cannons [i] = (GameObject)Instantiate
		(launcher, slots[i].transform.position, slots[i].transform.rotation);
		cannons [i].name = cannons [i].name + i;
		Player.slotPopulation[i]=200000000;
		Player.population -= 200000000;
	}
}
