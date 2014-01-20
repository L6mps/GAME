using UnityEngine;
using System.Collections;

public class MothershipBehaviour : MonoBehaviour {
	private float direction;
	private float speed;
	public GameObject enemy;
	private float offset = 50f;
	private float EnemySpawnDelay = 20f;
	
	void Start () {
		direction = (Random.value < 0.5)?(1f):(-1f);
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		Player.mothershipCount++;
		speed = (float) Random.Range(2,5);
		Invoke ("SpawnEnemy", 3);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		transform.position=Quaternion.Euler (0,0,direction*Time.deltaTime*speed)*transform.position;
		if(!IsInvoking()) 
			Invoke ("SpawnEnemy", EnemySpawnDelay);

		//debug code, d for slow enemies
		if(Input.GetKeyUp("d")) {
			SpawnEnemy();
		}
	}

	void SpawnEnemy() {
		int rnd = Random.Range (0,2);
		bool fastEnemies = false;
		if(rnd == 1)
			fastEnemies = true;
		for(int i=0; i<(fastEnemies == true ? 3 : 10); i++) {
			Vector3 newPos = transform.position;
			Vector2 newRand = Random.insideUnitCircle;
			newPos.x += offset*newRand.x;
			newPos.y += offset*newRand.y;
			GameObject bla = (GameObject) Instantiate(enemy, newPos, Quaternion.identity);
			bla.GetComponent<EnemyBehaviour>().speedType = fastEnemies == true ? 1 : -1;
		}
	}
}
