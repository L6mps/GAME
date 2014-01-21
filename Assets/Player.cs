using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static double population = 8000000000;
	private double populationGrowth = 1600000000;
	public GameObject endGame;
	public static float survival=0;
	public GameObject bigExplosion;
	public static int portalCount;
	public static int mothershipCount;
	public static int kamikazeCount;
	public static double[] slotPopulation=new double[10];
	public static float getSurvival(){
		return survival;
	}

	// Use this for initialization
	void Start () {
		survival = 0;
		population = 8000000000;
		portalCount = 0;
		mothershipCount = 0;
		kamikazeCount = 0;
		for(int i=0;i<10;i++){
			slotPopulation[i]=0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		survival+=Time.deltaTime;
		if (!IsInvoking ()) {
			Invoke ("addPopulation", 1);
		}
		if(population<=0){
			population=0;
			Application.LoadLevel (2);
			//Instantiate(endGame,transform.position,transform.rotation);
		}

	}
	void addPopulation(){
		population += (population / populationGrowth) * 0.1 * (double) Random.Range(5, 20);
	}
	void OnCollisionEnter2D(Collision2D collision){
		int slot=0;
		float decrease=  Random.Range(50000000, 100000000);
		float collisionAngle=-Mathf.Rad2Deg*Mathf.Acos (collision.transform.position.x/collision.transform.position.magnitude);
		if(collision.transform.position.x<0){
			collisionAngle=-360-collisionAngle;
		}
		if(collisionAngle<=0 && collisionAngle>=-36){
			slot=0;
		}
		for(int i=1;i<10;i++){
			if(collisionAngle<-36*i && collisionAngle>=-36*(i+1)){
				slot=i;
			}
		}
		if (Spawner.cannons[slot] != null) {
			if(slotPopulation[slot]<=decrease){
				population-=decrease-slotPopulation[slot];
				slotPopulation[slot]=0;
				Destroy (Spawner.cannons[slot]);
				Spawner.cannons[slot]=null;
			}
			else{
				slotPopulation[slot]-=decrease;
			}
		}
		else{
			population-=  decrease;
		}

	}
	void OnGUI(){
		GUI.Label (new Rect (Screen.width/2-100,10, 200, 50), ("Population: "+(long)population).ToString());

	}
}
