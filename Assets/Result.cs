using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

	public GUIStyle infoStyle = new GUIStyle();
	public GUIStyle itemStyle = new GUIStyle();
	public GUIStyle buttonStyle = new GUIStyle();

	int iHoriRectPosi = 100;
	int iVartRectPosi = 180;
	//int cent = 30;
	int iDif = 50;

	int infowidth = 200;
	int infoheight = 45;

	int itemwidth = 130;

	void OnGUI(){
		int horiPosition = Screen.width/2-iHoriRectPosi;
		int vartPosition = Screen.height/2-iVartRectPosi;
		GUI.Label(new Rect(horiPosition, vartPosition, infowidth, infoheight), globalVal.g_correctNum.ToString(), infoStyle);
		GUI.Label(new Rect(horiPosition, vartPosition+iDif, infowidth, infoheight), globalVal.g_wrongNum.ToString(), infoStyle);
		GUI.Label(new Rect(horiPosition, vartPosition+iDif*2, infowidth, infoheight), globalVal.parcent.ToString(), infoStyle);
		GUI.Label(new Rect(horiPosition, vartPosition+iDif*3, infowidth, infoheight), globalVal.score.ToString(), infoStyle);
		GUI.Label(new Rect(horiPosition, vartPosition+iDif*4, infowidth, infoheight), globalVal.finishTime.ToString(), infoStyle);

		GUI.Label(new Rect(horiPosition-itemwidth, vartPosition, infowidth, infoheight), "正解数", itemStyle);
		GUI.Label(new Rect(horiPosition-itemwidth, vartPosition+iDif, infowidth, infoheight), "失敗数", itemStyle);
		GUI.Label(new Rect(horiPosition-itemwidth, vartPosition+iDif*2, infowidth, infoheight), "正解率", itemStyle);
		GUI.Label(new Rect(horiPosition-itemwidth, vartPosition+iDif*3, infowidth, infoheight), "得点", itemStyle);
		GUI.Label(new Rect(horiPosition-itemwidth, vartPosition+iDif*4, infowidth, infoheight), "時間", itemStyle);

		if(GUI.Button(new Rect(horiPosition, vartPosition+iDif*5, infowidth, infoheight), "もういっかい！", buttonStyle)){
			Application.LoadLevel("typingScreen");
		}

		if(GUI.Button(new Rect(horiPosition, vartPosition+iDif*6, infowidth, infoheight), "選択画面へ", buttonStyle)){
			Application.LoadLevel("selection");
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
