using UnityEngine;
using System.Collections;
using System.IO;


public class MyReplay : MonoBehaviour {
	private MyKinectData[] datas = new MyKinectData[15];
	private int fileNum = 0;
	
	public GameObject[] posList;
	
	private int modelNum = 11;
	
	public int fps = 30;
	public float mstart=4.5f;
	public AudioSource music = null;
	private int flag=-1;
	public FieldSet bg;
	public bool RECORD = false;
	
	private float maxTime = 30;
	
	private  GameObject[] modelList ;
	public float yAngle=180f;
	private int replaceCount = 0;
	public int creditTiming = 20;
	
	// Use this for initialization
	void Start () {
		replaceCount = 0;
		//get model list
		GameObject list = GameObject.Find ("modelList");
		int chCnt = list.transform.childCount;
		modelList = new GameObject[chCnt];
		for (int i=0; i<chCnt; i++) {
			modelList[i]=list.transform.GetChild(i).gameObject;
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
			datas[i].load(fn,modelList);
		}
		
		for (int i=0; i<fileNum; i++) {
			if(RECORD){
				datas[i].getBody().transform.parent=posList[i+1].transform;
			}
			else{
				datas[i].getBody().transform.parent=posList[i].transform;
			}
			datas [i].getBody().transform.localPosition = new Vector3 (0, 0, 0);
			datas [i].getBody().transform.localRotation =Quaternion.Euler(new Vector3 (0, 0, 0));
			
		}
		
		if(!RECORD){
			datas[0].setYAngle(yAngle);
		}
		
		flag = 1;
		print ("END LOAD");
		music.Play ();
		if (maxTime == -1 || maxTime>music.clip.length) {
			maxTime = music.clip.length;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if (flag == 1) {
			
			
			float time = -1;
			if (music != null) {
				time = music.time;
			}
			
			for (int i=0; i<fileNum; i++) {
				datas [i].run (time);
			}
			
			
			if(time>=maxTime){
				music.time=0;
				bg.setPlace();
				replaceCount++;
				if(replaceCount>=creditTiming){
					Application.LoadLevel("StaffRoll");
				}
			}
			
			if (Input.GetButtonDown("Jump")){
				Application.LoadLevel("CharaSelect");
			}
			
		}
		
	}
	public float getMaxTime(){
		return this.maxTime;
	}
}
