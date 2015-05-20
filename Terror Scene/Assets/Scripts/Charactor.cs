using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class CharactorInfo {
	public string name ;
	public int[] speed ;
	public int[] might ;
	public int[] sanity ;
	public int[] knowledge ;
	public int modelid ;
	public int level_speed ;
	public int level_might ;
	public int level_sanity ;
	public int level_knowledge ;

	public int get_speed() {
		return speed [level_speed];
	}


	public bool change_speed_level(int t){	//返回是否死亡，true表示死亡，flase表示未死亡
		level_speed += t;
		if (level_speed > 8)
			level_speed = 8;
		if (level_speed < 1) {
			level_speed = 1;
			return false;
		}
		return true;
	}

	public string to_str() {
		string tmp = "name:" + name + "\nspeed:";
		foreach (int t in speed) {
			tmp = tmp + t.ToString () + " ";
		}
		tmp = tmp + "\nmight:";
		foreach (int t in might) {
			tmp = tmp + t.ToString () + " ";
		}
		tmp = tmp + "\nsanity:";
		foreach (int t in sanity) {
			tmp = tmp + t.ToString () + " ";
		}
		tmp = tmp + "\nknowledge:";
		foreach (int t in knowledge) {
			tmp = tmp + t.ToString () + " ";
		}
		tmp = tmp + "\nmodelid:" + modelid + "\nlevels:";
		tmp = tmp + level_speed + " " + level_might + " " + level_sanity + " " + level_knowledge;
		return tmp;
	}
}

public class CharactorAll {
	public List<CharactorInfo> charactor_all;
}

public class Charactor : MonoBehaviour {
	//public List<oneCharactor> charactors = new List<OneCharactor> ();
	public CharactorAll m_CharactorAll = null;
	public List<CharactorInfo> charactor_list;
	void Start () {
		LoadCharactorInfo ();
		ShowAllCharactor (m_CharactorAll);
	}

	void Update () {  
		
	}  

	void LoadCharactorInfo() {
		UnityEngine.TextAsset s = Resources.Load ("Charactors/CharactorInfo") as TextAsset;
		string tmp = s.text;
		m_CharactorAll = JsonMapper.ToObject<CharactorAll> (tmp);

	}


	void ShowAllCharactor(CharactorAll ch) {
		if (m_CharactorAll == null) {
			Debug.Log (1);
			return;
		}
		foreach (CharactorInfo info in ch.charactor_all) {
			Debug.Log (info.to_str());
		}
	}

	bool addcharactor(string name)
	{

		return true;
	}
}

