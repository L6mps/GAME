using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public static float slowSpeed = 0.01f;
	public static float fastSpeed = 0.04f;
	private float speed;
	public GameObject explosion;
	public int speedType = 0;
	private bool isNear = false;
	
	// Use this for initialization
	void Start () {
		while(speedType == 0) {}
		if(speedType == 1)
			speed = fastSpeed;
		else
			speed = slowSpeed;
		Physics2D.IgnoreLayerCollision (8, 8, true);
		Physics2D.IgnoreLayerCollision (8, 10, true);
		Player.kamikazeCount++;
		velocityTowardsPlanet();
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
	}

	void Update() {
		if(!isNear)
			if(transform.position.magnitude < 700) {
				rigidbody2D.velocity /= 5;
				isNear = true;
			}
	}
	
	void velocityTowardsPlanet() {
		float xSpeed = -speed*transform.position.x;
		float ySpeed = -speed*transform.position.y;
		Vector2 velocity = new Vector2(xSpeed, ySpeed);
		rigidbody2D.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D collision){
		Physics2D.IgnoreLayerCollision (8, 8, true);
		Physics2D.IgnoreLayerCollision (8, 10, true);
		Instantiate (explosion,transform.position,transform.rotation);
		Destroy (gameObject);
		Player.kamikazeCount--;
	}
}