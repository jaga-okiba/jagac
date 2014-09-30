using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MySensor : MonoBehaviour {
	private List<Vector3> pos = new List<Vector3>();
	private List<Vector3> rot = new List<Vector3>();
	private int maxTime=0;
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

	/*
	private GameObject target;
	private int time;

	public bool checkName(string name){
		bool result = false;
		if(target.name==name){
			result = true;
		}
		return result;
	}
	
	public int next(){
		time = (time + 1) % maxTime;
		return time;
	}
	public void setPos(){
		if (maxTime > 0) {
			target.transform.position = pos [time];
			target.transform.eulerAngles = rot [time];
		}
	}
	*/

	public void addData(Vector3 p,Vector3 r){
		pos.Add (p);
		rot.Add (r);
		maxTime++;
	}
	public Vector3 getPos(int f){
		int frame = f;
		//print ("hoge " + pos.Count + "  " + f);


		if (pos.Count < f) {
			frame = pos.Count - 2;
		}
		if (frame < 0) {
			frame = 0;
		}
		//print ("frame " + frame+"/"+pos.Count);
		return pos[frame];
	}
	public Vector3 getRot(int f){
			int frame = f;
			if (pos.Count < f) {
				frame = pos.Count - 2;
			}
			if (frame < 0) {
				frame = 0;
			}
			return rot[frame];
	}
	public int getMaxTime(){
		return maxTime;
	}
}
