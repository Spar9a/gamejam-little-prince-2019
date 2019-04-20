using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

    public Transform target;
    [HideInInspector]
    public float nextCameraX;
    [Space]
    public GameObject CameraObject;
    [SerializeField]
    float characterOffset = 1f;
    
    void Start () {
        nextCameraX = CameraObject.transform.position.x;
    }   
	
	void Update ()
    {
        var deltaTime = Time.deltaTime;
        var targetPos = target.position;
        var currentPos = transform.position;

        CameraObject.transform.position = Vector3.Lerp(CameraObject.transform.localPosition, 
            new Vector3(nextCameraX, CameraObject.transform.localPosition.y, CameraObject.transform.localPosition.z), 
            2.2f * deltaTime);
        
        transform.position = Vector3.Lerp(
            currentPos, 
            new Vector3(targetPos.x, targetPos.y + characterOffset, targetPos.z), 
            5f * deltaTime);
    }
}
