using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public GameObject turntable;
	public static int cnt = 0;
	public float rot = 1;
	public static bool reverse = false;
	public bool reset = false;
	private float angle = 72.0f;
	
	private GameObject[] character;
	
	private int charID = 0;
	private int charaNUM;
	
	public AudioClip changeSound;
	private AudioSource changeSoundSource;

	// Use this for initialization
	void Start () {

		//get model list
		GameObject modelList = GameObject.Find ("modelList");
		int chCnt = modelList.transform.childCount;
		character = new GameObject[chCnt];
		for (int i=0; i<chCnt; i++) {
			character[i]=modelList.transform.GetChild(i).gameObject;
			character[i].SetActive(true);
			print(character[i].name);
		}
		
		
		charaNUM = character.Length;
		angle = 360.0f / (float)character.Length;
		float r = 100f;
		Vector3 center = turntable.transform.position;
		for (int i = 0; i < character.Length; i++) {
			float ag = -angle * (float)i;
			float x = r * Mathf.Cos (Mathf.PI / 180.0f * ag);
			float y = 0.0f;
			float z = r * Mathf.Sin (Mathf.PI / 180.0f * ag);
			character[i].transform.position = new Vector3(center.x + x, center.y + y, center.z + z);

		}
		
		
		changeSoundSource = gameObject.GetComponent<AudioSource> ();

		int rand = (int)Random.Range (0, 1000*charaNUM);
		//print ("rand: " + rand);
		rand = rand % charaNUM;
		//print ("rand%charaNUM: " + rand);
		Turn_Table (rand);
	}
	public int getCharaID(){
		return charID;
	}
	
	public void ChangeScene(){
		PlayerPrefs.SetString("CharaName", getCharacterName());
		Application.LoadLevel("InterDance");
	}
	
	
	public string getCharacterName() {
		return character[charID].name;
	}
	
	// 右のキャラに移動
	public void Turn_Right() {
		if (cnt == 0) {
			Turn_Id();
			reverse = true;
		}
	}

	public void Turn_Id() {
		charID ++;
		charID=charID%charaNUM;
		character[charID].transform.rotation =Quaternion.Euler( new Vector3(0,225,0));

		//print("ID "+charID);
	}
	public void Turn_Table(int num){
		for (int tc=0; tc<num; tc++) {
			Turn_Id ();
			for (int i=0; i<(int)angle; i++) {
				turntable.transform.Rotate (0, -rot, 0);
			}
		}
	}


	void OnCollisionEnter(Collision col){
		changeSoundSource.PlayOneShot (changeSound);
		Turn_Right();
	}
	
	// Update is called once per frame
	void Update () {
		if (reset == true){
			cnt = 0;
			reset = false;
		}
		
		// 反時計回りに回る(操作受付中止)
		if(reverse == true){
			// カウントが90になるまで回転する(90℃回転)
			if(cnt < (int)angle){
				cnt++;
				turntable.transform.Rotate(0, -rot,0);
				
			} else {
				

				// 回転し終えたら操作可能にする
				reverse = false;
				reset = true;
			}
		}
	}
}
