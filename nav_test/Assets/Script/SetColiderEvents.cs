using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SetColiderEvents : MonoBehaviour
{
    public ParsePath parse_path;
    public SetPositions setPos;
    public Camera fpsCamera;

    public GameObject background;
    public GameObject setIsMoving;
    public bool isMoving = false;

    static string tagStopOver = "stopover";
    static string tagDestination = "destination";
    private string tagStart = "start";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌한 물체가 이제 경유지거나 도착지이면 이벤트 발생시키는 거!
    // 출발지일때도ㄱㄱ
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagStopOver || other.tag == tagDestination)
        {
            string nextObject_name = parse_path.destinations[parse_path.destIndex + 1];
            string currentObject_name = other.gameObject.name;

            string[] nextObject_arr = nextObject_name.Split('_');
            string[] currentObject_arr = currentObject_name.Split('_');

            string nextObject_building = nextObject_arr[1];
            string currentObject_building = currentObject_arr[1];

            string nextObject_floor = nextObject_arr[2];
            string currentObject_floor = currentObject_arr[2];

            // 경유지일 경우
            if (other.tag == tagStopOver)
            {
                // 건물 및 층수 비교 후 map과 dest 변경 결정
                // 건물이 같다
                if (nextObject_building == currentObject_building)
                {
                    // 층이 같다
                    if (nextObject_floor == currentObject_floor)
                    {
                        // isMoving이 false이면 destObject로 길 안내 시작
                        if (!isMoving)
                        {
                            Debug.Log("이동 완료했고, 제대로 된 건물과 층수에 도착했음");
                            
                            fpsCamera.GetComponent<FollowTarget>().Switch();
                            background.SetActive(false);
                            parse_path.destIndex++;
                            setPos.set_positions(currentObject_name);
                        }
                        else
                        {
                            Debug.Log("isMoving이 완료되지 못했음");
                        }
                    }
                    // 층이 다르다
                    else
                    {
                        Debug.Log(nextObject_building + "_floor_" + nextObject_floor + "로 이동하는 중");
                        // map과 dest 변경
                        setPos.set_Map_and_Destinations(nextObject_building + "_floor_" + nextObject_floor);
                        parse_path.destIndex++;
                        // 태그 설정
                        setPos.setTag();
                        // personIndicator를 다음 경유지로 이동시키고 비활성화(움직이지 않게)
                        //setPos.setPersonPos(nextObject_name);
                        setPos.person.SetActive(false);
                        // view 변환 (계단 이동하는 동안 화살표 안 보이게)
                        fpsCamera.GetComponent<FollowTarget>().Switch();
                        background.SetActive(true);
                        // 화살표에 필요한 트리거 삭제
                        Destroy(GameObject.Find("Anchor"));
                        Destroy(GameObject.Find("NavTrigger(Clone)"));
                        // 이동 완료 버튼 활성화
                        isMoving = true;
                        setIsMoving.SetActive(true);
                    }
                }
                // 건물이 다르다
                else
                {

                }
            }
        }
        // 도착지일 경우 (근데 이거는 updatenav에서 해주지 않나?
        else if (other.tag == tagDestination)
        {

        }
        
    }
}
