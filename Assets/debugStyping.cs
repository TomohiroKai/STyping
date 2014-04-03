using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Windows.Browser.ScriptObject;

public class debugStyping : MonoBehaviour {

	public GUIStyle qStyle = new GUIStyle();
	public GUIStyle iStyle = new GUIStyle();

	int qwidth = 600;
	int qheight = 40;

	int qHoriRectPosi = 300;
	int qVartRectPosi = 40;

	//int cent = 30;
	int qDif = 45;

	int iwidth = 150;
	int iheight = 50;
	int iHoriRectPosi = 75;

	Event e;
	Question[] qall;
	int place = 0;
	int qnum = 0;
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
		GUI.Label(new Rect(Screen.width/2-iHoriRectPosi, vartPosi+qDif*3, iwidth, iheight), debug, iStyle);

//Debug.Log("e.keyCode1: "+e.keyCode);
			//Debug.Log("ctrl,alt,shift"+qall[g_i].keyName);
			//if((e.keyCode != KeyCode.None) && (e.type != EventType.KeyUp)){
		e = Event.current;

		if(e.isKey && e.keyCode != KeyCode.None){
			Debug.Log("ctrl,shift,keycode:"+ e.control + e.shift + e.keyCode);
			debug = "Event.current:"+e;
			Debug.Log("Event.current:"+e);
			print("upcheck:"+upcheck);
			if(e.type == EventType.KeyDown){
				Debug.Log("EventType.KeyDown, keycode:"+ e.keyCode);
				if(checkCorrect(e.control, e.alt, e.shift, e.keyCode)){
					upcheck = 1;
					correctProcess();
				}else if(checkWrong(e.keyCode)){
					print("wrongProcess");
					wrongProcess();
				}
			}else if(e.type == EventType.KeyUp && upcheck != 1){
				Debug.Log("EventType.KeyUp");
				if(checkCorrect(e.control, e.alt, e.shift, e.keyCode)){
					correctProcess();
				}else if(checkWrong(e.keyCode)){
					print("wrongProcess");
					wrongProcess();
				}
			}else{
				Debug.Log("else");
					upcheck = 0;
				}
			}

		//Debug.Log("e.keyCode2: "+e.keyCode);
		//HtmlEventArgs.StopPropagation();
		//HtmlEventArgs.PreventDefault();

		// イベントの上位伝播を防止
		//e.which = 0;
		//event.keyCode = 0;
		/*
		if(e.preventDefault()){
			//console.log("e.preventDefault():"+e.preventDefault());
			e.preventDefault();
		}
		if(e.stopPropagation()){
			//console.log("e.stopPropagation():"+e.stopPropagation());
			e.stopPropagation();
		}*/

		//[2]=0の場合はAlt or Ctrl一回以外すべて対応（）
		/*
		if(qall[g_i].dist == 0){
		//console.log("g_question[2]==0");
		//Ctrl、Alt、Shiftはすべて無視
		if(!(15 < keycode && keycode < 19)){
			if(qall[g_i].ctrl == ctrl && qall[g_i].alt == alt &&
				qall[g_i].shift == shift && qall[g_i].kc[place] == keycode){
				correctProcess();
				//console.log("g_question[2]==0_if_if");
			}
			else {
				//console.log("g_question[2]==0_if_else");
				wrongProcess();
			}
		}
	}
	//altはキーを離す操作により値が変わるため検知しない（初めの一回はカウントしたいため押し続けは不正解とする）
	else{
		//console.log("g_question[2]_else,place:keycode:"+place+":"+keycode);
		if(qall[g_i].kc[place] == keycode) correctProcess();
		else wrongProcess();
	}
	*/
}
bool checkCorrect(bool ctrl, bool alt, bool shift, KeyCode ekc){
	Debug.Log("checkCorrect");
	if(qall[g_i].ctrl==ctrl
		&& qall[g_i].alt==alt
		&& qall[g_i].shift==shift
		&& ekc == qall[g_i].kcList[place]
		){
		Debug.Log("checkCorrectTrue");
		return true;
	}else{
		Debug.Log("checkCorrectFalse");
		return false;
	}

}
bool checkWrong(KeyCode cas){
	if(
		!(cas == KeyCode.RightControl)
		&& !(cas == KeyCode.LeftControl)
		&& !(cas == KeyCode.RightAlt)
		&& !(cas == KeyCode.LeftAlt)
		&& !(cas == KeyCode.RightShift)
		&& !(cas == KeyCode.LeftShift)
		){
		return true;
	}else{
		return false;
	}
}

void correctProcess(){
	//print("cPplace:"+place);
	keyLength--;
	//print("cPkeyLength:"+keyLength);
	//print("cPqnum:"+qnum);
	if(keyLength > 0){
		place++;
	}else{
		//Debug.Log("correctProcessNext");
		qnum++;
		correctNum++;
		inputLabel = qall[g_i].keyName;
		//print(inputLabel);
		loadQ1 = true;
		loadQ2 = false;
		//setQuestion();
	}
}
void wrongProcess(){
	//console.log("wrongProcess");
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

	if(qnum == 3){
		result();
		Application.LoadLevel("debugResult");
		System.Threading.Thread.Sleep(700);
	}else{
	System.Threading.Thread.Sleep(700);
	reset();
	g_i = Random.Range(0, 2);
	cmdLabel = qall[g_i].cmdName;
	keyLabel = qall[g_i].keyName;
	keyLength = qall[g_i].kcList.Count;
}
}

	// Use this for initialization
	void Start () {

		qall = new Question[89];
		//qall[0] = new Question("現在の時刻を入力","Ctrl-:",0,true,false,false,"colon");
/*qall[0] = new Question("［斜体］の設定・解除（テンキー不可）","Ctrl-3",0,true,false,false,KeyCode.Alpha3);
qall[1] = new Question("非表示の列を再表示（テンキー不可）","Ctrl-Shift-0",0,true,false,true,KeyCode.Alpha0);
qall[2] = new Question("SUM関数の挿入","Shift-Alt-=",0,false,true,true,KeyCode.Minus);
*/
qall[0] = new Question("データが入力されている範囲を選択（テンキーの［*］なら［Shift］不要）","Ctrl-Shift-*",0,true,false,true,KeyCode.Semicolon);
qall[1] = new Question("データの途切れるセルまで選択（上）","Ctrl-Shift-↑",0,true,false,true,KeyCode.UpArrow);
/*
qall[0] = new Question("データの途切れるセルにジャンプ(上)","Ctrl-↑",0,true,false,false,KeyCode.UpArrow);
qall[1] = new Question("データの途切れるセルまで選択（上）","Ctrl-Shift-↑",0,true,false,true,KeyCode.UpArrow);
qall[2] = new Question("複数セルに同じデータの入力","Ctrl-Enter",0,true,false,false,KeyCode.Return);
*/

		setQuestion();
	//	startset();
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
/*
        	/*
        	void getQuestion(){
        		cmdName = "上書き保存";
        		keyName = "Ctrl-s";
        		dist = 0;
        		ctrl = true;
        		alt = false;
        		shift = false;
        		kc1 = 83;
        		kc2 = null;
        		kc3 = null;
        		kc4 = null;
        	}*/

/*
void startset(){
	int qnum = 0;
	int place = 6;
	int sec=0;
	int correctNum=0;
	int wrongNum=0;
	int endflag=0;

	reset();
	//DB内想定-----------------------------------------------------
	//操作名	  コマンド	  Ctrl	Shift	Alt	 KeyCode
  	 cmdAry1 = ["上書き保存","Ctrl-s",0,true,false,false,83,null,null];
  	 cmdAry2 = ["シート名変更","Alt+O+H+R",1,false,false,false,18,79,72,82];
  	 cmdAry3 = ["名前をつけて保存","F12",0,false,false,false,123,null,null];
  	 cmdAry4 = ["値があるセルまで選択（下）","Ctrl-↓",0,true,false,false,40,null,null];
  	 cmdAry5 = ["印刷","Ctrl-p",0,true,false,false,80,null,null];
  	 //タブは伝搬されちゃう？
  	 cmdAry6 = ["ウインドウ変更","Alt-Tab",0,false,false,true,9,null,null];
	 //-----------------------------------------------------
	 cmdAryAll = [cmdAry1,cmdAry2,cmdAry3,cmdAry4,cmdAry5];
	 console.log("startset");
	 time=setInterval("sec++",1000);

	 setQuestion();
	 document.onkeydown=processHandle;
}



void processHandle(e){
 if(endflag != 1){
	console.log("processHandle");
	var shift, ctrl, alt;

	// Mozilla(Firefox, NN) and Opera
	if (e != null) {
		console.log("Mozilla preventDefault");
		keycode = e.which;
		ctrl    = typeof e.modifiers == 'undefined' ? e.ctrlKey : e.modifiers & Event.CONTROL_MASK;
		shift   = typeof e.modifiers == 'undefined' ? e.shiftKey : e.modifiers & Event.SHIFT_MASK;
		alt     = typeof e.modifiers == 'undefined' ? e.altKey : e.modifiers & Event.ALT_MASK;
		// イベントの上位伝播を防止
		//e.which = 0;
		//event.keyCode = 0;
		if(e.preventDefault()){
			console.log("e.preventDefault():"+e.preventDefault());
			e.preventDefault();
		}
		if(e.stopPropagation()){
			console.log("e.stopPropagation():"+e.stopPropagation());
			e.stopPropagation();
		}

		//IE仕様
		/*
		console.log("window.navigator.userAgent:"+window.navigator.userAgent);
		//if(window.navigator.userAgent.indexOf("Trident") != -1){
			console.log("Trident:"+keycode);
			event.keyCode = 0;
			event.cancelBubble = true;
			event.returnValue = false;
			e.returnValue = false;
			e.cancelBubble = true;

			event.preventDefault();
			event.stopPropagation();

		//}

		// Internet Explorer
	} else {
		console.log("IE returnValue");
		keycode = event.keyCode;
		ctrl    = event.ctrlKey;
		shift   = event.shiftKey;
		alt     = event.altKey;
		// イベントの上位伝播を防止
		event.returnValue = false;
		event.cancelBubble = true;
	}
	//[2]=0の場合はAlt or Ctrl一回以外すべて対応（）
	if(g_question[2] == 0){
		console.log("g_question[2]==0");
		//Ctrl、Alt、Shiftはすべて無視
		if(!(15 < keycode && keycode < 19)){
			if(g_question[3] == ctrl && g_question[4] == shift &&
				g_question[5] == alt && g_question[place] == keycode){
				correctProcess();
				console.log("g_question[2]==0_if_if");
			}
			else {
				console.log("g_question[2]==0_if_else");
				wrongProcess();
			}
		}
	}
	//altはキーを離す操作により値が変わるため検知しない（初めの一回はカウントしたいため押し続けは不正解とする）
	else{
		console.log("g_question[2]_else,place:keycode:"+place+":"+keycode);
		if(g_question[place] == keycode) correctProcess();
		else wrongProcess();
	}
 }
}
void correctProcess(){
	console.log("correctProcess");
	place++;
	if(g_question[place] == null){
		place = 6;
		qnum++;
		correctNum++;
		document.text.correct.value=correctNum;
		document.text.input.value=g_question[1];
		console.log("qnum:"+qnum);
		if(qnum == 20){
			result();
			endflag = 1;
		}else{
			empty();
			setQuestion();
		}
	}
}

void wrongProcess(){
	console.log("wrongProcess");
	wrongNum++;
	document.text.wrong.value=wrongNum;
}
void empty(){
	document.text.cmdName.value="";
	document.text.keyName.value="";
	document.text.input.value="";
}
void result(){
	document.text.correct.value=correctNum;
	document.text.wrong.value=wrongNum;
	document.text.percent.value=(correctNum/(correctNum+wrongNum)*100).toFixed(2);

	resultMin = (sec/60).toFixed(0);
	resultSec = (sec%60).toFixed(0);
	document.text.sumTime.value=resultMin+":"+resultSec;
	document.text.score.value=((correctNum-(wrongNum/2))*10-(resultMin+resultSec)).toFixed(0);
	empty();
}

}
*/