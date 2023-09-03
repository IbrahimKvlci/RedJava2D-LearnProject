using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] float _rotateSpeed=10;
    [SerializeField] int _damage;

    CharacterController _characterController;

    void Start()
    {
        _characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

   
    void FixedUpdate()
    {
        transform.Rotate(0, 0, _rotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _characterController.TakeDamage(_damage);
        }
    }

}
