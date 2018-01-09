using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float cameraSpeed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * cameraSpeed, Space.World);
        }
    }
}
