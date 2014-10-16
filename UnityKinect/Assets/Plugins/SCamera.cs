using UnityEngine;
using System.Collections;

public class SCamera : MonoBehaviour {
	public GameObject target;
	public float speed = 1.0f;
	private float time=0;
	private float dst = 1.0f;
	// Use this for initialization
	void Start () {
		Vector3 tg = target.transform.position;
		Vector3 p = this.transform.position;
		dst = Vector3.Distance (tg, p);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tg = target.transform.position;
		Vector3 p = this.transform.position;
		float r = Vector3.Distance (tg, p);

		float ang = (float)(time * Mathf.PI / 180.0f);
		float x =tg[0] + dst * Mathf.Cos (ang);
		float z =tg[2] + dst * Mathf.Sin (ang);

		Vector3 np = new Vector3 (x, p [1], z);
		this.transform.position = np;
		this.transform.LookAt(tg);


		time += speed;
		if (time > 360) {
			time = 0;
		}

		

	}
}
