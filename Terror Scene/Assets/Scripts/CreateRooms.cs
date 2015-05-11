using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class roompos {
	public int x;
	public int y;
	public int roomid;
	public roompos(int _x, int _y, int _roomid)
	{
		x = _x;
		y = _y;
		roomid = _roomid;
	}
}

public class CreateRooms : MonoBehaviour {
	public GameObject op;				//绑定目前操作的对象
	public GameObject[] rooms;			//绑定所有的房间
	int[] room_stats;					//记录所有房间的创建状态，保证每个房间只会出现1次
	public List<roompos> Ground = new List<roompos>(); 		//记录地图
	int remain_room_num;				//记录剩余房间数量
	const float room_gap = 7.2f;		//房间间隔距离
	int posx = 0;
	int posy = 0;
	// Use this for initialization
	void Start () {
		room_stats = new int[rooms.Length];
		remain_room_num = room_stats.Length;
		for (int i=0; i < room_stats.Length; ++i)
			room_stats [i] = 0;
		create_new_room (0, 0);
	}

	bool create_new_room(int x, int y){
		int num = get_random_room ();
		if (num == -1)
			return false;
		Ground.Add (new roompos (x, y, num));
		Debug.Log (num);
		Instantiate (rooms [num], new Vector3 (((float)x) * room_gap, 0f, ((float)y) * room_gap),new Quaternion(0,0,0,0));
		remain_room_num = rooms.Length - 1;
		return true;
	}

	int get_random_room(){
		if (remain_room_num > 0) {
			int num = Random.Range (0, room_stats.Length);
			Debug.Log (room_stats.Length);
			while (room_stats[num] != 0) {
				num = Random.Range (0, room_stats.Length);
				Debug.Log (num);
			}
			room_stats [num] = 1;
			return num;
		} else {
			Debug.Log ("没有多余房间了");
			return -1;
		}

	}

	bool go_to_room(int x, int y)
	{
		foreach(roompos onepos in Ground)
		{
			if (x == onepos.x && y == onepos.y) 
				return true;
		}
		return create_new_room (x, y);
	}
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		op.GetComponent<Transform> ().position += movement * Time.deltaTime;
		Vector3 nowpos = op.GetComponent<Transform> ().position;
		if (nowpos.x < ((float)posx) * room_gap - room_gap / 2) {
			if (go_to_room(posx - 1,posy)) 
				posx--;
		} else if (nowpos.x > ((float)posx) * room_gap + room_gap / 2){
			if (go_to_room(posx + 1,posy)) 
				posx++;
		} else if (nowpos.z < ((float)posy) * room_gap - room_gap / 2){
			if (go_to_room(posx ,posy - 1)) 
				posy--;
		} else if (nowpos.z > ((float)posy) * room_gap + room_gap / 2){
			if (go_to_room(posx ,posy + 1)) 
				posy++;
		}
	}


}
