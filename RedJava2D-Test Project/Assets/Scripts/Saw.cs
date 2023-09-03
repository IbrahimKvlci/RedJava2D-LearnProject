using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Saw : MonoBehaviour
{
    GameObject[] _sawPositionObjects;
    Vector3 _far;

    private bool _getFar = true;
    private bool _forwardorBack = true;

    float _speed=10;
    int _farObjectsCount = 0;

    void Start()
    {
        _sawPositionObjects = new GameObject[transform.childCount];
        for (int i = 0; i < _sawPositionObjects.Length; i++)
        {
            _sawPositionObjects[i] = transform.GetChild(0).gameObject;
            _sawPositionObjects[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        SawMovement();
    }

    void SawMovement()
    {
        
        
        if (_getFar)
        {
             _far = (_sawPositionObjects[_farObjectsCount].transform.position - transform.position).normalized;
             _getFar = false;
        }
        transform.position += _far * Time.deltaTime * _speed;
        float _farValue=Vector3.Distance(transform.position,_sawPositionObjects[_farObjectsCount].transform.position);
        if (_farValue < 0.5f)
        {
            if (_farObjectsCount == _sawPositionObjects.Length - 1)
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
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position,transform.GetChild(i+1).transform.position);
        }
    }
#endif
}

