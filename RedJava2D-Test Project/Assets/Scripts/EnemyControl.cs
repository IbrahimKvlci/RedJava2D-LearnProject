using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    GameObject[] _positionObjects;
    Vector3 _far;

    private bool _getFar = true;
    private bool _forwardorBack = true;

    public float _speed = 10;
    int _farObjectsCount = 0;

    void Start()
    {
        _positionObjects = new GameObject[transform.childCount];
        for (int i = 0; i < _positionObjects.Length; i++)
        {
            _positionObjects[i] = transform.GetChild(0).gameObject;
            _positionObjects[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {


        if (_getFar)
        {
            _far = (_positionObjects[_farObjectsCount].transform.position - transform.position).normalized;
            _getFar = false;
        }
        transform.position += _far * Time.deltaTime * _speed;
        float _farValue = Vector3.Distance(transform.position, _positionObjects[_farObjectsCount].transform.position);
        if (_farValue < 0.5f)
        {
            if (_farObjectsCount == _positionObjects.Length - 1)
            {
                _forwardorBack = false;
            }
            else if (_farObjectsCount == 0)
            {
                _forwardorBack = true;
            }
            if (_forwardorBack)
            {
                _farObjectsCount++;
            }
            else
            {
                _farObjectsCount--;
            }
            _getFar = true;
        }


    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}

