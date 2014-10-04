using UnityEngine;
using System.Collections;

public class CharacterDecide : MonoBehaviour {
	public Controller Controller;
	public GameObject LGrab;
	public GameObject RGrab;

	private bool LFlag = false;
	private bool RFlag = false;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "LGrab"){
			LFlag = true;
		}

		if (col.gameObject.name == "RGrab"){
			RFlag = true;
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.name == "LGrab"){
			LFlag = false;
		}
		
		if (col.gameObject.name == "RGrab"){
			RFlag = false;
		}
	}

	// Update is called once per frame
	void Update () {
	
		if (LFlag == true && RFlag == true){
			Controller.ChangeScene();
		}

	}
}
