using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleARCore;
using UnityEngine.AI;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class SetPositions : MonoBehaviour
{
    public GameObject ardevice;
    public GameObject person;
    public GameObject map;
    public GameObject calibrationLocations;
    public NavMeshSurface surface;

    public NavigationController naviCtrl;
    public GameObject trigger; // trigger to spawn and despawn AR arrows

    public ParsePath parse_path;

    static string tagStopOver = "stopover";
    static string tagDestination = "destination";

    // Start is called before the first frame update
    void Start()
    {
        naviCtrl = GameObject.Find("PathShower").GetComponent<NavigationController>();
        // 원래는 안드에서 오브젝트 이름 받아와야 하는데 일단 임의로 집어넣음.
        //set_positions("room_MS_4_405B/room_MS_3_321");
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void init_set_positions()
    {
        string currentPos = parse_path.destinations[parse_path.destIndex - 1];
        string nextPos = parse_path.destinations[parse_path.destIndex];

        string[] currentPos_arr = currentPos.Split('_');
        string current_building = currentPos_arr[1];
        string current_floor = currentPos_arr[2];
        string currentMap_name = current_building + "_floor_" + current_floor;
        GameObject currentObject = GameObject.Find(currentPos);

        /*
        for (int i= 0; i <temp.Length; i++)
        {
            Debug.Log("parsing result: " + temp[i]);
        }
        */

        // 1개 이상의 목적지가 있는지 체크할 필요 없을 거 같음 setPos 호출 시에 예외처리하니까
        this.setPersonPos(currentPos);
        this.setTag();
        this.callNavCtrl();
    }

    public void set_positions(string pos)
    {
        this.setPersonPos(pos);
        this.setTag();
        this.callNavCtrl();
    }

    // personIndicator 원하는 지점으로 위치 이동
    public void setPersonPos(string pos)
    {
        foreach (Transform child in calibrationLocations.transform)
        {
            if (child.name.Equals(pos))
            {
                person.transform.position = child.transform.position;
                break;
            }
        }
    }

    public void setTag()
    {
        // 다음 dest는 경유지
        if (parse_path.destinations[parse_path.destIndex + 1] != "" && parse_path.destIndex < 10)
            GameObject.Find(parse_path.destinations[parse_path.destIndex]).tag = tagStopOver;
        else
            GameObject.Find(parse_path.destinations[parse_path.destIndex]).tag = tagDestination;
        Debug.Log("SetTag: " + GameObject.Find(parse_path.destinations[parse_path.destIndex]).name);
        Debug.Log("SetTag: " + GameObject.Find(parse_path.destinations[parse_path.destIndex]).tag);
    }

    public void set_Map_and_Destinations(string currentMap_name)
    {
        // 이전의 인스턴스는 삭제
        if (GameObject.FindGameObjectWithTag("Map"))
            Destroy(GameObject.FindGameObjectWithTag("Map"));
        if (GameObject.FindGameObjectWithTag("Destinations"))
            Destroy(GameObject.FindGameObjectWithTag("Destinations"));

        // 해당 층의 프리팹으로 map이랑 dest 인스턴스화
        map = Instantiate(Resources.Load("Map/" + currentMap_name)) as GameObject;
        calibrationLocations = Instantiate(Resources.Load("Destinations/" + currentMap_name + "_Destinations")) as GameObject;

        // navmesh 생성
        if (surface.navMeshData != null)
            surface.UpdateNavMesh(surface.navMeshData);
        else
            surface.BuildNavMesh();
        
        // dest 오브젝트들 안 보이게 설정
        MeshRenderer[] allChildren = calibrationLocations.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in allChildren)
        {
            child.enabled = false;
        }
    }

    // navigationController스크립트의 destinations[]를 대입해주고, navigationController에 있는 setDestinations 함수 호출
    public void callNavCtrl()
    {
        Debug.Log("악");
        naviCtrl.destinations = (String[])parse_path.destinations.Clone();
        naviCtrl.setDestination(parse_path.destIndex);
        ardevice.GetComponent<ARCoreSession>().enabled = true;
    }
}
