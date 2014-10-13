using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class MyKinectData : MonoBehaviour {
	private MySensor[] sensor;
	private string modelName;
	public GameObject body;
	private int maxFrame;
	public static string[] bName = {
		"SpineRoot","Spine2","HeadRoot","Head",
		"LShoulder","LArmRoot","LForearm","LHand","LHandEff",
		"RShoulder","RArmRoot","RForearm","RHand","RHandEff",
		"Spine1",
		"LLegRoot","LShin","LRootFoot","LToe",
		"RLegRoot","RShin","RRootFoot","RToe"
	};
	private float yAngle = 0f;

	// Use this for initialization
	void Start () {
	}
	public void setYAngle(float y){
		yAngle=y;
	}
	public GameObject getBody(){
		return body;
	}
	public void load(string fname,GameObject[] modelList){
		sensor = new MySensor[bName.Length];
		for (int i=0; i<bName.Length; i++) {
			sensor[i]=new MySensor(bName[i]);
		}

		FileStream f = new FileStream(fname, FileMode.Open, FileAccess.Read);
		StreamReader reader = new StreamReader(f);
		if (reader != null) {
			int lineNum=0;

			while (!reader.EndOfStream) {
				string str = reader.ReadLine();
				string[] line = str.Split();
				if(lineNum==0){
					modelName=line[0];
					for(int i=0;i<modelList.Length;i++){


						if(modelList[i].name==modelName){
							body = (GameObject)Instantiate(modelList[i]);
							body.SetActive(true);

							break;
						}
					}
				}
				else{
					if(line.Length<7){
						print (fname+" "+lineNum+"  "+str);
					}
					else{
						float time = float.Parse(line[1]);
						float tx =  float.Parse(line[2]);
						float ty =  float.Parse(line[3]);
						float tz =  float.Parse(line[4]);
						Vector3 p = new Vector3(tx,ty,tz);
						
						float rx =  float.Parse(line[5]);
						float ry =  float.Parse(line[6]);
						float rz =  float.Parse(line[7]);
						Vector3 r = new Vector3(rx,ry,rz);

						int id = getBodyPartID(line[0]);
						sensor[id].addData(time,p,r);
					}
				}
				lineNum++;


			}
			maxFrame = sensor[0].getMaxFrame();
			for(int i=1;i<sensor.Length;i++){
				if(maxFrame<sensor[i].getMaxFrame()){
					maxFrame = sensor[i].getMaxFrame();
				}
			}

			reader.Close();
		}
	
	}

	public int getMaxFrame(){
		return maxFrame;
	}

	public int getBodyPartID(string n){
		int id = -1;
		for(int i=0;i<bName.Length;i++){
			if(bName[i]==n){
				id=i;
				break;
			}
		}
		return id;
	}

	public void printData(string pname,int frame){
		int id = getBodyPartID(pname);
		Vector3 pos = sensor [id].getPos(frame);
		print ("pos " + pos [0] + " " + pos [1] + " " + pos [2]);
	}

	// Update is called once per frame
	void Update () {
	}
	public void run (float sec) {
		if (body != null) {

			Component[] pt = body.GetComponentsInChildren<Component> ();
			for(int i=0;i<pt.Length;i++){
				int id = getBodyPartID (pt[i].gameObject.name);

				if(id>=0){
					Vector3 pos = this.sensor[id].getPos (sec);
					Vector3 rot = this.sensor[id].getRot (sec);

					rot[1]+=yAngle;
					pt[i].gameObject.transform.eulerAngles=rot;

				}
			}
		}

	}
}
