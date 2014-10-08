using UnityEngine;
using System.Collections;

public class CharacterDecide : MonoBehaviour {
	public Controller Controller;
	public GameObject LGrab;
	public GameObject RGrab;
	
	private bool LFlag = false;
	private bool RFlag = false;
	
	public AudioClip decideSound;
	private AudioSource decideSoundSource;
	// Use this for initialization
	void Start () {
		decideSoundSource = gameObject.GetComponent<AudioSource> ();
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
			decideSoundSource.PlayOneShot (decideSound);
			System.Threading.Thread.Sleep(2000);
			Controller.ChangeScene();
		}
		
	}
}
