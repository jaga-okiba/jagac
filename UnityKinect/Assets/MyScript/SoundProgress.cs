using UnityEngine;
using System.Collections;

public class SoundProgress : MonoBehaviour {
	public UISprite bar;
	public MyRec rec = null;
	public UILabel nguiText = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float time = rec.getTime ();
		float delay = rec.getDelay ();
		if (time < delay) {
			nguiText.text = "" + (delay - (int)time);
			bar.fillAmount = 1 - (time % 1.0f);
			print (1 - (time % 1.0f));
		} else {
			nguiText.text = "";
			bar.fillAmount = 0;
		}
	}
}
