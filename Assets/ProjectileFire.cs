﻿using UnityEngine;
using System.Collections;

public class ProjectileFire : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	public float range;
	public static float rangeMissile = 1000;
	public static float speedCannon= 50;
	public GameObject explosion;
	Vector2 maxVel;
	float sin;
	float cos;
	// Use this for initialization
	void Start () {
		if(transform.name=="Missile(Clone)"){
			if(rangeMissile!=1000){
				range=rangeMissile;
			}
		}
		else if(transform.name==("Cube(Clone)")){
			if(speedCannon!=50){
				maxSpeed=speedCannon;
				speed=speedCannon;
			}
		}
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
