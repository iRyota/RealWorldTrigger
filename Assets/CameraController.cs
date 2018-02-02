using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera MainCamera;
    private Vector3 CameraAngle = new Vector3(0,0,0);

    private int width;
    private int height;
    private int center_x;
    private int center_y;

    void Start () {
        width = Screen.width;
        height = Screen.height;
        center_x = width / 2;
        center_y = height / 2;
    }

	// Update is called once per frame
	void Update () {
        // CameraAngle = MainCamera.transform.localEulerAngles;
        // CameraAngle.y += (Input.mousePosition.x - center_x) * 0.005f;
        // CameraAngle.x -= (Input.mousePosition.y - center_y) * 0.005f;
        // MainCamera.gameObject.transform.localEulerAngles = CameraAngle;
	}
}
