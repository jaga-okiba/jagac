using UnityEngine;
using System.Collections;
//using System.Diagnostics;
using System.IO;


public class MyReplay : MonoBehaviour {
	private MyKinectData[] datas = new MyKinectData[15];
	private int fileNum = 0;

	//hogehoge
	public GameObject[] posList;

	private int modelNum = 11;

	public int fps = 30;
	public float mstart=4.5f;
	public AudioSource music = null;
	//private int frame=0;
	//private int maxFrame = -1;
	private int flag=-1;
	public FieldSet bg;
	public bool RECORD = false;

	private float maxTime = 30;

	private  GameObject[] modelList ;

	// Use this for initialization
	void Start () {
		//get model list
		GameObject list = GameObject.Find ("modelList");
		int chCnt = list.transform.childCount;
		modelList = new GameObject[chCnt];
		for (int i=0; i<chCnt; i++) {
			modelList[i]=list.transform.GetChild(i).gameObject;
			//print(modelList[i].name);
		}


		for (int i=0; i<datas.Length; i++) {
			datas[i]=new MyKinectData();
		}

		//get file list
		string[] files = Directory.GetFiles("myRecord");
		fileNum = files.Length;


		modelNum = posList.Length;
		if (RECORD) {
			modelNum--;
		}


		if (fileNum > modelNum) {
			fileNum=modelNum;
		}


		for(int i=0;i<fileNum;i++){
			string fn = files[files.Length-1-i];
			//print ("file "+fn);
			datas[i].load(fn,modelList);
			//if(datas[i].getMaxFrame() < maxFrame || maxFrame==-1){
			//	maxFrame=datas[i].getMaxFrame();
			//}
		}

		for (int i=0; i<fileNum; i++) {
			if(RECORD){
				datas[i].getBody().transform.parent=posList[i+1].transform;
			}
			else{
				datas[i].getBody().transform.parent=posList[i].transform;
			}
			datas [i].getBody().transform.localPosition = new Vector3 (0, 0, 0);

		}
		flag = 1;
		print ("END LOAD");
		music.Play ();
		if (maxTime == -1 || maxTime>music.clip.length) {
			maxTime = music.clip.length;
		}
	}
	/*
	public int getFrameFromSec(float time){
		float t = time;
		if(t<0){ t=0; }
		int frame = ((int)(fps*t));
		return frame;
	}
	*/

	// Update is called once per frame
	void Update () {
		if (flag == 1) {
			//print ("hoge");

			float time = -1;
			if (music != null) {
				time = music.time;
			}
			//frame = getFrameFromSec (time);
			//print ("frame "+time);
			for (int i=0; i<fileNum; i++) {
				datas [i].run (time);
			}


			if(time>maxTime){
			//if(time>music.clip.length){
				music.time=0;
				bg.setPlace();
			}


			//if (Input.GetKey(KeyCode.Space)){
			if (Input.GetButtonDown("Jump")){
				Application.LoadLevel("CharaSelect");
			}

		}

	}
	public float getMaxTime(){
		return this.maxTime;
	}
}
