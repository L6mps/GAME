﻿using UnityEngine;
using System.Collections;

public class MothershipBehaviour : MonoBehaviour {
	private float direction;
	public float speed = 5;
	// Use this for initialization
	void Start () {
		direction = (Random.value < 0.5)?(1f):(-1f);
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		Player.mothershipCount++;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt(new Vector3(0,0,0), Vector3.back);
		transform.position=Quaternion.Euler (0,0,direction*Time.deltaTime*speed)*transform.position;
	}
}
