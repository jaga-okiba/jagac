using UnityEngine;
using System.Collections;

public class AutoRot : MonoBehaviour {
	public float rotX = 0;
	public float rotY = 0;
	public float rotZ = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(rotX, rotY, rotZ);
	}
}