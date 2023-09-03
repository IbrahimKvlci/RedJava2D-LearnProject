using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    CubeEnemy _cubeEnemy;
    Rigidbody2D _rigidBody;
    CharacterController _characterController;

    [SerializeField] int _damage;

    void Start()
    {
        _cubeEnemy=GameObject.FindGameObjectWithTag("Enemy").GetComponent<CubeEnemy>();
        _rigidBody=GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(_cubeEnemy.GetPlayerDirection() *1000);
        _characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _characterController.TakeDamage(_damage);
        }
    }
}
