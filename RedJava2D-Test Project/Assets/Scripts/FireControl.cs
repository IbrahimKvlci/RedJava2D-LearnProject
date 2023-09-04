using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    CubeEnemy _cubeEnemy;
    Rigidbody2D _rigidBody;




    void Start()
    {
        _cubeEnemy=GameObject.FindGameObjectWithTag("CubeEnemy").GetComponent<CubeEnemy>();
        _rigidBody=GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(_cubeEnemy.GetPlayerDirection() *1000);

    }


    void FixedUpdate()
    {
        
    }


}
