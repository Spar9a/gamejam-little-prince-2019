using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarShaking : MonoBehaviour
{

    private Transform _this;
    // Start is called before the first frame update
    void Start()
    {
        _this = this.transform;
        Shake();
    }

    private void Shake()
    {
        _this.DOPunchScale(Vector3.one,Random.Range(1f, 10f), 1, 0.25f).OnComplete(Shake);
    }
    
}
