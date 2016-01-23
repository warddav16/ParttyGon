using UnityEngine;
using System.Collections;

public class FreeMoveCam : MonoBehaviour {

	public float minX = -360;
	public float maxX = 360;

	public float minY = -45;
	public float maxY = 45;

	public float sensX = 100;
	public float sensY = 100;

	public float zoomSpeed =2;
	float rotationY = 0;
	float rotationX =0;

	float sensitivity = 15;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		Camera.main.transform.Translate(new Vector3(xAxisValue,0,zAxisValue));

		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		transform.Translate (0, scroll * zoomSpeed, 0);


		if(Input.GetMouseButton(0)){

			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") *sensitivity;
			rotationY = Mathf.Clamp(rotationY,minY,maxY);
			transform.localEulerAngles  = new Vector3(-rotationY,rotationX,0);


		}
	}
}
