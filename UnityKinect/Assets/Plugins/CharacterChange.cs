using UnityEngine;
using System.Collections;

public class CharacterChange : MonoBehaviour {
	public static int cnt = 0;
	public float rot = 1;
	public static bool reverse = false;
	public bool reset = false;
	private float angle = 72.0f;
	public GameObject[] character;
	private int charID = 0;
	private int charaNUM;
	// Use this for initialization
	void Start () {
		charaNUM = character.Length;
		angle = 360.0f / (float)character.Length;
		float r = 100f;
		Vector3 center = this.transform.position;
		for (int i = 0; i < character.Length; i++) {
			float ag = -angle * i;
			print (ag);
			float x = r * Mathf.Cos (Mathf.PI / 180.0f * ag);
			float y = r * Mathf.Sin (Mathf.PI / 180.0f * ag);
			character[i].transform.position = new Vector3(center.x + x, center.y + y, center.z);
			character[i].transform.Rotate(new Vector3(0, 0, ag));
		}
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
			charID ++;
			charID=charID%charaNUM;

			print("ID "+charID);
			reverse = true;
		}
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
				this.transform.Rotate(0, 0, rot);
			} else {
				// 回転し終えたら操作可能にする
				reverse = false;
				reset = true;
			}
		}
	}
}
