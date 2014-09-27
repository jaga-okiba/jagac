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

		/*
		Vector3 p = new Vector3();
		for(int i=0;i<3;i++){
			p[i]=t[i]-13;
		}
		cam.transform.position=p;
		*/

		t[1]=t[1]+10f;
		cam.transform.LookAt(t);
		

	}
}
