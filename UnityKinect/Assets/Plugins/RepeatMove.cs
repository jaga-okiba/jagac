using UnityEngine;
using System.Collections;

public class RepeatMove : MonoBehaviour {
	
	public float moveX = 0f;
	public int distanceX = 0;
	public bool reverseX = false;
	
	public float moveY = 0f;
	public int distanceY = 0;
	public bool reverseY = false;
	
	public float moveZ = 0f;
	public int distanceZ = 0;
	public bool reverseZ = false;
	
	private float x;
	private int cntX = 0;
	
	private float y;
	private int cntY = 0;
	
	private float z;
	private int cntZ = 0;
	
	// Use this for initialization
	void Start () {
		x = this.transform.position.x; 
		y = this.transform.position.y;
		z = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (reverseX == false && cntX < distanceX) {
			x += moveX;
			cntX++;
		} else if (reverseX == false && cntX >= distanceX) {
			reverseX = true;
			cntX = 0;
		} else if (reverseX == true && cntX < distanceX) {
			x -= moveX;
			cntX++;
		} else if (reverseX == true && cntX >= distanceX) {
			reverseX = false;
			cntX = 0;
		}
		
		if (reverseY == false && cntY < distanceY) {
			y += moveY;
			cntY++;
		} else if (reverseY == false && cntY >= distanceY) {
			reverseY = true;
			cntY = 0;
		} else if (reverseY == true && cntY < distanceY) {
			y -= moveY;
			cntY++;
		} else if (reverseY == true && cntY >= distanceY) {
			reverseY = false;
			cntY = 0;
		}
		
		if (reverseZ == false && cntZ < distanceZ) {
			z += moveZ;
			cntZ++;
		} else if (reverseZ == false && cntZ >= distanceZ) {
			reverseZ = true;
			cntZ = 0;
		} else if (reverseZ == true && cntZ < distanceZ) {
			z -= moveZ;
			cntZ++;
		} else if (reverseZ == true && cntZ >= distanceZ) {
			reverseZ = false;
			cntZ = 0;
		}
		
		this.transform.position = new Vector3(x, y, z);
	}
}
