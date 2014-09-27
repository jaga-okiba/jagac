using UnityEngine;
using System.Collections;

public class AutoRot : MonoBehaviour {
	public float rot = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0, rot, 0);
	}
}
