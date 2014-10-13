using UnityEngine;
using System.Collections;

public class StaffRoll : MonoBehaviour {

	private float st = -14f;
	private float ed =  14f;
	private float sp = 0.03f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		pos [1] += sp;
		this.transform.position = pos;
		
		
		if (pos [1] > ed) {
			Application.LoadLevel ("InterReplay");
		}
	}
}
