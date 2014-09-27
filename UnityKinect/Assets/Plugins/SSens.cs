using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class SSens {
	public int fps = 30;
	public float mstart=4.5f;
	//public float mstart=14.5f;
	private int max=-1;
	private int iterator = -1;//現フレーム数
	private List<float> timeStamp = new List<float>();
	private List<Vector3> pos = new List<Vector3>();
	private List<Quaternion> rot = new List<Quaternion>();
	
	public void addData(float iframe,Vector3 ipos, Vector3 irot){
		timeStamp.Add((float)iframe);
		pos.Add(ipos);
		rot.Add(quatRot(irot,"ZYX"));

		//maxは配列の中で最も小さいものとする．
		max=timeStamp.Count;
		if(max>pos.Count){max=pos.Count;}
		if(max>rot.Count){max=rot.Count;}
		iterator=max-1;
	}
	
	public int setSec(float time){
		float t = time-mstart;

		if(t<0){ t=0; }
		return setTime ((int)(fps*t));
	}
	//iteratorをセット
	public int setTime(int time){
		iterator=time;
		//マイナスだったら，後ろに回る
		if(iterator<0){
			iterator=max+iterator;
		}
		//最大値以上の場合は，0に戻す
		if(iterator>=max){
			iterator=iterator-max;
		}
		return iterator;
	}

	public int nextTime(int st){
		return setTime(iterator+st);
	}
	public int nextTime(){
		return nextTime(1);
	}
	
	public int prevTime(int st){
		return setTime(iterator-st);
	}
	public int prevTime(){
		return prevTime(1);
	}
	
	
	//現在のposを返す
	public Vector3 getPos(int t){
		return pos[t];
	}
	public Vector3 getPos(){
		return pos[iterator];
	}
	public List<Vector3> getAllPos(){
		return pos;	
	}

	public Quaternion getRot(int t){
		return rot[t];
	}


	//現在のrotを返す
	public Quaternion getRot(){
		return rot[iterator];
	}
	public List<Quaternion> getAllRot(){
		return rot;	
	}
	
	
	//タイムスタンプを返す
	public float getTimeStamp(){
		return timeStamp[iterator];
	}
	
	public int getMax(){
		return max;	
	}
	
	//from tanamura
	//quaternionによる回転
	//回転順をtypeで指定
	public static Quaternion quatRot(Vector3 rot, string type){
		if(type == "XYZ"){
			return
				
				Quaternion.AngleAxis(rot.x, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 0, 1))
			;
		}
		else if (type == "XZY"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 0, 1)) * 
				Quaternion.AngleAxis(rot.z, new Vector3(0, 1, 0))
			;
		}
		else if (type == "YXZ"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 0, 1))
			;
		}
		else if (type == "YZX"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.z, new Vector3(1, 0, 0)) 
			;
		}
		else if (type == "ZXY"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.y, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 1, 0))
			;
		}
		else if (type == "ZYX"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(1, 0, 0))

			;
		}
	



		else if (type == "XYX"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(1, 0, 0))
			;
		}
		else if (type == "XZX"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(1, 0, 1)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.z, new Vector3(1, 0, 0))
			;
		}
		else if (type == "YXY"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 1, 0))
			;
	}
		else if (type == "YZY"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 1, 0))
			;
		}
		else if (type == "ZXZ"){
			return				
				Quaternion.AngleAxis(rot.x, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.y, new Vector3(1, 0, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 0, 1))
			;
		}
		else if (type == "ZYZ"){
			return
				Quaternion.AngleAxis(rot.x, new Vector3(0, 0, 1)) *
				Quaternion.AngleAxis(rot.y, new Vector3(0, 1, 0)) *
				Quaternion.AngleAxis(rot.z, new Vector3(0, 0, 1))
			;
		}

		return new Quaternion(0,0,0,0);
	}
}




