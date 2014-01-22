using UnityEngine;
using System.Collections;

public class NukeBehaviour : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	public GameObject explosion;
	public static float maxSpeedNuke = -1;
	private Vector2 maxVel;
	private float sin;
	private float cos;
	private Vector2 target;
	private Vector2 targetTemp;
	private Vector2 checkVel;
	private int direction=1;
	private float radius=0;
	// Use this for initialization
	public void setTarget(Vector2 target){
		this.target = target;
	}
	void Start () {
		Physics2D.IgnoreLayerCollision (9, 9, true);
		angle=(360-transform.rotation.eulerAngles.z)*Mathf.Deg2Rad;
		Vector2 newVelocity=Vector2.zero;
		sin=Mathf.Sin (angle);
		cos=Mathf.Cos (angle);
		newVelocity.x=sin*speed;
		newVelocity.y=cos*speed;
		float targetAngle=Mathf.Asin (target.y/target.magnitude);
		if(target.x<0){
			if(target.y<0){
				targetAngle=-Mathf.PI-targetAngle;
			}
			else{
				targetAngle=Mathf.PI-targetAngle;
			}
		}
		float objectAngle=Mathf.Asin (transform.position.y/(new Vector2(transform.position.x,transform.position.y)).magnitude);
		if(transform.position.x<0){
			if(transform.position.y<0){
				objectAngle=-Mathf.PI-objectAngle;
			}
			else{
				objectAngle=Mathf.PI-objectAngle;
			}
		}
		if(Mathf.Abs (objectAngle-targetAngle)<Mathf.PI){
			if(objectAngle>targetAngle){
				direction=-1;
			}
		}
		else if(objectAngle<targetAngle){
			direction=-1;
		}
		rigidbody2D.velocity=newVelocity;
		maxVel = new Vector2 (sin * maxSpeed, cos * maxSpeed);
		currentSpeed = speed;
		checkVel = newVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		if(maxSpeedNuke!=-1){
			maxSpeed=maxSpeedNuke;
			maxSpeedNuke=-1;
		}
		targetTemp.x = target.x - transform.position.x;
		targetTemp.y = target.y - transform.position.y;
		currentSpeed=rigidbody2D.velocity.magnitude;
		if (checkVel != maxVel) {
			if(Mathf.Abs(rigidbody2D.velocity.x)<Mathf.Abs (maxVel.x)){
				Vector2 newVel = rigidbody2D.velocity;
				Vector2.Lerp (newVel,maxVel,acceleration*Time.deltaTime);
				rigidbody2D.AddForce (1000*newVel);
				checkVel=rigidbody2D.velocity;
			}
			else{
				rigidbody2D.velocity=maxVel;
				checkVel=maxVel;
				radius=(new Vector2(transform.position.x-target.x,transform.position.y-target.y)).magnitude/2;
			}
		}
		else if(targetTemp.x/targetTemp.magnitude!=rigidbody2D.velocity.x/rigidbody2D.velocity.magnitude){
			if(Mathf.Abs (targetTemp.x/targetTemp.y-rigidbody2D.velocity.x/rigidbody2D.velocity.y)>0.5f || targetTemp.x/rigidbody2D.velocity.x<0 || targetTemp.y/rigidbody2D.velocity.y<0){
				Vector2 oldVel=rigidbody2D.velocity;
				Vector2 force=new Vector2(oldVel.x*oldVel.x/radius,oldVel.y*oldVel.y/radius);
				if(oldVel.x<0){
					force.x=-force.x;
					if(oldVel.y<0){
						force.y=-force.y;
					}
				}
				else{
					if(oldVel.y<0){
						force.y=-force.y;
					}
				}
				Vector2 tempForce=force;
				force.x=-tempForce.y*direction;
				force.y=tempForce.x*direction;
				rigidbody2D.AddForce (1000*force);
				Vector2 newVel=rigidbody2D.velocity;
				transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(newVel.x,newVel.y,0));
			}
			else{

				Vector2 newVel=rigidbody2D.velocity;
				newVel.x=newVel.magnitude*targetTemp.x/targetTemp.magnitude;
				newVel.y=newVel.magnitude*targetTemp.y/targetTemp.magnitude;
				rigidbody2D.velocity=newVel;
				transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(newVel.x,newVel.y,0));
			}
		}
		if(Mathf.Abs (transform.position.x-target.x)<10 && Mathf.Abs (transform.position.y-target.y)<10){
			Instantiate(explosion,transform.position,transform.rotation);
			Destroy (gameObject);
		}
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.transform.name.Equals("Mothership 2(Clone)")){
		   Instantiate(explosion,transform.position,transform.rotation);
		   Destroy (gameObject);
		}
	}
}
