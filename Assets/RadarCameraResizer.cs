using UnityEngine;
using System.Collections;

public class RadarCameraResizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int cameraRectSize = Screen.width/8;
		camera.pixelRect = new Rect(Screen.width - cameraRectSize, Screen.height - cameraRectSize, cameraRectSize, cameraRectSize);
	}
}
