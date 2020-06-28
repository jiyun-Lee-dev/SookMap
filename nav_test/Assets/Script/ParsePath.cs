using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// 싱글톤 패턴 적용
public class ParsePath : MonoBehaviour
{
    public string[] destinations = new string[10];
    public int destIndex = 1;
    public GameObject startObject;
    public SetPositions setPos;

    private string stair = "stair";
    private string elevator = "elevator";
    private string tagStart = "start";
    
    /*
    private static ParsePath instance;
    public static ParsePath Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<ParsePath>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newParsePath = new GameObject("ParsePath").AddComponent<ParsePath>();
                    instance = newParsePath;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<ParsePath>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    */

    void Start()
    {
        parsing("room_MS_4_405B/room_MS_3_321");
    }

    public void parsing(string positions_str)
    {
        int count = 0;
        string[] positions = positions_str.Split('/');
        string startObject_name = positions[0];
        string destObject_name = positions[1];
        string[] startObject_arr = startObject_name.Split('_');
        string[] destObject_arr = destObject_name.Split('_');
        string start_building = startObject_arr[1];
        string dest_building = destObject_arr[1];
        string start_floor = startObject_arr[2];
        string dest_floor = destObject_arr[2];

        string startMap_name = start_building + "_floor_" + start_floor;
        setPos.set_Map_and_Destinations(startMap_name);
        destinations.SetValue(startObject_name, count++);
        //GameObject.Find(startObject_name).tag = tagStart;

        if (start_building == dest_building)
        {
            Debug.Log("같은 건물입니다.");
            if (start_floor == dest_floor)
            {
                Debug.Log("같은 층입니다.");
                destinations.SetValue(destObject_name, count++);
            }
            else
            {
                Debug.Log("다른 층입니다.");
                // 임의로 데이터 세팅
                destinations.SetValue("stair_MS_4_0_down", count++);
                destinations.SetValue("stair_MS_3_0_down", count++);
                destinations.SetValue(destObject_name, count++);
            }
        } else
        {
            Debug.Log("다른 건물입니다.");
        }

        // setPos 함수 호출
        setPos.init_set_positions();
    }
}
