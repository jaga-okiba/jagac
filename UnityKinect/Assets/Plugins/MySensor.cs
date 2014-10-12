using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MySensor : MonoBehaviour {
	private List<Vector3> pos = new List<Vector3>();
	private List<Vector3> rot = new List<Vector3>();
	private List<float> time = new List<float>();


	private int maxFrame=0;
	private string partName;

	public bool checkName(string name){
		bool result = false;
		if(partName==name){
			result = true;
		}
		return result;
	}
	public MySensor(string n){
		partName = n;
	}

	public void addData(float t,Vector3 p,Vector3 r){
		time.Add (t);
		pos.Add (p);
		rot.Add (r);
		maxFrame++;
	}
	public Vector3 getPos(float f){
		int frame = time2frame (f);
		Vector3 result = new Vector3 (0f, 0f, 0f);
		try{
			result =pos[frame];
		}catch(Exception e){}

		return result;
	}
	public Vector3 getRot(float f){
		int frame = time2frame (f);
		Vector3 result = new Vector3 (0f, 0f, 0f);
		try{
			result =rot[frame];
		}catch(Exception e){}

		return result;
	}
	public int time2frame(float t){
		int result = 0;
		if (t < time [0]) {
			result = 0;
		} else if (t > time [time.Count - 1]) {
			result = pos.Count - 1;
		} else {
			for(int i=1;i<time.Count;i++){
				if( time[i-1]<t && t<time[i]){
					result = i;
					break;
				}
			}

		}
		return result;
	}
	public int getMaxFrame(){
		return maxFrame;
	}
}
