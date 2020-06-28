using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attach this script to the AR camera
public class FollowTarget : MonoBehaviour
{
    public Transform targetToFollow;    // The transform of the gameobject that gets followed
    public Quaternion targetRot;        // The rotation of the device camera from Frame.Pose.rotation    
    public RawImage minimap;            // The rawimage the view of the camera gets rendered to
    public Camera fullscreenCamera;     // The camera that captures the map (follow camera)
    public GameObject person;            // The direction indicator on the person indicator
    public GameObject switchButton;     // Button that switches the views
    public float rotationSmoothingSpeed = 1.5f; // rotation speed, change to personal preference

    private bool map = false;           // boolean to tell if map is showing (phone position)
    private bool pressed = false;       // boolean to tell if map is showing (button press)
    private RenderTexture texture;      // field to save texture to set again after view switch

    // Use lateUpdate to assure that the camera is updated after the target has been updated.
    void LateUpdate()
    {
        if (!targetToFollow)
            return;
        //receive rotation from camera
        Vector3 targetEulerAngles = targetRot.eulerAngles;
        
        // Calculate the current rotation angle around the Y axis we want to apply to the camera.
        // We add 180 degrees as the device camera points to the negative Z direction
        float rotationToApplyAroundY = targetEulerAngles.y; //+ 180;
        //Debug.Log(fullscreenCamera.gameObject.transform.localRotation.eulerAngles);
        //Debug.Log("old:" + rotationToApplyAroundY);
        // Smooth interpolation between current camera rotation angle and the rotation angle we want to apply.
        // Use LerpAngle to handle correctly when angles > 360
        float newCamRotAngleY = Mathf.LerpAngle(person.transform.eulerAngles.y, rotationToApplyAroundY, rotationSmoothingSpeed * Time.deltaTime);
        Quaternion newCamRotYQuat = Quaternion.Euler(0, newCamRotAngleY, 0);
        //extra check to make sure that the rotation of the arrow does not change when accessing mapview from placing phone horizontal
        if (targetEulerAngles.x < 65)
        {
            person.transform.rotation = newCamRotYQuat;
        }
    }

    //logic when switch button is pressed
    public void Switch()
    {
        if (!pressed)
        {
            //show mapview
            pressed = true;
            gameObject.GetComponent<Camera>().enabled = false;
            minimap.gameObject.SetActive(false);
            texture = fullscreenCamera.targetTexture;
            fullscreenCamera.targetTexture = null;
            fullscreenCamera.orthographicSize = 15;
        }
        else
        {
            //show cameraview
            pressed = false;
            gameObject.GetComponent<Camera>().enabled = true;
            minimap.gameObject.SetActive(true);
            fullscreenCamera.targetTexture = texture;
            fullscreenCamera.orthographicSize = 7;
        }
    }

}
