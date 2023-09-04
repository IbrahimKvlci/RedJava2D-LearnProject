using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour
{
    GameObject _player;
    [SerializeField] GameObject _fire;
    [SerializeField] Sprite _forward, _back;
    SpriteRenderer _renderer;


    RaycastHit2D _ray;
    [SerializeField] LayerMask _layerMask;

    float _shootTime = 0;
    [SerializeField] int _damage;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _renderer=GetComponent<SpriteRenderer>();

    }


    void FixedUpdate()
    {
        SeePlayer();

    }

    void Shoot()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > Random.Range(0.2f, 1))
        {
            Instantiate(_fire, transform.position, Quaternion.identity);
            _shootTime = 0;
        }

    }

    void SeePlayer()
    {
        Vector3 distancePlayerEnemy=_player.transform.position-transform.position;
        _ray=Physics2D.Raycast(transform.position, distancePlayerEnemy,1000,_layerMask);
        Debug.DrawLine(transform.position, _ray.point, Color.red);
        EnemyControl enemyControl = GetComponent<EnemyControl>();
        if (_ray.collider.tag == "Player")
        {
            _renderer.sprite = _forward;
            enemyControl._speed = 10;
            Shoot();
        }
        else
        {
            _renderer.sprite = _back;
            enemyControl._speed = 6;
        }
    }

    public Vector3 GetPlayerDirection()
    {
        return (_player.transform.position - transform.position).normalized;
    }


}
