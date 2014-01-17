using UnityEngine;
using System.Collections;

public class NukeBehaviour : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	public GameObject explosion;
	Vector2 maxVel;
	float sin;
	float cos;
	Vector2 target;
	Vector2 targetTemp;
	float radius;
	Vector2 checkVel;
	int direction=1;
	// Use this for initialization
	void Start () {
		
		Physics2D.IgnoreLayerCollision (9, 9, true);
		angle=(360-transform.rotation.eulerAngles.z)*Mathf.Deg2Rad;
		Vector2 newVelocity=Vector2.zero;
		sin=Mathf.Sin (angle);
		cos=Mathf.Cos (angle);
		newVelocity.x=sin*speed;
		newVelocity.y=cos*speed;
		target = new Vector2 (2000, 2000);
		targetTemp = target;
		float targetAngle=Mathf.Asin (target.y/target.magnitude);
		if(target.x<0){
			if(target.y<0){
				targetAngle-=Mathf.PI/2;
			}
			else{
				targetAngle+=Mathf.PI/2;
			}
		}
		float objectAngle=Mathf.Asin (transform.position.y/(new Vector2(transform.position.x,transform.position.y)).magnitude);
		//Debug.Log (targetAngle + " " + objectAngle);
		if(transform.position.x<0){
			if(transform.position.y<0){
				objectAngle-=Mathf.PI/2;
			}
			else{
				objectAngle+=Mathf.PI/2;
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
		radius = maxVel.magnitude;
		currentSpeed = speed;
		checkVel = newVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		targetTemp.x = target.x - transform.position.x;
		targetTemp.y = target.y - transform.position.y;
		currentSpeed=rigidbody2D.velocity.magnitude;
		if (checkVel != maxVel) {
			if(Mathf.Abs(rigidbody2D.velocity.x)<Mathf.Abs (maxVel.x)){
				Vector2 newVel = rigidbody2D.velocity;
				Vector2.Lerp (newVel,maxVel,acceleration*Time.deltaTime);
				rigidbody2D.AddForce (newVel);
				checkVel=rigidbody2D.velocity;
			}
			else{
				rigidbody2D.velocity=maxVel;
				checkVel=maxVel;
			}
		}
		else if(targetTemp.x/targetTemp.magnitude!=rigidbody2D.velocity.x/rigidbody2D.velocity.magnitude){
			if(Mathf.Abs (targetTemp.x/targetTemp.magnitude-rigidbody2D.velocity.x/rigidbody2D.velocity.magnitude)>0.1f){
				Vector2 oldVel=rigidbody2D.velocity;
				Vector2 force=new Vector2(oldVel.x*oldVel.x/400,oldVel.y*oldVel.y/400);
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
				rigidbody2D.AddForce (force);
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
		if(Mathf.Abs (transform.position.x-target.x)<100 && Mathf.Abs (transform.position.y-target.y)<100){
			Instantiate(explosion,transform.position,transform.rotation);
			Destroy (gameObject);
		}
	}
}
