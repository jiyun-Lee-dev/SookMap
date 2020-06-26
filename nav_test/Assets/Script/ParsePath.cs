using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsePath : MonoBehaviour
{
    private string stair = "stair";
    private string elevator = "elevator";
    private Transform closest = null;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] parsing(string currentPos, string destPos)
    {
        string[] result = new string[10];
        int index = 0;
        GameObject[] Stairs = GameObject.FindGameObjectsWithTag(stair);
        GameObject[] Elevators = GameObject.FindGameObjectsWithTag(elevator);
        float closestDistSqr = Mathf.Infinity;
        Transform closest = null;

        if (currentPos[0] == destPos[0])
        {
            Debug.Log("같은 층입니다.");
            result.SetValue(destPos, index++);
            /*
            // 2층일 때
            if (int.Parse(currentPos.Substring(0, 1)) == 2)
            {
                // 출발이 명신 왼쪽
                if (int.Parse(currentPos.Substring(0, 3)) < 212)
                {
                    // 도착이 명신 오른쪽
                    if (int.Parse(destPos.Substring(0, 3)) >= 212)
                    {
                        // 미소찬 계단 이용해서 3층으로 -> 명신 중앙 계단 이용해서 2층으로

                    }
                }
                // 출발이 명신 오른쪽
                else if (int.Parse(currentPos.Substring(0, 3)) >= 212)
                {
                    // 도착이 명신 왼쪽
                    if (int.Parse(destPos.Substring(0, 3)) < 212)
                    {
                        // 명신 중앙 계단 이용해서 3층으로 -> 미소찬 계단으로 2층으로
                    }
                }
            }
            */
        }
        // 장소 앞자리가 다를 때
        else
        {
            Debug.Log("출발 층: " + int.Parse(currentPos.Substring(0, 1)));
            // 출발 층 > 도착 층
            if (int.Parse(currentPos.Substring(0, 1)) > int.Parse(destPos.Substring(0, 1)))
            {
                // 층 차이가 2 이상
                if (int.Parse(currentPos.Substring(0, 1)) - int.Parse(destPos.Substring(0, 1)) >= 2)
                {
                    // 출발지 4층 초과일 때
                    if (int.Parse(currentPos.Substring(0, 1)) > 4)
                    {
                        // 출발지 7층일 때
                        if (currentPos.Substring(0, 1) == "7")
                        {
                            // 도착지 5층일 때
                            if (int.Parse(destPos.Substring(0, 1)) == 5)
                            {
                                // 계단만 이용해서 바로 목적지로 이동
                                // currentPos에서 가장 가까운 계단 찾기
                                foreach (GameObject all_stair in Stairs)
                                {
                                    Vector3 objectPos = all_stair.transform.position;
                                    dist = (objectPos - GameObject.Find(currentPos).transform.position).sqrMagnitude;
                                    if (dist < closestDistSqr)
                                    {
                                        closestDistSqr = dist;
                                        closest = all_stair.transform;
                                        result.SetValue(all_stair.ToString(), 1);
                                    }
                                }
                            }
                            // 6층 경유, 엘베 이용
                            else
                            {
                                // 도착이 명신 왼쪽
                                if (int.Parse(destPos.Substring(0, 3)) < 212)
                                {
                                    // 3층까지 엘베 이용, 그 뒤 미소찬 계단 이용
                                }
                            }
                        }
                    }
                    // 출발지가 4층일 때
                    else if (currentPos.Substring(0, 1) == "4")
                    {
                        // 도착이 명신 왼쪽
                        if (int.Parse(destPos.Substring(0, 3)) < 212)
                        {
                            // 미소찬 계단 이용
                        }
                        else
                        {
                            // 엘베 이용
                        }
                    }
                    // 출발지 3층 이하일 때 무조건 계단
                    else
                    {

                    }
                }
                // 층 차이 2 미만
                else
                {
                    // 출발지 3층일 때
                    if (currentPos.Substring(0, 1) == "3")
                    {
                        // 미소찬 or 새힘관 계단 이용
                    }
                    else if (currentPos.Substring(0, 1) == "2" && int.Parse(currentPos.Substring(0, 3)) >= 212)
                    {
                        // 3층으로 이동 -> 미소찬 계단 이용
                    }
                    // currentPos에서 가장 가까운 계단 찾기
                    foreach (GameObject all_stair in Stairs)
                    {
                        Vector3 objectPos = all_stair.transform.position;
                        dist = (objectPos - GameObject.Find(currentPos).transform.position).sqrMagnitude;
                        if (dist < closestDistSqr)
                        {
                            closestDistSqr = dist;
                            closest = all_stair.transform;
                            result.SetValue(all_stair.ToString(), 1);
                        }
                    }
                }
            }
            // 출발 층 < 도착 층
            else
            {
                // 층 차이가 2 이상 
                if (int.Parse(destPos.Substring(0, 1)) - int.Parse(currentPos.Substring(0, 1)) >= 2)
                {
                    // 도착지 7층일 때
                    if (destPos.Substring(0, 1) == "7")
                    {
                        // 6층까지 이동 후 계단으로 7층 이동
                    }
                    else
                    {
                        foreach (GameObject all_elevator in Elevators)
                        {
                            Vector3 objectPos = all_elevator.transform.position;
                            dist = (objectPos - GameObject.Find(currentPos).transform.position).sqrMagnitude;
                            if (dist < closestDistSqr)
                            {
                                closestDistSqr = dist;
                                closest = all_elevator.transform;
                                result.SetValue(all_elevator.ToString(), 1);
                            }
                        }
                    }
                }
                else
                {

                }
            }
        }
        

        return result;
    }
}
