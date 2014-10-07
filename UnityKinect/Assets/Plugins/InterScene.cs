using UnityEngine;
using System.Collections;

public class InterScene : MonoBehaviour {
	public bool moveToPlay = true;
	public bool moveToRePlay = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (moveToPlay) {
			Application.LoadLevel("LetsDance");
		}
		if (moveToRePlay) {
			Application.LoadLevel("LetsReplay");
		}
	}
}
