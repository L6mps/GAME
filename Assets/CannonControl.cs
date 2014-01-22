using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour {
	public float angleRange=75;
	public float range=500;
	public float cooldown=1;
	public static float cooldownCannon=-1;
	public static float angleCannon = -1;
	public static float cooldownMissile=-1;
	public static float angleMissile = -1;
	public static float cooldownMine=-1;
	public static float angleMine = -1;
	private float angle=Mathf.PI/2F;
	public GameObject projectile;
	public string cannonType;
	private float mouseAngle=0;
	private float maxAngle;
	private float minAngle;
	private float objectAngle;
	private Quaternion startingRotation;
	private float reload;
	public float getCooldown(){
		return cooldown;
	}
	
	// Use this for initialization
	void Start () {
		startingRotation = transform.rotation;
		float radius=Mathf.Sqrt (Mathf.Pow (transform.position.x,2)+Mathf.Pow (transform.position.y,2));
		if(transform.position.y<0){
			objectAngle=-(Mathf.Asin((transform.position.x)/radius)+Mathf.PI);
		}
		else if(transform.position.y>=0){
			objectAngle=Mathf.Asin((transform.position.x)/radius);
		}
		objectAngle=Mathf.Rad2Deg*objectAngle;
		maxAngle=objectAngle+angleRange;
		minAngle=objectAngle-angleRange;
		angle = objectAngle;
		reload = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		switch(cannonType){
		case "cannon":
			if(cooldownCannon!=-1){
				cooldown=cooldownCannon;
			}
			if(angleCannon!=-1){
				angleRange=angleCannon;
				maxAngle=objectAngle+angleRange;
				minAngle=objectAngle-angleRange;
			}
			break;
		case "missile":
			if(cooldownMissile!=-1){
				cooldown=cooldownMissile;
			}
			if(angleMissile!=-1){
				angleRange=angleMissile;
				maxAngle=objectAngle+angleRange;
				minAngle=objectAngle-angleRange;
			}
			break;
		case "mine":
			if(cooldownMine!=-1){
				cooldown=cooldownMine;
			}
			if(angleMine!=-1){
				angleRange=angleMine;
				maxAngle=objectAngle+angleRange;
				minAngle=objectAngle-angleRange;
			}
			break;
		}
		if(reload!=cooldown){
			if(reload<cooldown)
				reload+=Time.deltaTime;
			else
				reload=cooldown;
		}
		if(Spawner.getControlledCannon()==transform.parent.name){
			Vector3 objectPos=transform.position;
			Vector3 mouse= Input.mousePosition;
			Vector3 mousePos=Camera.main.ScreenToWorldPoint (mouse);
			if(mouse.x>Screen.width/8 && mouse.x<7*Screen.width/8){
				float mouseRadius=Mathf.Sqrt (Mathf.Pow (mousePos.x-objectPos.x,2)+Mathf.Pow (mousePos.y-objectPos.y,2));
				if(mouseRadius!=0 && mousePos.y-objectPos.y<0){
					mouseAngle=-(Mathf.Asin((mousePos.x-objectPos.x)/mouseRadius)+Mathf.PI);
				}
				else if(mouseRadius!=0 && mousePos.y-objectPos.y>=0){
					mouseAngle=Mathf.Asin((mousePos.x-objectPos.x)/mouseRadius);
				}
				mouseAngle=Mathf.Rad2Deg*mouseAngle;
				if(maxAngle>90){
					if (mouseAngle < maxAngle-360 && mouseAngle+360> minAngle || mouseAngle < maxAngle && mouseAngle> minAngle) {
						transform.Rotate (0, 0, angle - mouseAngle);
						angle = mouseAngle;
					}
				}
				else if(minAngle<-270){
					if(mouseAngle-360<maxAngle && mouseAngle>minAngle+360 || mouseAngle<maxAngle && mouseAngle>minAngle){
						transform.Rotate(0,0,angle-mouseAngle);
						angle=mouseAngle;
					}
				}
				else if(mouseAngle<maxAngle && mouseAngle>minAngle){
					transform.Rotate(0,0,angle-mouseAngle);
					angle=mouseAngle;
				}
				if(angle>=360)
					angle-=360;
				if(angle<=-360)
					angle+=360;
				if(Input.GetMouseButtonDown (0) && !Input.GetKey ("left ctrl") && reload==cooldown){
					GameObject temp=((GameObject)Instantiate(projectile,transform.position,transform.rotation));
					if(temp.name=="Mine(Clone)"){
						mousePos.z=0;
						temp.GetComponent<MineBehaviour>().setTarget(mousePos.magnitude);
					}
					reload=0;
					SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
				}
			}
		}
		else if(angle!=objectAngle){
			transform.rotation = startingRotation;
			angle = objectAngle;
		}
		
	}
}