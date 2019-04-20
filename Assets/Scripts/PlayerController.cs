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
        //Earth.Ro = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //h = Input.GetAxis("Vertical") * Time.deltaTime * speed;
    }
}
