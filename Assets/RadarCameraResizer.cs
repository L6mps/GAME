using UnityEngine;
using System.Collections;

public class RadarCameraResizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int cameraRectSize = Screen.height/8;
		camera.rect = new Rect(Screen.width - cameraRectSize, Screen.height - cameraRectSize, cameraRectSize, cameraRectSize);
	}
}
