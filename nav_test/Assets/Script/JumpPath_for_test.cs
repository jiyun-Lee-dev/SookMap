using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 씬 전환 및 도착 후 길 안내 종료를 위한 테스트 기능
// indicator를 현재 목표지점 바로 앞으로 이동시킴
public class JumpPath_for_test : MonoBehaviour
{
    public ParsePath parse_path;
    public GameObject person; // person indicator

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        if (parse_path.destinations[parse_path.destIndex] != "")
        {
            GameObject nextDest = GameObject.Find(parse_path.destinations[parse_path.destIndex]);
            Debug.Log("Jump: " + nextDest.name);
            person.GetComponent<NavMeshAgent>().SetDestination(nextDest.transform.position);
            //person.GetComponent<NavMeshAgent>().isStopped = true;
            //person.transform.position = new Vector3(nextDest.transform.position.x, nextDest.transform.position.y, nextDest.transform.position.z);
        }
    }
}
