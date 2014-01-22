using UnityEngine;
using System.Collections;

public class NukeControl : MonoBehaviour {
	public float angleRange=75;
	public float range=500;
	public float cooldown=1;
	private float angle=Mathf.PI/2F;
	public GameObject projectile;
	public string cannonType;
	public static float cooldownNuke=-1;
	public static bool angleNuke=false;
	private float mouseAngle=0;
	private float maxAngle;
	private float minAngle;
	private float objectAngle;
	private Quaternion startingRotation;
	private float reload;
	public Sprite motion1;
	public Sprite motion2;
	public Sprite motion3;
	public Sprite motion4;
	public Sprite motion5;
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
		if(cooldownNuke!=-1){
			cooldown=cooldownNuke;
			cooldownNuke=-1;
		}
		if(reload!=cooldown){
			float percent=reload/cooldown;
			if(percent<0.25){
				transform.parent.GetComponentInChildren<SpriteRenderer>().sprite=motion1;
			}
			else if(percent<0.50){
				transform.parent.GetComponentInChildren<SpriteRenderer>().sprite=motion2;
			}
			else if(percent<0.75){
				transform.parent.GetComponentInChildren<SpriteRenderer>().sprite=motion3;
			}
			else if(percent<1){
				transform.parent.GetComponentInChildren<SpriteRenderer>().sprite=motion4;
			}
			if(reload<cooldown)
				reload+=Time.deltaTime;
			else
				reload=cooldown;
			if(reload==cooldown){
				transform.parent.GetComponentInChildren<SpriteRenderer>().sprite=motion5;
			}
		}
		if(Spawner.getControlledCannon()==transform.parent.name){
			Vector3 objectPos=transform.position;
			Vector3 mouse= Input.mousePosition;    
			mouse.z=0;
			Vector3 mousePos=Camera.main.ScreenToWorldPoint (mouse);
			if(mouse.x>Screen.width/8 && mouse.x<7*Screen.width/8){
				/*float mouseRadius=Mathf.Sqrt (Mathf.Pow (mousePos.x-objectPos.x,2)+Mathf.Pow (mousePos.y-objectPos.y,2));
				if(mouseRadius!=0 && mousePos.y-objectPos.y<0){
					mouseAngle=-(Mathf.Asin((mousePos.x-objectPos.x)/mouseRadius)+Mathf.PI);
				}
				else if(mouseRadius!=0 && mousePos.y-objectPos.y>=0){
					mouseAngle=Mathf.Asin((mousePos.x-objectPos.x)/mouseRadius);
				}
				mouseAngle=Mathf.Rad2Deg*mouseAngle;
				if(maxAngle>90){
					if (mouseAngle < maxAngle-360 && mouseAngle+360> minAngle || mouseAngle < maxAngle && mouseAngle> minAngle) {
						((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
						reload=0;
						SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
						angle = mouseAngle;
					}
				}
				else if(minAngle<-270){
					if(mouseAngle-360<maxAngle && mouseAngle>minAngle+360 || mouseAngle<maxAngle && mouseAngle>minAngle){
						((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
						reload=0;
						SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
						angle=mouseAngle;
					}
				}
				else if(mouseAngle<maxAngle && mouseAngle>minAngle){
					((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
					reload=0;
					SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
					angle=mouseAngle;
				}
				if(angle>=360)
					angle-=360;
				if(angle<=-360)
					angle+=360;*/
				if(Input.GetMouseButtonDown (0)&& !Input.GetKey ("left ctrl") && reload==cooldown){
					if(angleNuke){
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
								((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
								reload=0;
								SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
								angle = mouseAngle;
							}
						}
						else if(minAngle<-270){
							if(mouseAngle-360<maxAngle && mouseAngle>minAngle+360 || mouseAngle<maxAngle && mouseAngle>minAngle){
								((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
								reload=0;
								SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
								angle=mouseAngle;
							}
						}
						else if(mouseAngle<maxAngle && mouseAngle>minAngle){
							((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
							reload=0;
							SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
							angle=mouseAngle;
						}
						if(angle>=360)
							angle-=360;
						if(angle<=-360)
							angle+=360;
					}
					else{
						((GameObject)Instantiate(projectile,transform.position,transform.rotation)).GetComponent<NukeBehaviour>().setTarget(new Vector2(mousePos.x,mousePos.y));
						reload=0;
						SideHUDLeft.moveProgressBar(Spawner.getControlledCannonByID(),cooldown);
					}
				}
			}
		}
		else if(angle!=objectAngle){
			transform.rotation = startingRotation;
			angle = objectAngle;
		}
		
	}
}