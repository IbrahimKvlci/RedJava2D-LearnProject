using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] float _rotateSpeed=10;


    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotateSpeed);
    }
    

}
