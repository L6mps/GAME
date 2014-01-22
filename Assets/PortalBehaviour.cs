using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour {

	public static float mothershipSpawnDelay = 20f;
	public GameObject mothership;
	public static float offset = 100f;
	private float speed;
	private float direction;
	private float state=0;
	private int dir=1;
	private Vector3 startScale;
	
	void Start () {
		startScale = transform.localScale;
		Physics2D.IgnoreLayerCollision (8, 8, true);
		transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(transform.position.x,transform.position.y,0));
		direction = (Random.value < 0.5)?(1f):(-1f);
		speed = (float) Random.Range(1,4);
		Invoke ("SpawnMothership", 3);
	}

	void Update () {
		state += dir*Time.deltaTime;
		if (state >= 3){
			dir = -1;
			state=3;
		}
		else if(state<=1){
			dir=1;
			state=1;
		}
		transform.localScale=startScale/state;
		transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(transform.position.x,transform.position.y,0));
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
	void OnCollisionEnter2D(Collision2D collision){
		Destroy (gameObject);
		Player.portalCount--;
	}
}
