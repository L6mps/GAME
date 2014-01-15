using UnityEngine;
using System.Collections;

public class NukeBehaviour : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	Vector2 maxVel;
	float sin;
	float cos;
	Vector2 target;
	Vector2 targetTemp;
	float radius;
	Vector2 checkVel;
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
		else if(targetTemp.x/targetTemp.y!=rigidbody2D.velocity.x/rigidbody2D.velocity.y){
			if(Mathf.Abs (targetTemp.x/targetTemp.y-rigidbody2D.velocity.x/rigidbody2D.velocity.y)>0.1f){
				Vector2 oldVel=rigidbody2D.velocity;
				Vector2 force=new Vector2(oldVel.x*oldVel.x/800,oldVel.y*oldVel.y/800);
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
				force.x=-tempForce.y;
				force.y=tempForce.x;
				rigidbody2D.AddForce (force);
				Vector2 newVel=rigidbody2D.velocity;
				transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(newVel.x,newVel.y,0));
			}
			else{

				Vector2 newVel=rigidbody2D.velocity;
				newVel.x=radius*targetTemp.x/targetTemp.magnitude;
				newVel.y=newVel.x*targetTemp.y/targetTemp.x;
				Debug.Log ("x"+newVel.x+"y"+newVel.y);
				rigidbody2D.velocity=newVel;
				transform.rotation =Quaternion.LookRotation (Vector3.forward,new Vector3(newVel.x,newVel.y,0));
			}
		}
		
	}
	void OnCollisionEnter2D(){
		
		Destroy (gameObject);
	}
}
