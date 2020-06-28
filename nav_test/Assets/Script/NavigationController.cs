using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//class that handles all navigation
public class NavigationController : MonoBehaviour
{
    public SetPositions setPos;
    public GameObject trigger;
    public string[] destinations; // list of destination positions
    public Transform target = null; // current choosen destination
    public GameObject person; // person indicator
    public Text text;  // information text box

    private NavMeshPath path; // current calculated path
    private LineRenderer line; // linerenderer to display path

    private bool destinationSet; // bool to say if a destination is set


    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        destinationSet = false;
        
     }

    // Update is called once per frame
    void Update()
    {
        //if a target is set, calculate and update path
        if (target != null)
        {
            NavMesh.CalculatePath(person.transform.position, target.position, NavMesh.AllAreas, path);
            //lost path due to standing above obstacle (drift)
            if (path.corners.Length == 0)
            {
                text.text = "Try moving away for obstacles (optionally recalibrate)";
            }
            else
            {
                text.text = "";
            }
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            line.enabled = true;
        }
    }

    //set current destination and create a trigger for showing AR arrows
    public void setDestination(int index) 
    {
        string destPos = destinations[index];
        foreach (Transform child in setPos.calibrationLocations.transform)
        {
            if (child.name.Equals(destPos))
            {
                target = child;
                break;
            }
        }
        GameObject.Instantiate(trigger, person.transform.position, person.transform.rotation);
    }

    // remove AR arrow when path is cleared
    private void RemoveArrowAndCollider()
    {
        Destroy(GameObject.Find("NavTrigger(Clone)"));
        Destroy(GameObject.Find("Anchor"));
    }

}
