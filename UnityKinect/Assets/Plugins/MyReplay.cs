using UnityEngine;
using System.Collections;
//using System.Diagnostics;
using System.IO;


public class MyReplay : MonoBehaviour {
	private MyKinectData[] datas = new MyKinectData[15];
	private int fileNum = 0;
	public  GameObject[] modelList ;
	public GameObject[] posList;

	public int fps = 30;
	public float mstart=4.5f;
	public AudioSource music = null;
	private int frame=0;

	private int flag=-1;
	public FieldSet bg;

	// Use this for initialization
	void Start () {
		//createList ();


		for (int i=0; i<datas.Length; i++) {
			datas[i]=new MyKinectData();
		}

		//get file list
		string[] files = Directory.GetFiles("myRecord");
		fileNum = files.Length;
		if (fileNum >= 11) {
			fileNum=11;
		}

		for(int i=0;i<fileNum;i++){
			datas[i].load(files[i],modelList);
		}

		for (int i=0; i<fileNum; i++) {
			datas[i].getBody().transform.parent=posList[i].transform;
			datas [i].getBody().transform.localPosition = new Vector3 (0, 0, 0);

		}
		flag = 1;
		print ("END LOAD");
	}

	public int getFrameFromSec(float time){
		float t = time;
		if(t<0){ t=0; }
		int frame = ((int)(fps*t));
		return frame;
	}
	// Update is called once per frame
	void Update () {
		if (flag == 1) {
			//print ("hoge");

			float time = -1;
			if (music != null) {
					time = music.time;
			}
			int frame = getFrameFromSec (time);
			//print ("frame "+time);
			for (int i=0; i<fileNum; i++) {
					datas [i].run (frame);
			}


			if(time>20){
			//if(time>music.clip.length){
				music.time=0;
				bg.setPlace();
			}


			if (Input.GetKey(KeyCode.Space)){
				Application.LoadLevel("CharaSelect");
			}

		}

	}
}
