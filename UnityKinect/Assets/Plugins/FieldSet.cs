using UnityEngine;
using System.Collections;

public class FieldSet : MonoBehaviour {
	// {x,y,r}
	//private float[][] form = {{0,0,0},{87,0,0}}; 
	
	private float[,] form = new float[,] {
		{-10, 0, 0},
		{82.5f, 135, 180},
		{-45, 123, 90},
		{-71, -35, 90} };
	
	private float[,] dLight = new float[,] {
		{35, 0, 180},
		{40, 25, 210},
		{60, 0, 0},
		{60, 300, 230} }; 

	private int id = 0;
	private int temp=0;
	
	// Use this for initialization
	void Start () {
		setPlace ();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setPlace(){
		while (id==temp) {
			id = (int)Random.Range (0, 4);
		}
		if (id != temp) {
			print("id "+id);
			this.transform.position = new Vector3 (form [id, 0], -1, form [id, 1]);
			this.transform.rotation=Quaternion.Euler(new Vector3 (0, form [id, 2], 0));
			GameObject.Find ("Directional light").transform.rotation = Quaternion.Euler(dLight[id, 0], dLight[id, 1], dLight[id, 2]);
		}
		
		temp = id;
	}
}
