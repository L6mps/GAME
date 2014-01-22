using UnityEngine;
using System.Collections;

public class ProjectileFire : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	public float range;
	public static float rangeMissile = -1;
	public static float speedCannon= -1;
	public GameObject explosion;
	Vector2 maxVel;
	float sin;
	float cos;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (9, 9, true);
		angle=(360-transform.rotation.eulerAngles.z)*Mathf.Deg2Rad;
		Vector2 newVelocity=Vector2.zero;
		sin=Mathf.Sin (angle);
		cos=Mathf.Cos (angle);
		newVelocity.x=sin*speed;
		newVelocity.y=cos*speed;
		rigidbody2D.velocity=newVelocity;
		maxVel = new Vector2 (sin * maxSpeed, cos * maxSpeed);
		currentSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.name=="Missile(Clone)"){
			if(rangeMissile!=-1){
				range=rangeMissile;
				rangeMissile=-1;
			}
		}
		else if(transform.name=="Mine(Clone"){
			if(speedCannon!=-1){
				maxSpeed=speedCannon;
				speed=speedCannon;
				speedCannon=-1;
			}
		}
		currentSpeed=rigidbody2D.velocity.x/sin;
		if (rigidbody2D.velocity != maxVel) {
			if(Mathf.Abs(rigidbody2D.velocity.x)<Mathf.Abs (maxVel.x)){

				Vector2 newVel = rigidbody2D.velocity;
				Vector2.Lerp (newVel,maxVel,acceleration*Time.deltaTime);
				rigidbody2D.AddForce (newVel);
			}
			else
				rigidbody2D.velocity=maxVel;
		}
		if(transform.position.magnitude>=range){
			if(gameObject.name.Equals("Missile(Clone)"))
				Instantiate (explosion,transform.position,transform.rotation);
			Destroy (gameObject);
		}
	
	}
	void OnCollisionEnter2D(){
		Instantiate (explosion, transform.position,transform.rotation);

				Destroy (gameObject);
		}
}
