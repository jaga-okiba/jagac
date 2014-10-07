using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public GameObject turntable;
	public static int cnt = 0;
	public float rot = 1;
	//	public static bool move = false; //更新により不要
	public static bool reverse = false;
	public bool reset = false;
	private float angle = 72.0f;

	private GameObject[] character;

	private int charID = 0;
	private int charaNUM;
	private string sceneName = "InterDance";
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
			//print ("ag "+ag);
			float x = r * Mathf.Cos (Mathf.PI / 180.0f * ag);
			float y = 0.0f;
			float z = r * Mathf.Sin (Mathf.PI / 180.0f * ag);
			character[i].transform.position = new Vector3(center.x + x, center.y + y, center.z + z);
			//character[i].transform.Rotate(new Vector3(0, 0, ag));
			//print(i+" -- "+x+","+y+","+z);
		}
	}
	public int getCharaID(){
		return charID;
	}
	
	public void ChangeScene(){
		PlayerPrefs.SetString("CharaName", getCharacterName());
		Application.LoadLevel(sceneName);
	}
	
	
	public string getCharacterName() {
		return character[charID].name;
	}
	
	// 右のキャラに移動
	public void Turn_Right() {
		if (cnt == 0) {
			charID ++;
			charID=charID%charaNUM;
			
			print("ID "+charID);
			reverse = true;
		}
	}
	
	void OnCollisionEnter(Collision col){
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
				//this.transform.Rotate(0, 0, -rot);
				turntable.transform.Rotate(0, -rot,0);
				
			} else {
				
				/* 回転方式の変更により一旦コメントアウト
				// 誤差があるかどうか
				if(this.transform.rotation.y % angle == 0){
					// 誤差の修正(完全に修正は出来ない？)
					if(this.transform.rotation.y < angle){
						float temp = (int)(this.transform.rotation.y - (this.transform.rotation.y % angle));
						this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, temp, this.transform.rotation.z);
					} else {
						float temp = (int)(this.transform.rotation.y + (this.transform.rotation.y % angle));
						this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, temp, this.transform.rotation.z);
					}
				}
				*/
				
				// 回転し終えたら操作可能にする
				reverse = false;
				reset = true;
			}
		}
	}
}
