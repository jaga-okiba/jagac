using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class MyRec : MonoBehaviour {
	public AudioSource music = null;
	public MyReplay replay;

	public float delayTime=3;
	private float recTime=-1;

	private float time=0;
	public GUIText guiCount;


	private int myState = -1;
	private StreamWriter writer;
	private GameObject[] bodys; // = new GameObject[25];
	private string charaName;


	private string[] bName = {
		"SpineRoot","Spine2","HeadRoot","Head",
		"LShoulder","LArmRoot","LForearm","LHand","LHandEff",
		"RShoulder","RArmRoot","RForearm","RHand","RHandEff",
		"Spine1",
		"LLegRoot","LShin","LRootFoot","LToe",
		"RLegRoot","RShin","RRootFoot","RToe"
	};
	public GameObject target;

	void Start () {
		this.renderer.material.color = Color.blue;

		string name = PlayerPrefs.GetString("CharaName");
		if(name==null){
			name = "human_base";
		}

		GameObject list = GameObject.Find ("playerList");
		int chCnt = list.transform.childCount;
		for (int i=0; i<chCnt; i++) {
			GameObject child = list.transform.GetChild(i).gameObject;
			if(child.name==name){
				//copy from list
				target = (GameObject)Instantiate(child);
				target.SetActive(true);
				break;
			}
		}


		charaName = name;
		setBodys ();

		//music is started by MyRelay.cs
		recTime = replay.getMaxTime ();
		print ("rec time " + recTime);



	}

	public void setBodys(){

		Component[] children = target.GetComponentsInChildren<Component> ();
		bodys = new GameObject[bName.Length];
		for(int i=0;i<children.Length;i++){
			for(int j=0;j<bName.Length;j++){
				if(children[i].gameObject.name==bName[j]){
					bodys[j]=children[i].gameObject;
					break;
				}
			}
		}



	}
	
	// Update is called once per frame
	void Update () {

		if (music != null) {
			time += Time.deltaTime;
		} else {
			time = music.time;
		}


		if (myState == -1) {
			if (time < delayTime) {
				float count_time = delayTime - time;
				guiCount.text = "READY " + (int)count_time;
			}else{
				guiCount.text = "";
				rec ();
			}
		}
		else if(myState==1){
			guiCount.text = "";

			for(int i=0;i<bodys.Length;i++){

				Vector3 pos = bodys[i].transform.position;
				Vector3 rot = bodys[i].transform.rotation.eulerAngles;
				string name = bodys[i].name;

				string str =name+" " +time+" "+pos[0]+" "+pos[1]+" "+pos[2]+" "+rot[0]+" "+rot[1]+" "+rot[2];
				
			
				writer.WriteLine(str);
				
			}
			
			if(time>=recTime){
				rec();
				Application.LoadLevel("InterReplay");
			}

		}
		
		
	}
	
	public void rec(){
		if(myState==-1){
			guiCount.text = "REC " + (int)time;
			this.renderer.material.color=Color.red;

			DateTime dtNow = DateTime.Now;
			string outName = dtNow.ToString("yyyy_MM_dd_HH_mm_ss")+".dat";
			print(outName);

			FileStream f = new FileStream("myRecord/"+outName, FileMode.Create, FileAccess.Write);
			writer = new StreamWriter(f);
			writer.WriteLine(charaName);
			print(charaName);
			myState=1;

		}
		else{
			time = 0;
			myState=-1;
			this.renderer.material.color=Color.blue;
			writer.Close();
		}
	}
}
