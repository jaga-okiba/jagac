using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using UnityEditor;

public class BvaAnim : MonoBehaviour {


	public string fname="samiNewSoran.vpm";
	public string setting="map15seg.dat";
	public Boolean hipMove=true;
	//public Boolean tpose = true;
	public float mocapScale=-1.0f;
	public int fixFrame=-1;
	public AudioSource music = null;
	public BvaAnim follow = null;


	//[Pulldown("T_POSE","I_POSE","N_POSE")]
	public string poseType="T_POSE";

	public Boolean upperBodyFlag = true;
	public Boolean lowerBodyFlag = true;
	public Boolean hipRotFlag = true;

	private Dictionary<string,string> reName = new Dictionary<string,string>();
	private Dictionary<string,GameObject> bodys = new Dictionary<string,GameObject>();
	private Dictionary<string,SSens> sensors = new Dictionary<string,SSens>();


	private string[] delimiter = {" ","\t"};//区切り文字
	private string[] names ={
		"Head","Body","Hip",
		"R-UpperArm","R-ForeArm","R-Hand",
		"L-UpperArm","L-ForeArm","L-Hand",
		"R-Thigh","R-Shin","R-Foot",
		"L-Thigh","L-Shin","L-Foot"};

	private float ang=0;
	private Quaternion baseForm;
	private Vector3 basePos;

	private int loadFlag=-1;
	// Use this for initialization
	void Start () {
		//Application.targetFrameRate = 30;


		baseForm = this.transform.rotation;
		basePos= this.transform.position;
		if(mocapScale<=0){
			mocapScale = this.transform.localScale[1];
		}
		//print ("baseForm "+baseScale[1]);


		for(int i=0;i<names.Length;i++){
			string n = names[i];
			bodys[n]=getBodyObject(n);
			sensors[n]=new SSens();
		}

		loadSetting();

		if(follow==null){
			loadMoCap();
		}
		else{
			sensors = follow.getSensors();
		}

		loadFlag=1;
	}

	public Dictionary<string,SSens> getSensors(){
		return sensors;
	}
	// Update is called once per frame
	void Update () {
		float time= -1;
		if(music!=null){
			time=music.time;
		}

		if(loadFlag==1){
			ang +=1;
			for(int i=0;i<names.Length;i++){
				string n=names[i];
				if(getBodyFlag(n)){

					if(music==null){
						sensors[n].nextTime();
					}
					else {
						sensors[n].setSec(time);
					}

					GameObject body=bodys[n];
					Quaternion rot = sensors[n].getRot();

					Quaternion tBase = new Quaternion();
					tBase.eulerAngles=new Vector3(0,0,0);
					Quaternion b = new Quaternion();
					b.eulerAngles=new Vector3(0,0,0);

					if(fixFrame>=0){
						tBase = sensors[n].getRot(fixFrame);
					}


					int dir=0;
					if(poseType=="T_POSE"){
						dir=1;
					}
					else if(poseType=="I_POSE"){
						dir=-1;
					}

					if(dir!=0){ // not N_POSE
						if(n=="R-UpperArm" || n=="R-ForeArm" || n=="R-Hand"){
							b.eulerAngles=new Vector3(0,0, 45*dir);
						}
						else if(n=="L-UpperArm" || n=="L-ForeArm" || n=="L-Hand"){
							b.eulerAngles=new Vector3(0,0,-45*dir);
						}
					}
					Quaternion r =baseForm*rot* Quaternion.Inverse(tBase)*b;
					body.transform.rotation=r;
				

					if(n=="Hip" && hipMove){
						Vector3 tPos = sensors[n].getPos(0);
						tPos[1]=0;

						Vector3 pos = sensors[n].getPos();
						body.transform.position=pos-tPos+basePos;
					}

				}

			}

		}
	}

	private Boolean getBodyFlag(string n){
		Boolean result = true;

		if(n=="Hip"){
			result = hipRotFlag;
		}
		else if(n=="R-Thigh" || n=="R-Shin" || n=="R-Foot" 
		        || n=="L-Thigh" || n=="L-Shin" || n=="L-Foot"){

			result=lowerBodyFlag;
		}
		else if(n=="Head" || n=="Body" 
		        || n=="R-UpperArm" || n=="R-ForeArm" || n=="R-Hand" 
		        || n=="L-UpperArm" || n=="L-ForeArm" || n=="L-Hand"){

			result=upperBodyFlag;

		}
		return result;


	}
	private void loadMoCap(){

		int nlineCount = 0;
		System.IO.StreamReader cReader = (
			new System.IO.StreamReader(Application.dataPath+"/MoCap/"+fname, System.Text.Encoding.Default)
		);

		string st;
		int frame=-5;
		string segName="";
		while( (st = cReader.ReadLine())!=null){
			nlineCount++;
			frame++;

			string[] stBuffer = st.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
			if(stBuffer[0]=="Segment:"){
				frame=-5;
				segName=reName[stBuffer[1]];
			}

			if(frame>=0 && segName!=null){
				Vector3 pos = str2Pos(stBuffer[0],stBuffer[1],stBuffer[2]);
				Vector3 rot = str2Rot(stBuffer[3],stBuffer[4],stBuffer[5]);
				sensors[segName].addData(frame,pos,rot);
			}
		}


		cReader.Close();

	}


	Vector3 str2Pos(string px,string py,string pz){
		float w = (float)(0.024*mocapScale);
		Vector3 pos = new Vector3(
			-float.Parse(px)*w, 
			float.Parse(py)*w, 
			float.Parse(pz)*w
			
		);
		return pos;
		
	}
	Vector3 str2Rot(string rx,string ry, string rz){
		Vector3 rot = new Vector3( 
              -float.Parse(rz),  //ry,-
			  -float.Parse(ry), 
			  float.Parse(rx)    //rx
		);
		return rot;
		
	}
	
	
	private void loadSetting(){
		int nlineCount = 0;
		System.IO.StreamReader cReader = (
			new System.IO.StreamReader(Application.dataPath+"/MoCap/"+setting, System.Text.Encoding.Default)
			);
		
		string st;
		while( (st = cReader.ReadLine())!=null){
			nlineCount++;
			string[] stBuffer = st.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
			if(stBuffer[0]!="#"){
				this.reName[stBuffer[0]]=stBuffer[1];
			}
		}
		cReader.Close();
		
	}
	private GameObject getBodyObject(string n){
		string path = this.name+"/-1.!Root/-1.!joint_Char/0.!joint_Center";
		if(n=="R-UpperArm"){
			path+="/1.joint_Torso/29.joint_RightClavicle/30.joint_RightShoulder";
		}
		else if(n=="R-ForeArm"){
			path+="/1.joint_Torso/29.joint_RightClavicle/30.joint_RightShoulder/31.joint_RightElbow";

		}
		else if(n=="R-Hand"){
			path+="/1.joint_Torso/29.joint_RightClavicle/30.joint_RightShoulder/31.joint_RightElbow/32.joint_RightWrist";
		}

		//////

		
		else if(n=="L-UpperArm"){
			path+="/1.joint_Torso/7.joint_LeftClavicle/8.joint_LeftShoulder";
		}
		else if(n=="L-ForeArm"){
			path+="/1.joint_Torso/7.joint_LeftClavicle/8.joint_LeftShoulder/9.joint_LeftElbow";
			
		}
		else if(n=="L-Hand"){
			path+="/1.joint_Torso/7.joint_LeftClavicle/8.joint_LeftShoulder/9.joint_LeftElbow/10.joint_LeftWrist";
		}

		///////////////////

		
		else if(n=="R-Thigh"){
			path+="/6.joint_HipMaster/48.joint_RightHip";
		}
		else if(n=="R-Shin"){
			path+="/6.joint_HipMaster/48.joint_RightHip/49.joint_RightKnee";
		}

		else if(n=="R-Foot"){
			path+="/6.joint_HipMaster/48.joint_RightHip/49.joint_RightKnee/50.joint_RightFoot";
		}

		//////

		else if(n=="L-Thigh"){
			path+="/6.joint_HipMaster/26.joint_LeftHip";
		}
		else if(n=="L-Shin"){
			path+="/6.joint_HipMaster/26.joint_LeftHip/27.joint_LeftKnee";
		}
		
		else if(n=="L-Foot"){
			path+="/6.joint_HipMaster/26.joint_LeftHip/27.joint_LeftKnee/28.joint_LeftFoot";
		}
		///////////////////

		else if(n=="Head"){
			path+="/1.joint_Torso/2.joint_Neck/3.joint_Head";
		}
		else if(n=="Body"){
			path+="/1.joint_Torso";
		}
		else if(n=="Hip"){
			//path+="/6.joint_HipMaster";
			path+="";
		}

		return GameObject.Find(path);
	}



}
