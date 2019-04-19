using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform Earth;

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float timeMultiple = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Earth.Rotate(new Vector3(0,0, (speed * -1)));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Earth.Rotate(new Vector3(0,0, speed));
        }
        if (Input.GetKey(KeyCode.W))
        {
            Earth.Rotate(new Vector3((speed * -1),0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            Earth.Rotate(new Vector3(speed,0, speed));
        }
    }
}
