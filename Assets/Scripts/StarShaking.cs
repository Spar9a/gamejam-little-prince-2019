using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShaking : MonoBehaviour
{

    private Transform _this;
    // Start is called before the first frame update
    void Start()
    {
        _this = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        _this.position = Vector3.Lerp(_this.position, Vector3.right * 1, Time.deltaTime);
    }


}
