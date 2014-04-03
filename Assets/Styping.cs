using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Windows.Browser.ScriptObject;

public class Styping : MonoBehaviour {

	public GUIStyle qStyle = new GUIStyle();
	public GUIStyle iStyle = new GUIStyle();

	int qwidth = 600;
	int qheight = 40;

	int qHoriRectPosi = 300;
	int qVartRectPosi = 40;

	int qDif = 45;

	int iwidth = 150;
	int iheight = 50;
	int iHoriRectPosi = 75;

	Event e;
	Question[] qall;
	int place = 0;
	int qnum = 1;
	float correctNum = 0;
	float wrongNum = 0;
	int g_i = 0;
	int keyLength = 0;
	float timer = 0;
	bool loadQ1 = false;
	bool loadQ2 = false;
	string cmdLabel, keyLabel, inputLabel, numLabel;
	string debug;
	int upcheck = 0;
	void OnGUI(){
		bool inputCheck = false;
		int horiPosi = Screen.width/2-qHoriRectPosi;
		int vartPosi = Screen.height/2-qVartRectPosi;

		GUI.Label(new Rect(horiPosi, vartPosi, qwidth, qheight), inputLabel, qStyle);
		GUI.Label(new Rect(horiPosi, vartPosi-qDif, qwidth, qheight), keyLabel, qStyle);
		GUI.Label(new Rect(horiPosi, vartPosi-qDif*2, qwidth, qheight), cmdLabel, qStyle);
		GUI.Label(new Rect(Screen.width/2-iHoriRectPosi, vartPosi+qDif*1.5f, iwidth, iheight), numLabel, iStyle);

		e = Event.current;
		//押されたキーコードチェック
		if(e.isKey && e.keyCode != KeyCode.None){
			if(e.type == EventType.KeyDown){
				if(checkCorrect()){
					upcheck = 1;
					correctProcess();
				}//連続Downの間違い検知防止
				else if(checkWrong() && upcheck != 1){
					upcheck = 1;
					wrongProcess();
				}
			}//UPのみ検知されるもの用
			else if(e.type == EventType.KeyUp && upcheck != 1){
				if(checkCorrect()){
					correctProcess();
				}
				else if(checkWrong()){
					wrongProcess();
				}
			}
			else{
				upcheck = 0;
			}
			e.type = EventType.Ignore;
		}
}

bool checkCorrect(){
	if(qall[g_i].ctrl==e.control
		&& qall[g_i].alt==e.alt
		&& qall[g_i].shift==e.shift
		&& qall[g_i].kcList[place] == e.keyCode
		){
		return true;
	}
	else{
		return false;
	}

}
bool checkWrong(){
	if(
		!(e.keyCode == KeyCode.RightControl)
		&& !(e.keyCode == KeyCode.LeftControl)
		&& !(e.keyCode == KeyCode.RightAlt)
		&& !(e.keyCode == KeyCode.LeftAlt)
		&& !(e.keyCode == KeyCode.RightShift)
		&& !(e.keyCode == KeyCode.LeftShift)
		){
		return true;
	}
	else{
		return false;
	}
}

void correctProcess(){
	keyLength--;
	if(keyLength > 0){
		place++;
	}
	else{
		qnum++;
		correctNum++;
		inputLabel = qall[g_i].keyName;
		loadQ1 = true;
		loadQ2 = false;
	}
}
void wrongProcess(){
	wrongNum++;
}

void result(){
	int resultMin = Mathf.RoundToInt(timer/60);
	int resultSec = Mathf.RoundToInt(timer%60);
	globalVal.g_correctNum = correctNum;
	globalVal.g_wrongNum = wrongNum;
	globalVal.finishTime = resultMin+":"+resultSec;
	globalVal.parcent = Mathf.RoundToInt(correctNum/(correctNum+wrongNum)*100);
	globalVal.score =Mathf.RoundToInt((correctNum-(wrongNum/2))*10-(resultMin+resultSec));
}

void setQuestion(){

	if(qnum > 10){
		result();
		Application.LoadLevel("result");
		System.Threading.Thread.Sleep(700);
	}
	else{
		System.Threading.Thread.Sleep(700);
		reset();
		g_i = Random.Range(0, 89);
		cmdLabel = qall[g_i].cmdName;
		keyLabel = qall[g_i].keyName;
		keyLength = qall[g_i].kcList.Count;
		numLabel = qnum.ToString();
		Debug.Log("qnum.ToString()"+numLabel);
	}
}

	// Use this for initialization
	void Start () {
		qall = new Question[89];
		//問題読み込み
		if(globalVal.selectQ == "Excel"){
			for(int i=0; i < ExcelQ.cmdName.Count; i++){
				qall[i] = new Question(ExcelQ.cmdName[i],ExcelQ.keyName[i],ExcelQ.dist[i],ExcelQ.ctrl[i],ExcelQ.alt[i],ExcelQ.shift[i],ExcelQ.keycode[i]);
			}

		}
		else if(globalVal.selectQ == "PowerPoint"){}
		else if(globalVal.selectQ == "Windows"){}

		setQuestion();
}
	// Update is called once per frame
	void Update () {
		if(loadQ1 && loadQ2){
			setQuestion();
			loadQ1 = false;
		}
		loadQ2 = true;
		timer += Time.deltaTime;
    }

        void reset(){
        	for(int i=0; i < 4; i++){
        		cmdLabel = "";
        		keyLabel = "";
        		inputLabel = "";
        	}
        }

       public class Question {
        	public string cmdName;
        	public string keyName;
        	public int dist=0;
        	public bool ctrl = false;
        	public bool alt = false;
        	public bool shift = false;
        	//public string[] kc = new string[4];
        	public List<KeyCode> kcList = new List<KeyCode>();

        public	Question(string cn, string kn, int di, bool ctrl, bool alt, bool shift, KeyCode kc1){
        		setInfo(cn, kn, di, ctrl, alt, shift);
        		this.kcList.Add(kc1);
        	}
		public	Question(string cn, string kn, int di, bool ctrl, bool alt, bool shift, KeyCode kc1, KeyCode kc2){
        		setInfo(cn, kn, di, ctrl, alt, shift);
        		this.kcList.Add(kc1);
        		this.kcList.Add(kc2);
        	}
		public	Question(string cn, string kn, int di, bool ctrl, bool alt, bool shift, KeyCode kc1, KeyCode kc2, KeyCode kc3){
        		setInfo(cn, kn, di, ctrl, alt, shift);
        		this.kcList.Add(kc1);
        		this.kcList.Add(kc2);
        		this.kcList.Add(kc3);
        	}
        public	Question(string cn, string kn, int di, bool ctrl, bool alt, bool shift, KeyCode kc1, KeyCode kc2, KeyCode kc3, KeyCode kc4){
        		setInfo(cn, kn, di, ctrl, alt, shift);
        		this.kcList.Add(kc1);
        		this.kcList.Add(kc2);
        		this.kcList.Add(kc3);
        		this.kcList.Add(kc4);
        	}

        void setInfo(string cn, string kn, int di, bool ctrl, bool alt, bool shift){
        	this.cmdName = cn;
        	this.keyName = kn;
        	this.dist = di;
        	this.ctrl = ctrl;
        	this.alt = alt;
        	this.shift = shift;
        }
    }
}
