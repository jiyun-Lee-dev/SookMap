using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

public class SetPositions : MonoBehaviour
{
    public GameObject ardevice;
    public GameObject person;
    //public GameObject controller;
    public GameObject calibrationLocations;
    public NavigationController naviCtrl;
    public GameObject trigger; // trigger to spawn and despawn AR arrows
    public ParsePath parse_path;
    
    // Start is called before the first frame update
    void Start()
    {
        naviCtrl = GameObject.Find("PathShower").GetComponent<NavigationController>();
        parse_path = GameObject.Find("ParsePath").GetComponent<ParsePath>();
        // 원래는 안드에서 오브젝트 이름 받아와야 하는데 일단 임의로 집어넣음.
        set_positions("304/321");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_positions(string positions_str)
    {
        string[] positions = positions_str.Split('/');
        string currentPos = positions[0];
        string destPos = positions[1];
        /*
        // 출발지와 목적지 값을 parse_path 함수에 넘겨주면서 호출
        // 경로 파싱 후 destinations[] 리턴 받고 나서 이제 NavController의 set_destinations를 호출하는 거임.
        // destinations에는 오브젝트들의 이름(string)이 들어가 있으면 될듯. 지금 suengeun이 리턴받은 배열이라고 가정함.
        */


        // 리턴 받은 배열을 temp 배열에 넣어줌
        String[] temp = new String[10];
        temp = (String[])parse_path.parsing(currentPos, destPos).Clone();
        /*
        for (int i= 0; i <temp.Length; i++)
        {
            Debug.Log("parsing result: " + temp[i]);
        }
        */
        
        // 1개 이상의 목적지가 있는지 체크
        if (temp[0] != "")
        {
            // personIndicator 출발지로 위치 설정
            foreach (Transform child in calibrationLocations.transform)
            {
                if (child.name.Equals(currentPos))
                {
                    person.transform.position = child.transform.position;
                    break;
                }

            }
            // navigationController스크립트의 destinations[]를 대입해주고, navigationController에 있는 setDestinations 함수 호출
            naviCtrl.destinations = (String[])temp.Clone();
            naviCtrl.setDestination(0);
            ardevice.GetComponent<ARCoreSession>().enabled = true;
            this.gameObject.SetActive(false);
        }

        return;
    }
}
