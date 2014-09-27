using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class MyKinectData : MonoBehaviour {
	private MySensor[] sensor;
	private string modelName;
	public GameObject body;
	private int frame;

	public static string[] bName = {
		"SpineRoot","Spine2","HeadRoot","Head",
		"LShoulder","LArmRoot","LForearm","LHand","LHandEff",
		"RShoulder","RArmRoot","RForearm","RHand","RHandEff",
		"Spine1",
		"LLegRoot","LShin","LRootFoot","LToe",
		"RLegRoot","RShin","RRootFoot","RToe"
	};


	// Use this for initialization
	void Start () {
	}

	public GameObject getBody(){
		return body;
	}

	public void setFrame(int f){
		frame = f;
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

						float tx =  float.Parse(line[1]);
						float ty =  float.Parse(line[2]);
						float tz =  float.Parse(line[3]);
						Vector3 p = new Vector3(tx,ty,tz);
						
						float rx =  float.Parse(line[4]);
						float ry =  float.Parse(line[5]);
						float rz =  float.Parse(line[6]);
						Vector3 r = new Vector3(rx,ry,rz);

						int id = getBodyPartID(line[0]);
						sensor[id].addData(p,r);
					}
				}
				lineNum++;


			}

			//datas.Add (sensor);
			reader.Close();
		}
	
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
	public void run (int f) {
		setFrame (f);
		if (body != null) {

			Component[] pt = body.GetComponentsInChildren<Component> ();
			for(int i=0;i<pt.Length;i++){
				int id = getBodyPartID (pt[i].gameObject.name);
				//print ("hoge "+id + " "+pt[i].gameObject.name);


				if(id>=0){
					Vector3 pos = this.sensor[id].getPos (frame);
					Vector3 rot = this.sensor[id].getRot (frame);

					//Vector3 hogeRot = new Vector3(0.0f,90.0f,0.0f);

					//pt[i].gameObject.transform.position=pos;
					pt[i].gameObject.transform.eulerAngles=rot;

					//print (frame+" [pos] "+rot);

				}
			}
		}

	}
}
