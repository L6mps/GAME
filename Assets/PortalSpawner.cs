using UnityEngine;
using System.Collections;

public class PortalSpawner : MonoBehaviour {
	
	public GameObject portal;
	public static float minPortalDistance = 1500f;
	public static float maxPortalDistance = 2000f;
	public static float portalSpawnRate = 45f;

	void Start() {
		Invoke ("SpawnPortal", 5);
	}

	// Update is called once per frame
	void Update () { 
		if(!IsInvoking())
			Invoke ("SpawnPortal", portalSpawnRate>1?Mathf.Round (portalSpawnRate):1);

		//debug code, press a to spawn a portal
		if(Input.GetKeyUp ("a")) {
			SpawnPortal();
		}
	}

	void SpawnPortal() {
		Vector2 randomPos = new Vector2(0,0);
		while(Mathf.Sqrt(randomPos.x*randomPos.x + randomPos.y*randomPos.y)<minPortalDistance)
			randomPos = Random.insideUnitCircle * maxPortalDistance;
		Vector3 newPos = new Vector3(randomPos.x, randomPos.y, 0);
		Instantiate (portal, newPos, new Quaternion(0,0,0,0));
		Player.portalCount++;
		portalSpawnRate *= 0.92f;
	}
}
