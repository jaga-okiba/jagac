using UnityEngine;
using System.Collections;

public class MyGrab : MonoBehaviour {
	public CharacterChange turntable;
	public bool right=true;
	public bool left=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {}
	void OnCollisionEnter(Collision col){
		if(right){
			turntable.Turn_Right ();
		}
		if(left){
			turntable.ChangeScene();
		}
	}

}
