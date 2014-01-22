using UnityEngine;
using System.Collections;

public class MineBehaviour : MonoBehaviour {
	public float acceleration=10;
	public float maxSpeed=100;
	public float speed=100;
	public float currentSpeed;
	public float angle;
	public float range;
	public static float speedMine = -1;
	private float target=float.MaxValue;
	private float stepX;
	private float stepY;
	public GameObject explosion;
	Vector2 maxVel;
	float sin;
	float cos;
	public void setTarget(float target){
		this.target = target;
	}
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision (10, 8, true);
		Physics2D.IgnoreLayerCollision (10, 9, true);
		Physics2D.IgnoreLayerCollision (10, 10, true);
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
		if(speedMine!=-1){
			speed=speedMine;
			maxSpeed=speedMine;
			speedMine=-1;
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
		if(target-transform.position.magnitude<5){
			Vector2 newVel=new Vector2(0,0);
			rigidbody2D.velocity=newVel;
			Physics2D.IgnoreLayerCollision (10,8,false);

		}

		
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(!collision.transform.name.Equals("Enemy 1(Clone)")){
			Instantiate (explosion, transform.position,transform.rotation);
			Destroy (gameObject);
		}
	}
}
