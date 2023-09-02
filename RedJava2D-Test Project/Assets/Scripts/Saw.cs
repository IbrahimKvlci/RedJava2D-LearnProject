using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Saw : MonoBehaviour
{
    GameObject[] _sawPositionObjects;
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
    }

    void SawMovement()
    {

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

