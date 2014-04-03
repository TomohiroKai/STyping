using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class selection : MonoBehaviour {

	public GUIStyle qStyle = new GUIStyle();
	public GUIStyle iStyle = new GUIStyle();

	int qwidth = 300;
	int qheight = 40;

	int qHoriRectPosi = 150;
	int qVartRectPosi = 100;

	//int cent = 30;
	int qDif = 45;

	int iwidth = 150;
	int iheight = 50;
	int iHoriRectPosi = 75;

	int place = 0;
	int qnum = 0;
	float correctNum = 0;
	float wrongNum = 0;

	List<string> labelList = new List<string>();

	void OnGUI(){
		bool inputCheck = false;
		int horiPosi = Screen.width/2-qHoriRectPosi;
		int vartPosi = Screen.height/2+qVartRectPosi;

		if(GUI.Button(new Rect(horiPosi, vartPosi, qwidth, qheight), labelList[0], qStyle)){
			globalVal.selectQ = labelList[0];
			labelList[0] = "作成中";
			//Application.LoadLevel("typingScreen");
		}
		if(GUI.Button(new Rect(horiPosi, vartPosi-qDif, qwidth, qheight), labelList[1], qStyle)){
			globalVal.selectQ = labelList[1];
			labelList[1] = "作成中";
			//Application.LoadLevel("typingScreen");
		}
		if(GUI.Button(new Rect(horiPosi, vartPosi-qDif*2, qwidth, qheight), labelList[2], qStyle)){
			globalVal.selectQ = labelList[2];
			labelList[2] = "作成中";
			//Application.LoadLevel("typingScreen");
		}
		if(GUI.Button(new Rect(horiPosi, vartPosi-qDif*3, qwidth, qheight), labelList[3], qStyle)){
			globalVal.selectQ = labelList[3];
			labelList[3] = "作成中";
			//Application.LoadLevel("typingScreen");
		}
		if(GUI.Button(new Rect(horiPosi, vartPosi-qDif*4, qwidth, qheight), labelList[4], qStyle)){
			globalVal.selectQ = labelList[4];
			labelList[4] = "作成中";
			//Application.LoadLevel("typingScreen");
		}
		if(GUI.Button(new Rect(horiPosi, vartPosi-qDif*5, qwidth, qheight), labelList[5], qStyle)){
			globalVal.selectQ = labelList[5];
			Application.LoadLevel("typingScreen");
		}
	}
		/*
		GUI.Label(new Rect(Screen.width/2-iHoriRectPosi, vartPosi+qDif*1.5f, iwidth, iheight), numLabel, iStyle);
		GUI.Label(new Rect(Screen.width/2-iHoriRectPosi, vartPosi+qDif*3, iwidth, iheight), debug, iStyle);
*/
	// Use this for initialization
	void Start () {
		labelList.Add("Sublime Text");
		labelList.Add("Vim");
		labelList.Add("Windows");
		labelList.Add("Browser");
		labelList.Add("PowerPoint");
		labelList.Add("Excel");
	}

	// Update is called once per frame
	void Update () {

	}
}
