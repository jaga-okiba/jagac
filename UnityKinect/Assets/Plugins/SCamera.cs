using UnityEngine;
using System.Collections;

public class SCamera : MonoBehaviour {
	public GameObject target;
	public Camera cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 t = target.transform.position;
		t[1]=t[1]+10f;
		cam.transform.LookAt(t);
		

	}
}
