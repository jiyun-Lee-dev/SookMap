using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsMoving : MonoBehaviour
{
    public GameObject person;
    public GameObject Indicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void clickSetIsMoving()
    {
        person.SetActive(true);
        Indicator.GetComponent<SetColiderEvents>().isMoving = false;
        this.gameObject.SetActive(false);
    }
}
