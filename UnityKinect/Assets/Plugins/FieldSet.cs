using UnityEngine;
using System.Collections;

public class FieldSet : MonoBehaviour {
	// {x,y,r}
	//private float[][] form = {{0,0,0},{87,0,0}}; 
	
	private float[,] form = new float[,] { {-10,0,0},{82.5f,135,180},{-45,123,90},{-71,-35,90} };
	//private int id = 0;
	private int temp=0;
	
	// Use this for initialization
	void Start () {
		setPlace ();
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void setPlace(){
		int id = temp;
		while(id==temp){
			id=(int)Random.Range (0, 4);
		}

		if (id != temp) {
			this.transform.position = new Vector3 (form [id, 0], 0, form [id, 1]);
			this.transform.Rotate (new Vector3 (0, form [id, 2] -form [temp, 2] , 0));
		}
		
		temp = id;
	}
}


/*


using UnityEngine;
using System.Collections;

public class FieldSet : MonoBehaviour {
	// {x,y,r}
	//private float[][] form = {{0,0,0},{87,0,0}}; 

	private float[,] form = new float[,] { {-10,0,0},{88,135,180},{-45,123,90},{-71,-35,90} };

	//public int id = 0;
	// Use this for initialization
	void Start () {
		setPlace ();
	}



	// Update is called once per frame
	void Update () {
	
	}

	public void setPlace(){
		int id = (int)Random.Range (0, 4);
		this.transform.position = new Vector3 (form [id,0], 0, form [id,1]);
		this.transform.Rotate(new Vector3(0,form[id,2],0));

	}
}
*/

