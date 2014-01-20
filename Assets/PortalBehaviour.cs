using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour {

	public static float mothershipSpawnDelay = 20f;
	public GameObject mothership;
	public static float offset = 100f;
	private float speed;
	private float direction;
	
	void Start () {
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		direction = (Random.value < 0.5)?(1f):(-1f);
		speed = (float) Random.Range(1,4);
		Invoke ("SpawnMothership", 3);
	}

	void Update () {
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		if(!IsInvoking())
			Invoke ("SpawnMothership", mothershipSpawnDelay);
		transform.position=Quaternion.Euler (0,0,direction*Time.deltaTime*speed)*transform.position;
		//debug code, s for mothership
		if(Input.GetKeyUp("s")) {
			SpawnMothership ();
		}
	}

	void SpawnMothership() {
		Vector3 newPos = transform.position;
		Vector2 newRand = Random.insideUnitCircle;
		newPos.x += offset*newRand.x;
		newPos.y += offset*newRand.y;
		Instantiate(mothership, newPos, Quaternion.identity);
	}
}
